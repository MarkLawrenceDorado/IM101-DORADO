Imports MySql.Data.MySqlClient
Imports System.Data

Public Class DBConnection

    Private Const Host As String = "127.0.0.1"
    Private Const Port As UInteger = 3306UI
    Private Const DatabaseName As String = "DB_Dorado"

    Public Shared Property CurrentUserId As Integer = 0
    Public Shared Property CurrentRoleId As Integer = 0
    Public Shared Property CurrentIp As String = "127.0.0.1"

    Private Shared Function AccountForRole(roleId As Integer) As String
        Select Case roleId
            Case 1
                Return "academic_admin"
            Case 2
                Return "academic_teacher"
            Case 3
                Return "academic_student"
            Case Else
                Return "academic_login"
        End Select
    End Function

    Private Shared Function BuildConnectionString(account As String) As String
        Dim builder As New MySqlConnectionStringBuilder()
        builder.Server = Host
        builder.Port = Port
        builder.Database = DatabaseName
        builder.UserID = account
        builder.Password = CredentialStore.GetPassword(account)

        builder.Pooling = True
        builder.MinimumPoolSize = 1
        builder.MaximumPoolSize = 20
        builder.ConnectionLifeTime = 300
        builder.ConnectionTimeout = 15
        builder.DefaultCommandTimeout = 30

        builder.AllowUserVariables = True
        Return builder.ConnectionString
    End Function

    ' Connection for the logged-in user's role (admin / teacher / student)
    Public Shared Function GetConnection() As MySqlConnection
        Dim conn As New MySqlConnection(BuildConnectionString(AccountForRole(CurrentRoleId)))
        AddHandler conn.StateChange, AddressOf OnStateChange
        Return conn
    End Function

    ' Connection for logging in and for session tracking
    Public Shared Function GetLoginConnection() As MySqlConnection
        Return New MySqlConnection(BuildConnectionString("academic_login"))
    End Function

    ' Tells the audit triggers who is making the change
    Private Shared Sub OnStateChange(sender As Object, e As StateChangeEventArgs)
        If e.CurrentState <> ConnectionState.Open Then Return
        If CurrentUserId <= 0 Then Return

        Dim conn As MySqlConnection = DirectCast(sender, MySqlConnection)
        Using cmd As New MySqlCommand("SET @current_user_id = @uid, @current_user_ip = @ip", conn)
            cmd.Parameters.AddWithValue("@uid", CurrentUserId)
            cmd.Parameters.AddWithValue("@ip", CurrentIp)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

End Class