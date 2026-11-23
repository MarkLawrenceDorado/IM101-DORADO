Imports MySql.Data.MySqlClient
Imports BCrypt.Net

Public Class LoginForm

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text

        If username = "" OrElse password = "" Then
            MessageBox.Show("Please enter both username and password.", "Login Failed")
            Return
        End If

        Dim found As Boolean = False
        Dim userId As Integer
        Dim roleId As Integer
        Dim storedHash As String = ""

        Dim conn As MySqlConnection = DBConnection.GetLoginConnection()
        Try
            conn.Open()
            Using cmd As New MySqlCommand(
                "SELECT user_id, password_hash, role_id FROM Users WHERE username = @username", conn)
                cmd.Parameters.AddWithValue("@username", username)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        found = True
                        userId = reader.GetInt32("user_id")
                        storedHash = reader.GetString("password_hash")
                        roleId = reader.GetInt32("role_id")
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Database error: " & ex.Message, "Error")
            Return
        Finally
            conn.Close()
        End Try

        If Not found OrElse Not BCrypt.Net.BCrypt.Verify(password, storedHash) Then
            MessageBox.Show("Incorrect username or password.", "Login Failed")
            Return
        End If

        Dim message As String = ""
        If Not SessionManager.TryStartSession(userId, roleId, username, message) Then
            MessageBox.Show(message, "Login Failed")
            Return
        End If

        If OpenDashboard(roleId, userId) Then
            Me.Hide()
        End If
    End Sub

    Private Function OpenDashboard(roleId As Integer, userId As Integer) As Boolean
        If roleId < 1 OrElse roleId > 3 Then
            SessionManager.EndSession()
            MessageBox.Show("Unrecognized role.", "Error")
            Return False
        End If

        Dim parent As New MainMDIParent(userId)
        AddHandler parent.FormClosed, AddressOf DashboardClosed
        parent.Show()
        Return True
    End Function


    ' Closing a dashboard counts as logging out
    Private Sub DashboardClosed(sender As Object, e As FormClosedEventArgs)
        If SessionManager.IsActive Then
            SessionManager.EndSession()
            ShowForNewLogin("")
        End If
    End Sub

    Public Sub ShowForNewLogin(message As String)
        txtUsername.Clear()
        txtPassword.Clear()
        Me.Show()
        txtUsername.Focus()

        If message <> "" Then
            MessageBox.Show(message, "Session Ended", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

End Class