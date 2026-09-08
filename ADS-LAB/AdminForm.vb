Imports MySql.Data.MySqlClient
Imports BCrypt.Net
Imports System.Data

Public Class AdminForm

    Private ReadOnly _adminUserId As Integer

    Public Sub New(adminUserId As Integer)
        InitializeComponent()
        _adminUserId = adminUserId

        ' Populate the Teacher/Student choice
        cmbUserType.Items.Clear()
        cmbUserType.Items.AddRange({"Teacher", "Student"})
        cmbUserType.SelectedIndex = 0 ' default to Teacher, shows Department field

        LoadAllLists()
        LoadAssignmentTypes()
        LoadAllGrades()
    End Sub

    ' =========================================================
    '  ADD TEACHER OR STUDENT (Users row -> Instructors/Students row)
    '  cmbUserType decides which branch runs. Both use a transaction
    '  so the Users row and the profile row are never left mismatched.
    ' =========================================================
    Private Sub cmbUserType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbUserType.SelectedIndexChanged
        Dim isStudent As Boolean = (cmbUserType.Text = "Student")
        lblDepartment.Visible = Not isStudent
        txtDepartment.Visible = Not isStudent
        lblDateOfBirth.Visible = isStudent
        dtpDateOfBirth.Visible = isStudent
        lblEmail.Visible = isStudent
        txtEmail.Visible = isStudent
    End Sub

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
        Dim userType As String = cmbUserType.Text ' "Teacher" or "Student"
        Dim username As String = txtNewUsername.Text.Trim()
        Dim password As String = txtNewPassword.Text
        Dim firstName As String = txtFirstName.Text.Trim()
        Dim lastName As String = txtLastName.Text.Trim()

        If userType = "" Then
            MessageBox.Show("Select whether you're adding a Teacher or a Student.", "Missing Info")
            Return
        End If

        If username = "" OrElse password = "" OrElse firstName = "" OrElse lastName = "" Then
            MessageBox.Show("Please fill in username, password, first name, and last name.", "Missing Info")
            Return
        End If

        Dim conn As MySqlConnection = DBConnection.GetConnection()

        Try
            conn.Open()
            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try
                Dim hashedPassword As String = BCrypt.Net.BCrypt.HashPassword(password)
                Dim roleId As Integer = If(userType = "Teacher", 2, 3) ' 2 = Teacher, 3 = Student

                ' STEP 1: Create the Users row
                Dim insertUserQuery As String =
                    "INSERT INTO Users (username, password_hash, role_id) VALUES (@username, @hash, @roleId)"
                Dim userCmd As New MySqlCommand(insertUserQuery, conn, transaction)
                userCmd.Parameters.AddWithValue("@username", username)
                userCmd.Parameters.AddWithValue("@hash", hashedPassword)
                userCmd.Parameters.AddWithValue("@roleId", roleId)
                userCmd.ExecuteNonQuery()

                Dim newUserId As Long = userCmd.LastInsertedId

                ' STEP 2: Create the matching profile row
                If userType = "Teacher" Then
                    Dim department As String = txtDepartment.Text.Trim()

                    Dim insertInstructorQuery As String =
                        "INSERT INTO Instructors (user_id, first_name, last_name, department) " &
                        "VALUES (@userId, @firstName, @lastName, @department)"
                    Dim instructorCmd As New MySqlCommand(insertInstructorQuery, conn, transaction)
                    instructorCmd.Parameters.AddWithValue("@userId", newUserId)
                    instructorCmd.Parameters.AddWithValue("@firstName", firstName)
                    instructorCmd.Parameters.AddWithValue("@lastName", lastName)
                    instructorCmd.Parameters.AddWithValue("@department", department)
                    instructorCmd.ExecuteNonQuery()

                Else ' Student
                    Dim dob As Date = dtpDateOfBirth.Value.Date
                    Dim email As String = txtEmail.Text.Trim()

                    If email = "" Then
                        Throw New Exception("Email is required for a student.")
                    End If

                    Dim insertStudentQuery As String =
                        "INSERT INTO Students (user_id, first_name, last_name, date_of_birth, email) " &
                        "VALUES (@userId, @firstName, @lastName, @dob, @email)"
                    Dim studentCmd As New MySqlCommand(insertStudentQuery, conn, transaction)
                    studentCmd.Parameters.AddWithValue("@userId", newUserId)
                    studentCmd.Parameters.AddWithValue("@firstName", firstName)
                    studentCmd.Parameters.AddWithValue("@lastName", lastName)
                    studentCmd.Parameters.AddWithValue("@dob", dob)
                    studentCmd.Parameters.AddWithValue("@email", email)
                    studentCmd.ExecuteNonQuery()
                End If

                transaction.Commit()

                lblStatus.Text = userType & " '" & firstName & " " & lastName & "' added successfully."
                lblStatus.ForeColor = Color.Green

                txtNewUsername.Clear()
                txtNewPassword.Clear()
                txtFirstName.Clear()
                txtLastName.Clear()
                txtDepartment.Clear()
                txtEmail.Clear()

                ' Refresh the grids
                LoadAllLists()

            Catch ex As Exception
                transaction.Rollback()
                lblStatus.Text = "Failed to add " & userType.ToLower() & ": " & ex.Message
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

    ' =========================================================
    '  ASSIGNMENT TYPES — CRUD (Add / Edit / Delete)
    ' =========================================================
    Private Sub LoadAssignmentTypes()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter(
                "SELECT type_id, type_name, default_weight FROM AssignmentTypes ORDER BY type_name", conn)
            Dim table As New DataTable()
            adapter.Fill(table)
            dgvAssignmentTypes.DataSource = table

        Catch ex As Exception
            MessageBox.Show("Error loading assignment types: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnAddType_Click(sender As Object, e As EventArgs) Handles btnAddType.Click
        Dim typeName As String = txtTypeName.Text.Trim()
        Dim weightText As String = txtDefaultWeight.Text.Trim()
        Dim weightValue As Decimal

        If typeName = "" OrElse Not Decimal.TryParse(weightText, weightValue) Then
            MessageBox.Show("Enter a valid type name and numeric default weight.", "Missing/Invalid Info")
            Return
        End If

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim query As String = "INSERT INTO AssignmentTypes (type_name, default_weight) VALUES (@name, @weight)"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@name", typeName)
            cmd.Parameters.AddWithValue("@weight", weightValue)
            cmd.ExecuteNonQuery()

            txtTypeName.Clear()
            txtDefaultWeight.Clear()
            LoadAssignmentTypes()

        Catch ex As Exception
            MessageBox.Show("Error adding assignment type: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnUpdateType_Click(sender As Object, e As EventArgs) Handles btnUpdateType.Click
        If dgvAssignmentTypes.SelectedRows.Count = 0 Then
            MessageBox.Show("Select a row in the grid to update.", "No Selection")
            Return
        End If

        Dim typeId As Integer = Convert.ToInt32(dgvAssignmentTypes.SelectedRows(0).Cells("type_id").Value)
        Dim typeName As String = txtTypeName.Text.Trim()
        Dim weightText As String = txtDefaultWeight.Text.Trim()
        Dim weightValue As Decimal

        If typeName = "" OrElse Not Decimal.TryParse(weightText, weightValue) Then
            MessageBox.Show("Enter a valid type name and numeric default weight.", "Missing/Invalid Info")
            Return
        End If

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim query As String =
                "UPDATE AssignmentTypes SET type_name = @name, default_weight = @weight WHERE type_id = @id"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@name", typeName)
            cmd.Parameters.AddWithValue("@weight", weightValue)
            cmd.Parameters.AddWithValue("@id", typeId)
            cmd.ExecuteNonQuery()

            txtTypeName.Clear()
            txtDefaultWeight.Clear()
            LoadAssignmentTypes()

        Catch ex As Exception
            MessageBox.Show("Error updating assignment type: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnDeleteType_Click(sender As Object, e As EventArgs) Handles btnDeleteType.Click
        If dgvAssignmentTypes.SelectedRows.Count = 0 Then
            MessageBox.Show("Select a row in the grid to delete.", "No Selection")
            Return
        End If

        Dim typeId As Integer = Convert.ToInt32(dgvAssignmentTypes.SelectedRows(0).Cells("type_id").Value)
        Dim typeName As String = dgvAssignmentTypes.SelectedRows(0).Cells("type_name").Value.ToString()

        Dim confirm As DialogResult = MessageBox.Show(
            "Delete assignment type '" & typeName & "'? This cannot be undone.",
            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If confirm <> DialogResult.Yes Then Return

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim query As String = "DELETE FROM AssignmentTypes WHERE type_id = @id"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@id", typeId)
            cmd.ExecuteNonQuery()

            LoadAssignmentTypes()

        Catch ex As Exception
            ' Will fail if Grades rows still reference this type_id (FK constraint) — by design.
            MessageBox.Show("Cannot delete: this type is likely still used in existing grades. " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub dgvAssignmentTypes_SelectionChanged(sender As Object, e As EventArgs) Handles dgvAssignmentTypes.SelectionChanged
        ' Populate the textboxes with the selected row, so Update/Delete act on it
        If dgvAssignmentTypes.SelectedRows.Count > 0 Then
            txtTypeName.Text = dgvAssignmentTypes.SelectedRows(0).Cells("type_name").Value.ToString()
            txtDefaultWeight.Text = dgvAssignmentTypes.SelectedRows(0).Cells("default_weight").Value.ToString()
        End If
    End Sub

    ' =========================================================
    '  ALL GRADES (read-only, system-wide)
    ' =========================================================
    Private Sub LoadAllGrades()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim query As String =
                "SELECT stu.first_name AS student_first, stu.last_name AS student_last, " &
                "       c.course_code, c.course_name, at.type_name, g.grade, g.date_recorded " &
                "FROM Grades g " &
                "INNER JOIN Students stu ON g.student_id = stu.student_id " &
                "INNER JOIN Courses c ON g.course_id = c.course_id " &
                "INNER JOIN AssignmentTypes at ON g.type_id = at.type_id " &
                "ORDER BY g.date_recorded DESC"

            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim table As New DataTable()
            adapter.Fill(table)
            dgvAllGrades.DataSource = table
            dgvAllGrades.ReadOnly = True

        Catch ex As Exception
            MessageBox.Show("Error loading grades: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    ' =========================================================
    '  CUSTOM TAB STYLING (unchanged from your version)
    ' =========================================================
    Private Sub tabsController_DrawItem(sender As Object, e As DrawItemEventArgs) _
    Handles tabsController.DrawItem

        Dim tab As TabControl = DirectCast(sender, TabControl)

        Dim tabPage As TabPage = tab.TabPages(e.Index)

        ' Tab rectangle
        Dim rect As Rectangle = e.Bounds

        ' Background
        If e.Index = tab.SelectedIndex Then
            e.Graphics.FillRectangle(Brushes.White, rect)
        Else
            e.Graphics.FillRectangle(
            New SolidBrush(Color.FromArgb(245, 247, 250)),
            rect
        )
        End If

        ' Blue selected indicator
        If e.Index = tab.SelectedIndex Then
            Using indicator As New SolidBrush(Color.FromArgb(0, 120, 215))
                e.Graphics.FillRectangle(
                indicator,
                rect.X,
                rect.Y,
                4,
                rect.Height
            )
            End Using
        End If

        ' Text
        Dim textRect As New Rectangle(
        rect.X + 15,
        rect.Y,
        rect.Width - 20,
        rect.Height
    )

        Dim textColor As Color

        If e.Index = tab.SelectedIndex Then
            textColor = Color.FromArgb(30, 30, 30)
        Else
            textColor = Color.FromArgb(90, 90, 90)
        End If

        Using font As New Font(
        "Segoe UI",
        10,
        If(e.Index = tab.SelectedIndex,
           FontStyle.Bold,
           FontStyle.Regular)
    )

            Using brush As New SolidBrush(textColor)

                Dim format As New StringFormat()

                format.Alignment = StringAlignment.Near
                format.LineAlignment = StringAlignment.Center

                e.Graphics.DrawString(
                tabPage.Text,
                font,
                brush,
                textRect,
                format
            )

            End Using

        End Using

    End Sub

End Class