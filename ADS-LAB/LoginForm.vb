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

        Dim conn As MySqlConnection = DBConnection.GetConnection()

        Try
            conn.Open()

            Dim query As String = "SELECT user_id, password_hash, role_id FROM Users WHERE username = @username"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@username", username)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim userId As Integer = reader.GetInt32("user_id")
                Dim storedHash As String = reader.GetString("password_hash")
                Dim roleId As Integer = reader.GetInt32("role_id")

                reader.Close()

                ' Compare plain-text input against the stored bcrypt hash
                If BCrypt.Net.BCrypt.Verify(password, storedHash) Then
                    MessageBox.Show("Login successful!", "Welcome")
                    RedirectByRole(roleId, userId)
                    Me.Hide()
                Else
                    MessageBox.Show("Incorrect username or password.", "Login Failed")
                End If
            Else
                reader.Close()
                MessageBox.Show("Incorrect username or password.", "Login Failed")
            End If

        Catch ex As Exception
            MessageBox.Show("Database error: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub RedirectByRole(roleId As Integer, userId As Integer)
        Select Case roleId
            Case 1 ' Admin
                MessageBox.Show("Login OK — Admin (user_id=" & userId & ")")
                Dim AdminForm As New AdminForm(userId)
                AdminForm.Show()
            Case 2 ' Teacher
                MessageBox.Show("Login OK — Teacher (user_id=" & userId & ")")
                'Dim teacherForm As New TeacherForm(userId)
                'teacherForm.Show()
            Case 3 ' Student
                MessageBox.Show("Login OK — Student (user_id=" & userId & ")")
                'Dim studentForm As New StudentForm(userId)
                'studentForm.Show()
            Case Else
                MessageBox.Show("Unrecognized role.", "Error")
        End Select
    End Sub

End Class