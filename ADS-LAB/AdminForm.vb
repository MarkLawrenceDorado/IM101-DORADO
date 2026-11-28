Imports MySql.Data.MySqlClient
Imports BCrypt.Net
Imports System.Data

Public Class AdminForm

    Private ReadOnly _adminUserId As Integer

    Public Sub New(adminUserId As Integer)
        InitializeComponent()
        _adminUserId = adminUserId
        SessionManager.AttachStatusLabel(lblSession, Me)

        ' Populate the Teacher/Student choice
        cmbUserType.Items.Clear()
        cmbUserType.Items.AddRange({"Teacher", "Student"})
        cmbUserType.SelectedIndex = 0 ' default to Teacher, shows Department field

        LoadAllLists()
        LoadEnrollmentLists()
        LoadAssignmentTypes()
        LoadAllGrades()
        LoadCoursesAndInstructors()
        AttachAutoComplete(txtGradeStudent, "SELECT CONCAT(student_id, ' - ', first_name, ' ', last_name) FROM Students")
        AttachAutoComplete(txtGradeCourse, "SELECT CONCAT(course_code, ' - ', course_name) FROM Courses")
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
        errProvider.SetError(cmbUserType, "")
    End Sub

    Private Function ValidateRegistration() As Boolean
        errProvider.Clear()
        Dim isStudent As Boolean = (cmbUserType.Text = "Student")
        Dim valid As Boolean = True

        valid = Check(errProvider, cmbUserType, cmbUserType.Text = "", "Choose Teacher or Student.") And valid

        Dim username As String = txtNewUsername.Text.Trim()
        valid = Check(errProvider, txtNewUsername, Not IsValidUsername(username),
                      "3-30 characters: letters, numbers, dot or underscore.") And valid
        If IsValidUsername(username) Then
            valid = Check(errProvider, txtNewUsername,
                          ValueExists("SELECT 1 FROM Users WHERE username = @u LIMIT 1", New MySqlParameter("@u", username)),
                          "That username is already taken.") And valid
        End If

        Dim passwordMessage As String = PasswordProblem(txtNewPassword.Text)
        valid = Check(errProvider, txtNewPassword, passwordMessage <> "", passwordMessage) And valid

        valid = Check(errProvider, txtFirstName, Not IsValidName(txtFirstName.Text.Trim()),
                      "Enter a first name (letters only, 50 characters maximum).") And valid
        valid = Check(errProvider, txtLastName, Not IsValidName(txtLastName.Text.Trim()),
                      "Enter a last name (letters only, 50 characters maximum).") And valid

        valid = Check(errProvider, txtDepartment, Not isStudent AndAlso txtDepartment.Text.Trim().Length > 50,
                      "Department must be 50 characters or fewer.") And valid

        If isStudent Then
            Dim email As String = txtEmail.Text.Trim()
            valid = Check(errProvider, txtEmail, Not IsValidEmail(email), "Enter a valid email address.") And valid
            If IsValidEmail(email) Then
                valid = Check(errProvider, txtEmail,
                              ValueExists("SELECT 1 FROM Students WHERE email = @e LIMIT 1", New MySqlParameter("@e", email)),
                              "That email is already registered.") And valid
            End If

            valid = Check(errProvider, dtpDateOfBirth,
                          dtpDateOfBirth.Value.Date > Date.Today OrElse dtpDateOfBirth.Value.Year < 1900,
                          "Enter a valid date of birth.") And valid
        End If

        Return valid
    End Function

    Private Sub ClearFieldError(sender As Object, e As EventArgs) _
        Handles txtNewUsername.TextChanged, txtNewPassword.TextChanged, txtFirstName.TextChanged,
                txtLastName.TextChanged, txtDepartment.TextChanged, txtEmail.TextChanged,
                dtpDateOfBirth.ValueChanged
        errProvider.SetError(DirectCast(sender, Control), "")
    End Sub

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
        Dim userType As String = cmbUserType.Text ' "Teacher" or "Student"
        Dim username As String = txtNewUsername.Text.Trim()
        Dim password As String = txtNewPassword.Text
        Dim firstName As String = txtFirstName.Text.Trim()
        Dim lastName As String = txtLastName.Text.Trim()

        If Not ValidateRegistration() Then Return

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
                errProvider.Clear()

                ' Refresh the grids and related controls
                LoadAllLists()
                If userType = "Student" Then
                    LoadEnrollmentLists()
                ElseIf userType = "Teacher" Then
                    LoadCoursesAndInstructors()
                End If

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
    Friend Sub LoadAllLists()
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
        LoadEnrollmentLists()
        LoadCoursesAndInstructors()
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

        Dim problem As String = PasswordProblem(newPassword)
        If problem <> "" Then
            MessageBox.Show(problem, "Weak Password")
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
    Friend Sub LoadAssignmentTypes()
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

        If weightValue < 0D OrElse weightValue > 100D Then
            MessageBox.Show("Each assignment weight must be between 0 and 100.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            ' Check total default_weight across existing AssignmentTypes + this new one
            Dim existingTotal As Decimal = 0D
            Using sumCmd As New MySqlCommand("SELECT COALESCE(SUM(default_weight), 0) FROM AssignmentTypes", conn)
                existingTotal = Convert.ToDecimal(sumCmd.ExecuteScalar())
            End Using

            Dim totalWeight As Decimal = existingTotal + weightValue
            If Math.Abs(totalWeight - 100D) > 0.01D Then
                MessageBox.Show("Total assignment weights must equal 100% (current total with this entry: " & totalWeight.ToString("0.##") & "%).",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

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
    Friend Sub LoadAllGrades()
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
    '  CUSTOM TAB STYLING
    ' =========================================================
    Private Sub tabsController_DrawItem(sender As Object, e As DrawItemEventArgs) _
    Handles tabsController.DrawItem

        Dim tab As TabControl = DirectCast(sender, TabControl)
        Dim tabPage As TabPage = tab.TabPages(e.Index)
        Dim rect As Rectangle = e.Bounds

        If e.Index = tab.SelectedIndex Then
            e.Graphics.FillRectangle(Brushes.White, rect)
        Else
            e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(245, 247, 250)), rect)
        End If

        If e.Index = tab.SelectedIndex Then
            Using indicator As New SolidBrush(Color.FromArgb(0, 120, 215))
                e.Graphics.FillRectangle(indicator, rect.X, rect.Y, 4, rect.Height)
            End Using
        End If

        Dim textRect As New Rectangle(rect.X + 15, rect.Y, rect.Width - 20, rect.Height)
        Dim textColor As Color = If(e.Index = tab.SelectedIndex, Color.FromArgb(30, 30, 30), Color.FromArgb(90, 90, 90))

        Using font As New Font("Segoe UI", 10, If(e.Index = tab.SelectedIndex, FontStyle.Bold, FontStyle.Regular))
            Using brush As New SolidBrush(textColor)
                Dim format As New StringFormat() With {
                    .Alignment = StringAlignment.Near,
                    .LineAlignment = StringAlignment.Center
                }
                e.Graphics.DrawString(tabPage.Text, font, brush, textRect, format)
            End Using
        End Using
    End Sub

    Friend Sub LoadEnrollmentLists()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            Dim studentsTable As New DataTable()
            Dim studentsAdapter As New MySqlDataAdapter(
                "SELECT student_id, CONCAT(first_name, ' ', last_name) AS full_name " &
                "FROM Students ORDER BY last_name, first_name", conn)
            studentsAdapter.Fill(studentsTable)

            Dim coursesTable As New DataTable()
            Dim coursesAdapter As New MySqlDataAdapter(
                "SELECT course_id, CONCAT(course_code, ' - ', course_name) AS course_label " &
                "FROM Courses ORDER BY course_code", conn)
            coursesAdapter.Fill(coursesTable)

            cmbEnrollStudent.DataSource = studentsTable
            cmbEnrollStudent.DisplayMember = "full_name"
            cmbEnrollStudent.ValueMember = "student_id"

            cmbEnrollCourse.DataSource = coursesTable
            cmbEnrollCourse.DisplayMember = "course_label"
            cmbEnrollCourse.ValueMember = "course_id"

            clbBulkStudents.DataSource = studentsTable.Copy()
            clbBulkStudents.DisplayMember = "full_name"
            clbBulkStudents.ValueMember = "student_id"

            cmbBulkCourse.DataSource = coursesTable.Copy()
            cmbBulkCourse.DisplayMember = "course_label"
            cmbBulkCourse.ValueMember = "course_id"

        Catch ex As Exception
            MessageBox.Show("Error loading enrollment lists: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    ' ---------- Single enrollment: calls sp_EnrollStudent ----------
    Private Sub btnEnrollStudent_Click(sender As Object, e As EventArgs) Handles btnEnrollStudent.Click
        If cmbEnrollStudent.SelectedValue Is Nothing OrElse cmbEnrollCourse.SelectedValue Is Nothing Then
            MessageBox.Show("Select a student and a course.", "Missing Info")
            Return
        End If

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            Using cmd As New MySqlCommand("sp_EnrollStudent", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@p_student_id", Convert.ToInt32(cmbEnrollStudent.SelectedValue))
                cmd.Parameters.AddWithValue("@p_course_id", Convert.ToInt32(cmbEnrollCourse.SelectedValue))

                Dim resultParam As New MySqlParameter("@p_result", MySqlDbType.VarChar, 255)
                resultParam.Direction = ParameterDirection.Output
                cmd.Parameters.Add(resultParam)

                cmd.ExecuteNonQuery()

                Dim message As String = Convert.ToString(resultParam.Value)
                lblEnrollStatus.Text = message
                lblEnrollStatus.ForeColor = If(message.StartsWith("Success"), Color.Green, Color.Red)
            End Using

        Catch ex As Exception
            lblEnrollStatus.Text = "Enrollment error: " & ex.Message
            lblEnrollStatus.ForeColor = Color.Red
        Finally
            conn.Close()
        End Try
    End Sub

    ' ---------- Bulk enrollment: one transaction, all-or-nothing ----------
    Private Sub btnBulkEnroll_Click(sender As Object, e As EventArgs) Handles btnBulkEnroll.Click
        If clbBulkStudents.CheckedItems.Count = 0 OrElse cmbBulkCourse.SelectedValue Is Nothing Then
            MessageBox.Show("Check at least one student and select a course.", "Missing Info")
            Return
        End If

        Dim courseId As Integer = Convert.ToInt32(cmbBulkCourse.SelectedValue)
        Dim enrolled As Integer = 0
        Dim skipped As Integer = 0

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try
                For Each item As DataRowView In clbBulkStudents.CheckedItems
                    Dim studentId As Integer = Convert.ToInt32(item("student_id"))

                    Using checkCmd As New MySqlCommand(
                        "SELECT COUNT(*) FROM Enrollments WHERE student_id = @s AND course_id = @c", conn, transaction)
                        checkCmd.Parameters.AddWithValue("@s", studentId)
                        checkCmd.Parameters.AddWithValue("@c", courseId)

                        If Convert.ToInt32(checkCmd.ExecuteScalar()) > 0 Then
                            skipped += 1
                            Continue For
                        End If
                    End Using

                    Using insertCmd As New MySqlCommand(
                        "INSERT INTO Enrollments (student_id, course_id, enrollment_date) VALUES (@s, @c, NOW())",
                        conn, transaction)
                        insertCmd.Parameters.AddWithValue("@s", studentId)
                        insertCmd.Parameters.AddWithValue("@c", courseId)
                        insertCmd.ExecuteNonQuery()
                        enrolled += 1
                    End Using
                Next

                transaction.Commit()

                lblEnrollStatus.Text = "Bulk enrollment complete: " & enrolled & " enrolled, " & skipped & " already enrolled."
                lblEnrollStatus.ForeColor = Color.Green

                For i As Integer = 0 To clbBulkStudents.Items.Count - 1
                    clbBulkStudents.SetItemChecked(i, False)
                Next

            Catch ex As Exception
                transaction.Rollback()
                lblEnrollStatus.Text = "Bulk enrollment failed, nothing was saved: " & ex.Message
                lblEnrollStatus.ForeColor = Color.Red
            End Try

        Catch ex As Exception
            MessageBox.Show("Database connection error: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub txtSearchUsers_TextChanged(sender As Object, e As EventArgs) Handles txtSearchUsers.TextChanged
        FilterGrid(dgvUsers, txtSearchUsers.Text)
    End Sub

    Private Sub txtSearchInstructors_TextChanged(sender As Object, e As EventArgs) Handles txtSearchInstructors.TextChanged
        FilterGrid(dgvInstructors, txtSearchInstructors.Text)
    End Sub

    Private Sub txtSearchStudents_TextChanged(sender As Object, e As EventArgs) Handles txtSearchStudents.TextChanged
        FilterGrid(dgvStudents, txtSearchStudents.Text)
    End Sub

    Private Sub txtSearchTypes_TextChanged(sender As Object, e As EventArgs) Handles txtSearchTypes.TextChanged
        FilterGrid(dgvAssignmentTypes, txtSearchTypes.Text)
    End Sub

    ' ---------- Grades search (server-side, uses idx_grades_student_course) ----------
    Private Sub btnSearchGrades_Click(sender As Object, e As EventArgs) Handles btnSearchGrades.Click
        Dim sql As New System.Text.StringBuilder(
            "SELECT stu.first_name AS student_first, stu.last_name AS student_last, " &
            "c.course_code, c.course_name, at.type_name, g.grade, g.date_recorded " &
            "FROM Grades g " &
            "INNER JOIN Students stu ON g.student_id = stu.student_id " &
            "INNER JOIN Courses c ON g.course_id = c.course_id " &
            "INNER JOIN AssignmentTypes at ON g.type_id = at.type_id WHERE 1=1")

        Dim params As New List(Of MySqlParameter)

        Dim studentText As String = txtGradeStudent.Text.Trim()
        If studentText <> "" Then
            Dim studentId As Integer? = ParseLeadingId(studentText)
            If studentId Is Nothing Then
                MessageBox.Show("Pick a student from the suggestions (or type the student ID).", "Search")
                Return
            End If
            sql.Append(" AND g.student_id = @studentId")
            params.Add(New MySqlParameter("@studentId", studentId.Value))
        End If

        Dim courseText As String = txtGradeCourse.Text.Trim()
        If courseText <> "" Then
            sql.Append(" AND c.course_code = @courseCode")
            params.Add(New MySqlParameter("@courseCode", ParseCourseCode(courseText)))
        End If

        sql.Append(" ORDER BY g.date_recorded DESC")

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter(sql.ToString(), conn)
            adapter.SelectCommand.Parameters.AddRange(params.ToArray())
            Dim table As New DataTable()
            adapter.Fill(table)
            dgvAllGrades.DataSource = table
        Catch ex As Exception
            MessageBox.Show("Error searching grades: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    Friend Sub LoadCoursesAndInstructors()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            Dim instructorsTable As New DataTable()
            Dim instAdapter As New MySqlDataAdapter(
                "SELECT instructor_id, CONCAT(first_name, ' ', last_name, " &
                "IF(department IS NULL OR department = '', '', CONCAT(' (', department, ')'))) AS instructor_label " &
                "FROM Instructors ORDER BY last_name, first_name", conn)
            instAdapter.Fill(instructorsTable)

            Dim unassignedRow As DataRow = instructorsTable.NewRow()
            unassignedRow("instructor_id") = DBNull.Value
            unassignedRow("instructor_label") = "(Unassigned)"
            instructorsTable.Rows.InsertAt(unassignedRow, 0)

            cmbCourseInstructor.DataSource = instructorsTable
            cmbCourseInstructor.DisplayMember = "instructor_label"
            cmbCourseInstructor.ValueMember = "instructor_id"

            If dgvCourses IsNot Nothing Then
                Dim coursesAdapter As New MySqlDataAdapter(
                "SELECT c.course_id, c.course_code, c.course_name, c.course_fee, " &
                "COALESCE(CONCAT(i.first_name, ' ', i.last_name), 'Unassigned') AS instructor " &
                "FROM Courses c " &
                "LEFT JOIN Instructors i ON c.instructor_id = i.instructor_id " &
                "ORDER BY c.course_code", conn)
                Dim coursesTable As New DataTable()
                coursesAdapter.Fill(coursesTable)
                dgvCourses.DataSource = coursesTable
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading courses/instructors: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnUpdateCourse_Click(sender As Object, e As EventArgs) Handles btnUpdateCourse.Click
        If dgvCourses.SelectedRows.Count = 0 Then
            MessageBox.Show("Select a course row to update.", "No Selection")
            Return
        End If

        Dim courseId As Integer = Convert.ToInt32(dgvCourses.SelectedRows(0).Cells("course_id").Value)
        Dim courseCode As String = txtCourseCode.Text.Trim().ToUpper()
        Dim courseName As String = txtCourseName.Text.Trim()
        Dim courseFee As Decimal

        If courseCode = "" OrElse courseName = "" Then
            MessageBox.Show("Enter both a course code and a course title.", "Missing Info")
            Return
        End If

        If Not Decimal.TryParse(txtCourseFee.Text.Trim(), courseFee) OrElse courseFee < 0D Then
            MessageBox.Show("Enter a valid course fee (0 or greater).", "Invalid Fee", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim instructorParam As Object = DBNull.Value
        Dim selectedVal As Object = cmbCourseInstructor.SelectedValue
        If selectedVal IsNot Nothing AndAlso Not IsDBNull(selectedVal) AndAlso Not (TypeOf selectedVal Is DataRowView) Then
            instructorParam = Convert.ToInt32(selectedVal)
        End If

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            Using dupCmd As New MySqlCommand(
                "SELECT COUNT(*) FROM Courses WHERE course_code = @code AND course_id <> @id", conn)
                dupCmd.Parameters.AddWithValue("@code", courseCode)
                dupCmd.Parameters.AddWithValue("@id", courseId)
                If Convert.ToInt32(dupCmd.ExecuteScalar()) > 0 Then
                    MessageBox.Show("Another course already uses code '" & courseCode & "'.", "Duplicate Course")
                    Return
                End If
            End Using

            Dim sql As String =
                "UPDATE Courses SET course_code = @code, course_name = @name, " &
                "course_fee = @fee, instructor_id = @instructorId WHERE course_id = @id"

            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@code", courseCode)
                cmd.Parameters.AddWithValue("@name", courseName)
                cmd.Parameters.AddWithValue("@fee", courseFee)
                cmd.Parameters.AddWithValue("@instructorId", instructorParam)
                cmd.Parameters.AddWithValue("@id", courseId)
                cmd.ExecuteNonQuery()
            End Using

            If lblCourseStatus IsNot Nothing Then
                lblCourseStatus.Text = "Course '" & courseCode & "' updated."
                lblCourseStatus.ForeColor = Color.Green
            Else
                MessageBox.Show("Course '" & courseCode & "' updated.", "Success")
            End If

            LoadCoursesAndInstructors()
            LoadEnrollmentLists()
            AttachAutoComplete(txtGradeCourse, "SELECT CONCAT(course_code, ' - ', course_name) FROM Courses")

        Catch ex As Exception
            MessageBox.Show("Error updating course: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnDeleteCourse_Click(sender As Object, e As EventArgs) Handles btnDeleteCourse.Click
        If dgvCourses.SelectedRows.Count = 0 Then
            MessageBox.Show("Select a course row to delete.", "No Selection")
            Return
        End If

        Dim courseId As Integer = Convert.ToInt32(dgvCourses.SelectedRows(0).Cells("course_id").Value)
        Dim courseCode As String = Convert.ToString(dgvCourses.SelectedRows(0).Cells("course_code").Value)

        If MessageBox.Show("Delete course '" & courseCode & "'? This cannot be undone.",
                           "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            Using cmd As New MySqlCommand("DELETE FROM Courses WHERE course_id = @id", conn)
                cmd.Parameters.AddWithValue("@id", courseId)
                cmd.ExecuteNonQuery()
            End Using

            If lblCourseStatus IsNot Nothing Then
                lblCourseStatus.Text = "Course '" & courseCode & "' deleted."
                lblCourseStatus.ForeColor = Color.Green
            Else
                MessageBox.Show("Course '" & courseCode & "' deleted.", "Success")
            End If

            txtCourseCode.Clear()
            txtCourseName.Clear()
            txtCourseFee.Clear()
            If cmbCourseInstructor.Items.Count > 0 Then
                cmbCourseInstructor.SelectedIndex = 0
            End If

            LoadCoursesAndInstructors()
            LoadEnrollmentLists()
            AttachAutoComplete(txtGradeCourse, "SELECT CONCAT(course_code, ' - ', course_name) FROM Courses")

        Catch ex As Exception
            MessageBox.Show("Cannot delete course (it may have enrollments, grades, or payments): " & ex.Message, "Delete Error")
        Finally
            conn.Close()
        End Try
    End Sub
    Private Sub btnClearGradeSearch_Click(sender As Object, e As EventArgs) Handles btnClearGradeSearch.Click
        txtGradeStudent.Clear()
        txtGradeCourse.Clear()
        LoadAllGrades()
    End Sub

    Private Sub btnAuditLog_Click(sender As Object, e As EventArgs) Handles btnAuditLog.Click
        Dim form As New AuditLogForm()
        form.Show()
    End Sub

    Private Sub btnBackups_Click(sender As Object, e As EventArgs) Handles btnBackups.Click
        Dim form As New BackupForm()
        form.Show()
    End Sub

    Private Sub btnReportsForm_Click(sender As Object, e As EventArgs) Handles btnReportsForm.Click
        Dim reports As New ReportsForm(_adminUserId, 1)
        reports.Show()
    End Sub

    Private Sub FilterGrid(grid As DataGridView, searchText As String)
        Dim dt As DataTable = TryCast(grid.DataSource, DataTable)
        If dt Is Nothing Then Return

        Dim term As String = searchText.Trim().Replace("'", "''")
        If term = "" Then
            dt.DefaultView.RowFilter = ""
            Return
        End If

        Dim parts As New List(Of String)()
        For Each col As DataColumn In dt.Columns
            If col.DataType Is GetType(String) Then
                parts.Add(String.Format("[{0}] LIKE '%{1}%'", col.ColumnName, term))
            End If
        Next

        dt.DefaultView.RowFilter = String.Join(" OR ", parts)
    End Sub

    Private Function ParseLeadingId(text As String) As Integer?
        Dim digits As New String(text.TakeWhile(AddressOf Char.IsDigit).ToArray())
        Dim id As Integer
        If digits <> "" AndAlso Integer.TryParse(digits, id) Then
            Return id
        End If
        Return Nothing
    End Function

    Private Function ParseCourseCode(text As String) As String
        Dim cleaned As String = text.Trim().ToUpper()
        Dim dashIdx As Integer = cleaned.IndexOf(" - ")
        If dashIdx > 0 Then
            cleaned = cleaned.Substring(0, dashIdx).Trim()
        End If
        Return cleaned
    End Function

    Private Sub AttachAutoComplete(box As TextBox, sql As String)
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    Dim items As New AutoCompleteStringCollection()
                    While reader.Read()
                        If Not reader.IsDBNull(0) Then
                            items.Add(reader.GetString(0))
                        End If
                    End While
                    box.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    box.AutoCompleteSource = AutoCompleteSource.CustomSource
                    box.AutoCompleteCustomSource = items
                End Using
            End Using
        Catch
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnAddCourse_Click(sender As Object, e As EventArgs) Handles btnAddCourse.Click
        Dim courseCode As String = txtCourseCode.Text.Trim().ToUpper()
        Dim courseName As String = txtCourseName.Text.Trim()
        Dim courseFee As Decimal

        If courseCode = "" OrElse courseName = "" Then
            MessageBox.Show("Enter both a course code and a course title.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not Decimal.TryParse(txtCourseFee.Text.Trim(), courseFee) OrElse courseFee < 0D Then
            MessageBox.Show("Enter a valid course fee (0 or greater).", "Invalid Fee", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim instructorParam As Object = DBNull.Value
        Dim selectedVal As Object = cmbCourseInstructor.SelectedValue
        If selectedVal IsNot Nothing AndAlso Not IsDBNull(selectedVal) AndAlso Not (TypeOf selectedVal Is DataRowView) Then
            instructorParam = Convert.ToInt32(selectedVal)
        End If

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            Using checkCmd As New MySqlCommand("SELECT COUNT(*) FROM Courses WHERE course_code = @code", conn)
                checkCmd.Parameters.AddWithValue("@code", courseCode)
                If Convert.ToInt32(checkCmd.ExecuteScalar()) > 0 Then
                    MessageBox.Show("Course code '" & courseCode & "' already exists.", "Duplicate Course", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End Using

            Dim sql As String =
            "INSERT INTO Courses (course_code, course_name, instructor_id, course_fee) " &
            "VALUES (@code, @name, @instructorId, @fee)"

            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@code", courseCode)
                cmd.Parameters.AddWithValue("@name", courseName)
                cmd.Parameters.AddWithValue("@instructorId", instructorParam)
                cmd.Parameters.AddWithValue("@fee", courseFee)
                cmd.ExecuteNonQuery()
            End Using

            Dim msg As String = "Course '" & courseCode & " - " & courseName & "' created."
            If lblCourseStatus IsNot Nothing Then
                lblCourseStatus.Text = msg
                lblCourseStatus.ForeColor = Color.Green
            End If
            MessageBox.Show(msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            txtCourseCode.Clear()
            txtCourseName.Clear()
            txtCourseFee.Text = "0.00"
            If cmbCourseInstructor.Items.Count > 0 Then
                cmbCourseInstructor.SelectedIndex = 0
            End If

            LoadCoursesAndInstructors()
            LoadEnrollmentLists()
            AttachAutoComplete(txtGradeCourse, "SELECT CONCAT(course_code, ' - ', course_name) FROM Courses")

        Catch ex As Exception
            Dim errMsg As String = "Error creating course: " & ex.Message
            If lblCourseStatus IsNot Nothing Then
                lblCourseStatus.Text = errMsg
                lblCourseStatus.ForeColor = Color.Red
            End If
            MessageBox.Show(errMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub

End Class