Imports MySql.Data.MySqlClient
Imports System.Net
Imports System.Net.Sockets
Imports System.Windows.Forms

Public Module SessionManager

    Public Const TimeoutMinutes As Integer = 30
    Private Const WarningSeconds As Integer = 120
    Private Const HeartbeatSeconds As Integer = 60

    Public Delegate Sub SessionTickHandler(remaining As TimeSpan)
    Public Event SessionTick As SessionTickHandler

    Private _isActive As Boolean
    Private _lastActivity As Date
    Private _lastHeartbeat As Date
    Private _activitySinceHeartbeat As Boolean
    Private _filter As ActivityFilter
    Private _exitHooked As Boolean
    Private WithEvents _timer As New System.Windows.Forms.Timer() With {.Interval = 1000}

    Public Property UserId As Integer
    Public Property RoleId As Integer
    Public Property Username As String = ""
    Public Property SessionId As Long
    Public Property LoginTime As Date

    Public ReadOnly Property IsActive As Boolean
        Get
            Return _isActive
        End Get
    End Property

    Public Function RoleName() As String
        Select Case RoleId
            Case 1
                Return "Admin"
            Case 2
                Return "Teacher"
            Case 3
                Return "Student"
            Case Else
                Return "Unknown"
        End Select
    End Function

    ' ---------- Start ----------
    Public Function TryStartSession(userId As Integer, roleId As Integer, username As String, ByRef message As String) As Boolean
        Dim conn As MySqlConnection = DBConnection.GetLoginConnection()
        Dim ip As String = DetectLocalIp()
        Dim newSessionId As Long

        Try
            conn.Open()

            ' Sessions that stopped sending activity are treated as ended
            Using cleanup As New MySqlCommand(
                "UPDATE UserSessions SET is_active = 0, logout_time = last_activity " &
                "WHERE user_id = @u AND is_active = 1 " &
                "AND last_activity < DATE_SUB(NOW(), INTERVAL " & TimeoutMinutes & " MINUTE)", conn)
                cleanup.Parameters.AddWithValue("@u", userId)
                cleanup.ExecuteNonQuery()
            End Using

            Using check As New MySqlCommand(
                "SELECT COUNT(*) FROM UserSessions WHERE user_id = @u AND is_active = 1", conn)
                check.Parameters.AddWithValue("@u", userId)
                If Convert.ToInt32(check.ExecuteScalar()) > 0 Then
                    message = "This account is already logged in on another session."
                    Return False
                End If
            End Using

            Using insert As New MySqlCommand(
                "INSERT INTO UserSessions (user_id, ip_address) VALUES (@u, @ip)", conn)
                insert.Parameters.AddWithValue("@u", userId)
                insert.Parameters.AddWithValue("@ip", ip)
                insert.ExecuteNonQuery()
                newSessionId = insert.LastInsertedId
            End Using

        Catch ex As Exception
            message = "Could not start the session: " & ex.Message
            Return False
        Finally
            conn.Close()
        End Try

        SessionManager.UserId = userId
        SessionManager.RoleId = roleId
        SessionManager.Username = username
        SessionManager.SessionId = newSessionId
        SessionManager.LoginTime = Date.Now

        DBConnection.CurrentUserId = userId
        DBConnection.CurrentRoleId = roleId
        DBConnection.CurrentIp = ip

        _lastActivity = Date.Now
        _lastHeartbeat = Date.Now
        _activitySinceHeartbeat = False
        _isActive = True

        _filter = New ActivityFilter()
        Application.AddMessageFilter(_filter)
        _timer.Start()

        If Not _exitHooked Then
            AddHandler Application.ApplicationExit, Sub(s, e) EndSession()
            _exitHooked = True
        End If

        Return True
    End Function

    ' ---------- End ----------
    Public Sub EndSession()
        If Not _isActive Then Return
        _isActive = False
        _timer.Stop()

        If _filter IsNot Nothing Then
            Application.RemoveMessageFilter(_filter)
            _filter = Nothing
        End If

        Try
            Using conn As MySqlConnection = DBConnection.GetLoginConnection()
                conn.Open()
                Using cmd As New MySqlCommand(
                    "UPDATE UserSessions SET logout_time = NOW(), is_active = 0 WHERE session_id = @id", conn)
                    cmd.Parameters.AddWithValue("@id", SessionId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            ApplicationLogger.LogError("Session operation failed: " & ex.Message, ex)
        End Try

        DBConnection.CurrentUserId = 0
        DBConnection.CurrentRoleId = 0
    End Sub

    ' ---------- Activity and timeout ----------
    Friend Sub RegisterActivity()
        _lastActivity = Date.Now
        _activitySinceHeartbeat = True
    End Sub

    Private Sub _timer_Tick(sender As Object, e As EventArgs) Handles _timer.Tick
        If Not _isActive Then Return

        Dim remaining As TimeSpan = TimeSpan.FromMinutes(TimeoutMinutes) - (Date.Now - _lastActivity)

        If remaining <= TimeSpan.Zero Then
            EndSession()
            ReturnToLogin("You were logged out after " & TimeoutMinutes & " minutes of inactivity.")
            Return
        End If

        If _activitySinceHeartbeat AndAlso (Date.Now - _lastHeartbeat).TotalSeconds >= HeartbeatSeconds Then
            SendHeartbeat()
        End If

        RaiseEvent SessionTick(remaining)
    End Sub

    Private Sub SendHeartbeat()
        _lastHeartbeat = Date.Now
        _activitySinceHeartbeat = False
        Try
            Using conn As MySqlConnection = DBConnection.GetLoginConnection()
                conn.Open()
                Using cmd As New MySqlCommand(
                    "UPDATE UserSessions SET last_activity = NOW() WHERE session_id = @id", conn)
                    cmd.Parameters.AddWithValue("@id", SessionId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            ApplicationLogger.LogError("Session operation failed: " & ex.Message, ex)
        End Try
    End Sub

    Public Sub ReturnToLogin(message As String)
        Dim login As LoginForm = Nothing

        For Each f As Form In Application.OpenForms.Cast(Of Form)().ToList()
            If TypeOf f Is LoginForm Then
                login = DirectCast(f, LoginForm)
            Else
                f.Close()
            End If
        Next

        If login IsNot Nothing Then login.ShowForNewLogin(message)
    End Sub

    ' ---------- Status bar ----------
    Public Sub AttachStatusLabel(label As ToolStripStatusLabel, owner As Form)
        Dim handler As SessionTickHandler =
            Sub(remaining As TimeSpan)
                If Not label.IsDisposed Then UpdateStatusLabel(label, remaining)
            End Sub

        AddHandler SessionTick, handler
        AddHandler owner.FormClosed, Sub(s, e) RemoveHandler SessionTick, handler

        UpdateStatusLabel(label, TimeSpan.FromMinutes(TimeoutMinutes))
    End Sub

    Private Sub UpdateStatusLabel(label As ToolStripStatusLabel, remaining As TimeSpan)
        Dim text As String = Username & " (" & RoleName() & ")  |  Logged in " &
                             LoginTime.ToString("HH:mm:ss") & "  |  Session #" & SessionId &
                             "  |  Auto-logout in " & remaining.ToString("mm\:ss")

        If remaining.TotalSeconds <= WarningSeconds Then
            label.Text = "WARNING: session expires in " & remaining.ToString("mm\:ss") &
                         ". Move the mouse to stay logged in.  |  " & text
            label.ForeColor = Color.Firebrick
        Else
            label.Text = text
            label.ForeColor = SystemColors.ControlText
        End If
    End Sub

    Private Function DetectLocalIp() As String
        Try
            For Each address As IPAddress In Dns.GetHostAddresses(Dns.GetHostName())
                If address.AddressFamily = AddressFamily.InterNetwork Then Return address.ToString()
            Next
        Catch ex As Exception
            ApplicationLogger.LogError("Session operation failed: " & ex.Message, ex)
        End Try
        Return "127.0.0.1"
    End Function

End Module

' Counts keyboard and mouse input anywhere in the application as activity
Friend Class ActivityFilter
    Implements IMessageFilter

    Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
        Select Case m.Msg
            Case &H100, &H101, &H200, &H201, &H204, &H207, &H20A
                SessionManager.RegisterActivity()
        End Select
        Return False
    End Function

End Class