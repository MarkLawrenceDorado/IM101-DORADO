Imports System.Data
Imports MySql.Data.MySqlClient
Imports Org.BouncyCastle.Asn1.Cmp

Public Class TeacherDashboard

    Private ReadOnly _teacherUserId As Integer

    Public Sub New(teacherUserId As Integer)
        InitializeComponent()
        _teacherUserId = teacherUserId
        lblStatus.Text = ""

        dgvCourseOverview.ReadOnly = True
        dgvCourseOverview.AllowUserToAddRows = False

        LoadCourseOverview()
        LoadCoursesForGrading()
        LoadAssignmentTypes()
        SearchHelper.AttachAutoComplete(txtCourseSearch, "SELECT c.course_code FROM Courses c " &
        "INNER JOIN Instructors i ON c.instructor_id = i.instructor_id WHERE i.user_id = @uid",
        New MySqlParameter("@uid", _teacherUserId))
    End Sub

    ' =========================================================
    '  COURSE OVERVIEW (from TeacherCourseOverview view)
    ' =========================================================
    Private Sub LoadCourseOverview()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            Dim query As String =
                "SELECT course_code, course_name, type_name, submissions, average_grade " &
                "FROM TeacherCourseOverview WHERE user_id = @userId"

            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.SelectCommand.Parameters.AddWithValue("@userId", _teacherUserId)

            Dim table As New DataTable()
            adapter.Fill(table)
            dgvCourseOverview.DataSource = table

        Catch ex As Exception
            MessageBox.Show("Error loading course overview: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    ' =========================================================
    '  LOAD COURSES TAUGHT BY THIS TEACHER (for the grading dropdown)
    ' =========================================================
    Private Sub LoadCoursesForGrading()
        Using conn As MySqlConnection = DBConnection.GetConnection()
            conn.Open()
            Dim sql As String =
            "SELECT c.course_id, CONCAT(c.course_code, ' - ', c.course_name) AS course_label " &
            "FROM Courses c " &
            "INNER JOIN Instructors i ON c.instructor_id = i.instructor_id " &
            "WHERE i.user_id = @userId " &
            "ORDER BY c.course_code"

            Using adapter As New MySqlDataAdapter(sql, conn)
                adapter.SelectCommand.Parameters.AddWithValue("@userId", _teacherUserId)
                Dim table As New DataTable()
                adapter.Fill(table)

                cmbCourse.DisplayMember = "course_label"
                cmbCourse.ValueMember = "course_id"
                cmbCourse.DataSource = table
            End Using
        End Using
    End Sub

    ' =========================================================
    '  LOAD ASSIGNMENT TYPES (for the grading dropdown)
    ' =========================================================
    Private Sub LoadAssignmentTypes()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            Dim adapter As New MySqlDataAdapter("SELECT type_id, type_name FROM AssignmentTypes", conn)
            Dim table As New DataTable()
            adapter.Fill(table)

            cmbAssignmentType.DataSource = table
            cmbAssignmentType.DisplayMember = "type_name"
            cmbAssignmentType.ValueMember = "type_id"

        Catch ex As Exception
            MessageBox.Show("Error loading assignment types: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    ' =========================================================
    '  LOAD STUDENTS ENROLLED IN THE SELECTED COURSE
    ' =========================================================
    Private Sub cmbCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourse.SelectedIndexChanged
        If cmbCourse.SelectedValue Is Nothing OrElse
       IsDBNull(cmbCourse.SelectedValue) OrElse
       TypeOf cmbCourse.SelectedValue Is DataRowView Then
            cmbStudent.DataSource = Nothing
            Return
        End If

        Dim selectedCourseId As Integer = Convert.ToInt32(cmbCourse.SelectedValue)

        Using conn As MySqlConnection = DBConnection.GetConnection()
            Try
                conn.Open()

                Dim query As String =
                "SELECT stu.student_id, " &
                "       COALESCE(stu.first_name, '') AS first_name, " &
                "       COALESCE(stu.last_name, '') AS last_name " &
                "FROM Enrollments e " &
                "INNER JOIN Students stu ON e.student_id = stu.student_id " &
                "WHERE e.course_id = @courseId " &
                "ORDER BY stu.last_name, stu.first_name"

                Using adapter As New MySqlDataAdapter(query, conn)
                    adapter.SelectCommand.Parameters.AddWithValue("@courseId", selectedCourseId)

                    Dim table As New DataTable()
                    adapter.Fill(table)

                    table.Columns.Add("full_name", GetType(String), "first_name + ' ' + last_name")

                    cmbStudent.DisplayMember = "full_name"
                    cmbStudent.ValueMember = "student_id"
                    cmbStudent.DataSource = table
                End Using

            Catch ex As Exception
                MessageBox.Show("Error loading enrolled students: " & ex.Message, "Error")
            End Try
        End Using
    End Sub

    ' =========================================================
    '  SAVE GRADE (INSERT new grade, or UPDATE if one already exists)
    ' =========================================================
    Private Sub btnSaveGrade_Click(sender As Object, e As EventArgs) Handles btnSaveGrade.Click
        If cmbCourse.SelectedValue Is Nothing OrElse cmbAssignmentType.SelectedValue Is Nothing OrElse cmbStudent.SelectedValue Is Nothing Then
            MessageBox.Show("Please select a course, assignment type, and student.", "Missing Info")
            Return
        End If

        Dim gradeText As String = txtGrade.Text.Trim()
        Dim gradeValue As Decimal

        If Not Decimal.TryParse(gradeText, gradeValue) Then
            MessageBox.Show("Please enter a valid numeric grade.", "Invalid Grade")
            Return
        End If

        If gradeValue < 0 OrElse gradeValue > 100 Then
            MessageBox.Show("Grade must be between 0 and 100.", "Invalid Grade")
            Return
        End If

        Dim studentId As Integer = Convert.ToInt32(cmbStudent.SelectedValue)
        Dim courseId As Integer = Convert.ToInt32(cmbCourse.SelectedValue)
        Dim typeId As Integer = Convert.ToInt32(cmbAssignmentType.SelectedValue)

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            ' Since (student_id, course_id, type_id) is the composite PK,
            ' this either inserts a new grade or updates the existing one.
            Dim query As String =
                "INSERT INTO Grades (student_id, course_id, type_id, grade) " &
                "VALUES (@studentId, @courseId, @typeId, @grade) " &
                "ON DUPLICATE KEY UPDATE grade = @grade, date_recorded = CURRENT_TIMESTAMP"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@studentId", studentId)
            cmd.Parameters.AddWithValue("@courseId", courseId)
            cmd.Parameters.AddWithValue("@typeId", typeId)
            cmd.Parameters.AddWithValue("@grade", gradeValue)
            cmd.ExecuteNonQuery()

            lblStatus.Text = "Grade saved successfully."
            lblStatus.ForeColor = Color.Green

            txtGrade.Clear()
            LoadCourseOverview() ' refresh the averages/submission counts

        Catch ex As Exception
            lblStatus.Text = "Error saving grade: " & ex.Message
            lblStatus.ForeColor = Color.Red
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnCalculateFinal_Click(sender As Object, e As EventArgs) Handles btnCalculateFinal.Click
        If cmbCourse.SelectedValue Is Nothing Then
            MessageBox.Show("Select a course first.", "Missing Info")
            Return
        End If

        Dim courseId As Integer = Convert.ToInt32(cmbCourse.SelectedValue)

        Dim results As New DataTable()
        results.Columns.Add("student_id", GetType(Integer))
        results.Columns.Add("Student", GetType(String))
        results.Columns.Add("Calculated", GetType(Decimal))
        results.Columns.Add("Final Grade", GetType(Decimal))
        results.Columns.Add("Weight Graded %", GetType(Decimal))
        results.Columns.Add("Status", GetType(String))

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            Dim enrolled As New DataTable()
            Dim enrolledAdapter As New MySqlDataAdapter(
                "SELECT s.student_id, CONCAT(s.first_name, ' ', s.last_name) AS full_name " &
                "FROM Enrollments e INNER JOIN Students s ON e.student_id = s.student_id " &
                "WHERE e.course_id = @courseId ORDER BY s.last_name, s.first_name", conn)
            enrolledAdapter.SelectCommand.Parameters.AddWithValue("@courseId", courseId)
            enrolledAdapter.Fill(enrolled)

            For Each student As DataRow In enrolled.Rows
                Dim studentId As Integer = Convert.ToInt32(student("student_id"))

                ' Weighted final grade from the stored procedure
                Dim finalGrade As Object = DBNull.Value
                Using cmd As New MySqlCommand("sp_CalculateCourseGrade", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@p_student_id", studentId)
                    cmd.Parameters.AddWithValue("@p_course_id", courseId)

                    Dim output As New MySqlParameter("@p_final_grade", MySqlDbType.Decimal)
                    output.Precision = 5
                    output.Scale = 2
                    output.Direction = ParameterDirection.Output
                    cmd.Parameters.Add(output)

                    cmd.ExecuteNonQuery()
                    If output.Value IsNot Nothing AndAlso Not IsDBNull(output.Value) Then
                        finalGrade = Convert.ToDecimal(output.Value)
                    End If
                End Using

                ' How much of the course weight has been graded so far
                Dim weightGraded As Decimal = 0
                Using weightCmd As New MySqlCommand(
                    "SELECT COALESCE(SUM(at.default_weight), 0) FROM Grades g " &
                    "INNER JOIN AssignmentTypes at ON g.type_id = at.type_id " &
                    "WHERE g.student_id = @s AND g.course_id = @c", conn)
                    weightCmd.Parameters.AddWithValue("@s", studentId)
                    weightCmd.Parameters.AddWithValue("@c", courseId)
                    weightGraded = Convert.ToDecimal(weightCmd.ExecuteScalar())
                End Using

                Dim status As String
                If IsDBNull(finalGrade) Then
                    status = "No grades yet"
                ElseIf weightGraded < 100 Then
                    status = "Partial (" & weightGraded.ToString("0.##") & "% graded)"
                Else
                    status = "Calculated"
                End If

                results.Rows.Add(studentId, student("full_name"), finalGrade, finalGrade, weightGraded, status)
            Next

            dgvFinalGrades.DataSource = results
            dgvFinalGrades.AllowUserToAddRows = False
            dgvFinalGrades.ReadOnly = False
            dgvFinalGrades.Columns("student_id").Visible = False
            dgvFinalGrades.Columns("Final Grade").DefaultCellStyle.Format = "0.00"
            dgvFinalGrades.Columns("Calculated").DefaultCellStyle.Format = "0.00"

            ' Only "Final Grade" can be edited (the override)
            For Each column As DataGridViewColumn In dgvFinalGrades.Columns
                column.ReadOnly = (column.Name <> "Final Grade")
            Next

            lblStatus.Text = "Final grades calculated for " & results.Rows.Count & " student(s). Edit 'Final Grade' to override."
            lblStatus.ForeColor = Color.Green

        Catch ex As Exception
            lblStatus.Text = "Error calculating final grades: " & ex.Message
            lblStatus.ForeColor = Color.Red
        Finally
            conn.Close()
        End Try
    End Sub

    ' ---------- Override: validate and mark the row ----------
    Private Sub dgvFinalGrades_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFinalGrades.CellEndEdit
        If dgvFinalGrades.Columns(e.ColumnIndex).Name <> "Final Grade" Then Return

        Dim row As DataGridViewRow = dgvFinalGrades.Rows(e.RowIndex)
        Dim calculated As Object = row.Cells("Calculated").Value
        Dim entered As Object = row.Cells("Final Grade").Value
        Dim newValue As Decimal

        If IsDBNull(entered) OrElse Not Decimal.TryParse(entered.ToString(), newValue) _
           OrElse newValue < 0 OrElse newValue > 100 Then
            MessageBox.Show("Enter a final grade between 0 and 100.", "Invalid Grade")
            row.Cells("Final Grade").Value = calculated
            Return
        End If

        If IsDBNull(calculated) OrElse newValue <> Convert.ToDecimal(calculated) Then
            row.Cells("Status").Value = "Overridden"
        End If
    End Sub

    Private Sub dgvFinalGrades_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvFinalGrades.DataError
        e.ThrowException = False
        MessageBox.Show("Enter a numeric grade between 0 and 100.", "Invalid Grade")
    End Sub

    Private Sub btnCourseSearch_Click(sender As Object, e As EventArgs) Handles btnCourseSearch.Click
        Dim rawText As String = txtCourseSearch.Text.Trim()

        If rawText = "" Then
            lblSearchStatus.Text = ""
            LoadCourseOverview()
            Return
        End If

        Dim codeTerm As String = FormHelpers.ParseCourseCode(rawText)

        Using conn As MySqlConnection = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim sql As String =
                    "SELECT course_code, course_name, type_name, submissions, average_grade " &
                    "FROM TeacherCourseOverview " &
                    "WHERE user_id = @userId AND (course_code LIKE @codeTerm OR course_name LIKE @nameTerm)"

                Using adapter As New MySqlDataAdapter(sql, conn)
                    adapter.SelectCommand.Parameters.AddWithValue("@userId", _teacherUserId)
                    adapter.SelectCommand.Parameters.AddWithValue("@codeTerm", EscapeLike(codeTerm) & "%")
                    adapter.SelectCommand.Parameters.AddWithValue("@nameTerm", "%" & EscapeLike(rawText) & "%")

                    Dim table As New DataTable()
                    adapter.Fill(table)
                    dgvCourseOverview.DataSource = table

                    If table.Rows.Count = 0 Then
                        lblSearchStatus.Text = "No matching courses found for '" & rawText & "'."
                        lblSearchStatus.ForeColor = Color.DarkOrange
                    Else
                        lblSearchStatus.Text = table.Rows.Count & " result(s) found."
                        lblSearchStatus.ForeColor = Color.DimGray
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show("Error searching courses: " & ex.Message, "Error")
            End Try
        End Using
    End Sub

    Private Sub SaveFinalGradeOverride(studentId As Integer, courseId As Integer, finalGrade As Decimal)
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim sql As String =
            "INSERT INTO FinalGrades (student_id, course_id, final_grade, is_override, updated_at) " &
            "VALUES (@studentId, @courseId, @finalGrade, 1, NOW()) " &
            "ON DUPLICATE KEY UPDATE " &
            "final_grade = VALUES(final_grade), " &
            "is_override = 1, " &
            "updated_at = NOW()"

            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@studentId", studentId)
                cmd.Parameters.AddWithValue("@courseId", courseId)
                cmd.Parameters.AddWithValue("@finalGrade", finalGrade)
                cmd.ExecuteNonQuery()
            End Using
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnClearCourseSearch_Click(sender As Object, e As EventArgs) Handles btnCourseSearchClear.Click
        txtCourseSearch.Clear()
        lblSearchStatus.Text = ""
        LoadCourseOverview()
    End Sub

    Private Sub btnReportsForm_Click(sender As Object, e As EventArgs) Handles btnReportsForm.Click
        Dim frm As New ReportsForm(_teacherUserId, 2)
        frm.Show()
    End Sub

    Private Sub txtCourseSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCourseSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnCourseSearch.PerformClick()
        End If
    End Sub
    Private Function EscapeLike(value As String) As String
        Return value.Replace("\", "\\").Replace("%", "\%").Replace("_", "\_")
    End Function
End Class