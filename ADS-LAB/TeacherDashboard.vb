Imports System.Data
Imports MySql.Data.MySqlClient
Imports Org.BouncyCastle.Asn1.Cmp

Public Class TeacherDashboard

    Private ReadOnly _teacherUserId As Integer

    Public Sub New(teacherUserId As Integer)
        InitializeComponent()
        _teacherUserId = teacherUserId
        LoadCourseOverview()
        LoadCoursesForGrading()
        LoadAssignmentTypes()
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
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            Dim query As String =
                "SELECT c.course_id, c.course_code, c.course_name " &
                "FROM Courses c " &
                "INNER JOIN Instructors i ON c.instructor_id = i.instructor_id " &
                "WHERE i.user_id = @userId"

            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.SelectCommand.Parameters.AddWithValue("@userId", _teacherUserId)

            Dim table As New DataTable()
            adapter.Fill(table)

            cmbCourse.DataSource = table
            cmbCourse.DisplayMember = "course_code"
            cmbCourse.ValueMember = "course_id"

        Catch ex As Exception
            MessageBox.Show("Error loading courses: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
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
        If cmbCourse.SelectedValue Is Nothing Then Return

        Dim selectedCourseId As Integer = Convert.ToInt32(cmbCourse.SelectedValue)

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            Dim query As String =
                "SELECT stu.student_id, stu.first_name, stu.last_name " &
                "FROM Enrollments e " &
                "INNER JOIN Students stu ON e.student_id = stu.student_id " &
                "WHERE e.course_id = @courseId"

            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.SelectCommand.Parameters.AddWithValue("@courseId", selectedCourseId)

            Dim table As New DataTable()
            adapter.Fill(table)

            ' Build a display column combining first + last name
            table.Columns.Add("full_name", GetType(String), "first_name + ' ' + last_name")

            cmbStudent.DataSource = table
            cmbStudent.DisplayMember = "full_name"
            cmbStudent.ValueMember = "student_id"

        Catch ex As Exception
            MessageBox.Show("Error loading enrolled students: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
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


End Class