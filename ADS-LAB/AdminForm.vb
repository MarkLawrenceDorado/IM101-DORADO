Imports MySql.Data.MySqlClient
Imports BCrypt.Net
Imports System.Data

Public Class AdminForm

    Private ReadOnly _adminUserId As Integer

    Public Sub New(adminUserId As Integer)
        InitializeComponent()
        _adminUserId = adminUserId
        LoadAllLists()
    End Sub

    ' =========================================================
    '  ADD TEACHER (Users row -> Instructors row, in one transaction)
    ' =========================================================
    Private Sub btnAddTeacher_Click(sender As Object, e As EventArgs) Handles btnAddTeacher.Click
        Dim username As String = txtNewUsername.Text.Trim()
        Dim password As String = txtNewPassword.Text
        Dim firstName As String = txtFirstName.Text.Trim()
        Dim lastName As String = txtLastName.Text.Trim()
        Dim department As String = txtDepartment.Text.Trim()

        If username = "" OrElse password = "" OrElse firstName = "" OrElse lastName = "" Then
            MessageBox.Show("Please fill in username, password, first name, and last name.", "Missing Info")
            Return
        End If

        Dim conn As MySqlConnection = DBConnection.GetConnection()

        Try
            conn.Open()
            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try
                ' STEP 1: Create the Users row (role_id = 2 = Teacher)
                Dim hashedPassword As String = BCrypt.Net.BCrypt.HashPassword(password)

                Dim insertUserQuery As String =
                    "INSERT INTO Users (username, password_hash, role_id) VALUES (@username, @hash, 2)"
                Dim userCmd As New MySqlCommand(insertUserQuery, conn, transaction)
                userCmd.Parameters.AddWithValue("@username", username)
                userCmd.Parameters.AddWithValue("@hash", hashedPassword)
                userCmd.ExecuteNonQuery()

                Dim newUserId As Long = userCmd.LastInsertedId

                ' STEP 2: Create the Instructors row, linked via that user_id
                Dim insertInstructorQuery As String =
                    "INSERT INTO Instructors (user_id, first_name, last_name, department) " &
                    "VALUES (@userId, @firstName, @lastName, @department)"
                Dim instructorCmd As New MySqlCommand(insertInstructorQuery, conn, transaction)
                instructorCmd.Parameters.AddWithValue("@userId", newUserId)
                instructorCmd.Parameters.AddWithValue("@firstName", firstName)
                instructorCmd.Parameters.AddWithValue("@lastName", lastName)
                instructorCmd.Parameters.AddWithValue("@department", department)
                instructorCmd.ExecuteNonQuery()

                transaction.Commit()

                lblStatus.Text = "Teacher '" & firstName & " " & lastName & "' added successfully."
                lblStatus.ForeColor = Color.Green

                txtNewUsername.Clear()
                txtNewPassword.Clear()
                txtFirstName.Clear()
                txtLastName.Clear()
                txtDepartment.Clear()

                ' Refresh the grids
                LoadAllLists()

            Catch ex As Exception
                transaction.Rollback()
                lblStatus.Text = "Failed to add teacher: " & ex.Message
                lblStatus.ForeColor = Color.Red
            End Try

        Catch ex As Exception
            MessageBox.Show("Database connection error: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    ' =========================================================
    '  VIEW LISTS: Users, Instructors, Students
    ' =========================================================
    Private Sub LoadAllLists()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            ' Users (password_hash intentionally excluded — never display it)
            Dim usersAdapter As New MySqlDataAdapter(
                "SELECT user_id, username, role_id FROM Users", conn)
            Dim usersTable As New DataTable()
            usersAdapter.Fill(usersTable)
            dgvUsers.DataSource = usersTable

            ' Instructors
            Dim instructorsAdapter As New MySqlDataAdapter(
                "SELECT instructor_id, user_id, first_name, last_name, department FROM Instructors", conn)
            Dim instructorsTable As New DataTable()
            instructorsAdapter.Fill(instructorsTable)
            dgvInstructors.DataSource = instructorsTable

            ' Students
            Dim studentsAdapter As New MySqlDataAdapter(
                "SELECT student_id, user_id, first_name, last_name, date_of_birth FROM Students", conn)
            Dim studentsTable As New DataTable()
            studentsAdapter.Fill(studentsTable)
            dgvStudents.DataSource = studentsTable

        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadAllLists()
    End Sub

    ' =========================================================
    '  RESET USER PASSWORD (securely)
    ' =========================================================
    Private Sub btnResetPassword_Click(sender As Object, e As EventArgs) Handles btnResetPassword.Click
        Dim targetUsername As String = txtTargetUsername.Text.Trim()
        Dim newPassword As String = txtNewResetPassword.Text

        If targetUsername = "" OrElse newPassword = "" Then
            MessageBox.Show("Select a user and enter a new password.", "Missing Info")
            Return
        End If

        If newPassword.Length < 8 Then
            MessageBox.Show("Password must be at least 8 characters.", "Weak Password")
            Return
        End If

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            ' Hash before it ever touches the database — never store plain text
            Dim newHash As String = BCrypt.Net.BCrypt.HashPassword(newPassword)

            Dim query As String = "UPDATE Users SET password_hash = @hash WHERE username = @username"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@hash", newHash)
            cmd.Parameters.AddWithValue("@username", targetUsername)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MessageBox.Show("Password reset successfully for '" & targetUsername & "'.", "Success")
                txtTargetUsername.Clear()
                txtNewResetPassword.Clear()
            Else
                MessageBox.Show("User not found.", "Error")
            End If

        Catch ex As Exception
            MessageBox.Show("Error resetting password: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

End Class