Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO
Imports System.Text

Public Class ReportsForm

    Private ReadOnly _userId As Integer
    Private ReadOnly _roleId As Integer ' 1 = Admin, 2 = Teacher

    Public Sub New(userId As Integer, roleId As Integer)
        InitializeComponent()
        _userId = userId
        _roleId = roleId

        dgvTranscript.AllowUserToAddRows = False
        dgvTranscript.ReadOnly = True
        dgvAnalytics.AllowUserToAddRows = False
        dgvAnalytics.ReadOnly = True

        dtpFrom.Value = Date.Today.AddMonths(-6)
        dtpTo.Value = Date.Today

        LoadStudentAutoComplete()
        LoadCourseFilter()
        LoadInstructorFilter()
        LoadAnalytics()
    End Sub

    Private Function TeacherCourseFilter(column As String) As String
        If _roleId <> 2 Then Return ""
        Return " AND " & column & " IN (SELECT c.course_" & If(column.EndsWith("code"), "code", "id") &
               " FROM Courses c INNER JOIN Instructors i ON c.instructor_id = i.instructor_id" &
               " WHERE i.user_id = @uid)"
    End Function

    Private Sub FillGrid(grid As DataGridView, sql As String, ParamArray params() As MySqlParameter)
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter(sql, conn)
            adapter.SelectCommand.Parameters.AddRange(params)
            Dim table As New DataTable()
            adapter.Fill(table)
            grid.DataSource = table
        Catch ex As Exception
            MessageBox.Show("Error loading report: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    ' ---------- Student Transcript ----------
    Private Sub LoadStudentAutoComplete()
        Dim source As New AutoCompleteStringCollection()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand(
                "SELECT student_id, CONCAT(first_name, ' ', last_name) AS full_name FROM Students", conn)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    source.Add(reader.GetString("full_name"))
                    source.Add(reader.GetInt32("student_id").ToString())
                End While
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading student suggestions: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try

        txtStudentSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtStudentSearch.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtStudentSearch.AutoCompleteCustomSource = source
    End Sub

    Private Sub btnSearchTranscript_Click(sender As Object, e As EventArgs) Handles btnSearchTranscript.Click
        Dim term As String = txtStudentSearch.Text.Trim()
        If term = "" Then
            MessageBox.Show("Enter a student ID or name.", "Missing Info")
            Return
        End If

        Dim sql As String =
            "SELECT student_id, student_name, course_code, course_name, assignment_type, grade, " &
            "default_weight, enrollment_date, " &
            "CONCAT(instructor_first_name, ' ', instructor_last_name) AS instructor " &
            "FROM StudentTranscript WHERE "

        Dim studentId As Integer
        Dim param As MySqlParameter
        If Integer.TryParse(term, studentId) Then
            sql &= "student_id = @term"
            param = New MySqlParameter("@term", studentId)
        Else
            sql &= "student_name LIKE @term"
            param = New MySqlParameter("@term", "%" & EscapeLike(term) & "%")
        End If

        sql &= TeacherCourseFilter("course_code") & " ORDER BY student_name, course_code, assignment_type"

        FillGrid(dgvTranscript, sql, param, New MySqlParameter("@uid", _userId))

        lblTranscriptInfo.Text = dgvTranscript.Rows.Count & " record(s) found."
    End Sub

    ' ---------- Course Analytics ----------
    Private Sub LoadCourseFilter()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim sql As String = "SELECT course_id, course_code FROM Courses WHERE 1=1" &
                                TeacherCourseFilter("course_id")
            Dim adapter As New MySqlDataAdapter(sql, conn)
            adapter.SelectCommand.Parameters.AddWithValue("@uid", _userId)
            Dim table As New DataTable()
            adapter.Fill(table)

            Dim allRow As DataRow = table.NewRow()
            allRow("course_id") = 0
            allRow("course_code") = "(All courses)"
            table.Rows.InsertAt(allRow, 0)

            cmbCourseFilter.DataSource = table
            cmbCourseFilter.DisplayMember = "course_code"
            cmbCourseFilter.ValueMember = "course_id"
        Catch ex As Exception
            MessageBox.Show("Error loading courses: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub LoadInstructorFilter()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter(
                "SELECT instructor_id, CONCAT(first_name, ' ', last_name) AS instructor_name FROM Instructors", conn)
            Dim table As New DataTable()
            adapter.Fill(table)

            Dim allRow As DataRow = table.NewRow()
            allRow("instructor_id") = 0
            allRow("instructor_name") = "(All instructors)"
            table.Rows.InsertAt(allRow, 0)

            cmbInstructorFilter.DataSource = table
            cmbInstructorFilter.DisplayMember = "instructor_name"
            cmbInstructorFilter.ValueMember = "instructor_id"

            ' A teacher only ever sees their own courses, so the instructor filter doesn't apply
            cmbInstructorFilter.Enabled = (_roleId = 1)
        Catch ex As Exception
            MessageBox.Show("Error loading instructors: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub LoadAnalytics()
        Dim sql As New StringBuilder(
            "SELECT course_code, course_name, instructor_name, enrolled_students, grades_submitted, " &
            "ROUND(average_grade, 2) AS average_grade, minimum_grade, maximum_grade " &
            "FROM CoursePerformanceAnalytics WHERE 1=1")

        Dim params As New List(Of MySqlParameter)
        params.Add(New MySqlParameter("@uid", _userId))

        If cmbCourseFilter.SelectedValue IsNot Nothing AndAlso Convert.ToInt32(cmbCourseFilter.SelectedValue) > 0 Then
            sql.Append(" AND course_id = @courseId")
            params.Add(New MySqlParameter("@courseId", Convert.ToInt32(cmbCourseFilter.SelectedValue)))
        End If

        If cmbInstructorFilter.SelectedValue IsNot Nothing AndAlso Convert.ToInt32(cmbInstructorFilter.SelectedValue) > 0 Then
            sql.Append(" AND course_id IN (SELECT course_id FROM Courses WHERE instructor_id = @instructorId)")
            params.Add(New MySqlParameter("@instructorId", Convert.ToInt32(cmbInstructorFilter.SelectedValue)))
        End If

        If chkUseDates.Checked Then
            sql.Append(" AND course_id IN (SELECT course_id FROM Enrollments WHERE enrollment_date >= @fromDate AND enrollment_date < @toDateExclusive)")
            params.Add(New MySqlParameter("@fromDate", dtpFrom.Value.Date))
            params.Add(New MySqlParameter("@toDateExclusive", dtpTo.Value.Date.AddDays(1)))
        End If

        sql.Append(TeacherCourseFilter("course_id"))
        sql.Append(" ORDER BY course_code")

        FillGrid(dgvAnalytics, sql.ToString(), params.ToArray())
    End Sub

    Private Sub btnApplyFilters_Click(sender As Object, e As EventArgs) Handles btnApplyFilters.Click
        LoadAnalytics()
    End Sub

    ' ---------- Export ----------
    Private Function ActiveGrid() As DataGridView
        Return If(tabReports.SelectedTab Is tabTranscript, dgvTranscript, dgvAnalytics)
    End Function

    Private Function CsvEscape(value As Object) As String
        Dim text As String = If(value Is Nothing OrElse IsDBNull(value), "", value.ToString())
        If text.Contains(",") OrElse text.Contains("""") OrElse text.Contains(vbLf) Then
            text = """" & text.Replace("""", """""") & """"
        End If
        Return text
    End Function

    Private Sub btnExportCsv_Click(sender As Object, e As EventArgs) Handles btnExportCsv.Click
        Dim grid As DataGridView = ActiveGrid()
        If grid.Rows.Count = 0 Then
            MessageBox.Show("There is nothing to export. Run a report first.", "No Data")
            Return
        End If

        Using dialog As New SaveFileDialog()
            dialog.Filter = "CSV file (*.csv)|*.csv"
            dialog.FileName = "report_" & Date.Now.ToString("yyyyMMdd_HHmm") & ".csv"
            If dialog.ShowDialog() <> DialogResult.OK Then Return

            Try
                Dim sb As New StringBuilder()
                sb.AppendLine(String.Join(",", grid.Columns.Cast(Of DataGridViewColumn)().Select(Function(c) CsvEscape(c.HeaderText))))

                For Each row As DataGridViewRow In grid.Rows
                    If row.IsNewRow Then Continue For
                    sb.AppendLine(String.Join(",", row.Cells.Cast(Of DataGridViewCell)().Select(Function(c) CsvEscape(c.Value))))
                Next

                File.WriteAllText(dialog.FileName, sb.ToString(), Encoding.UTF8)
                MessageBox.Show("Report exported to CSV.", "Export Complete")
            Catch ex As Exception
                MessageBox.Show("Export failed: " & ex.Message, "Error")
            End Try
        End Using
    End Sub

    Private Sub btnExportPdf_Click(sender As Object, e As EventArgs) Handles btnExportPdf.Click
        Dim grid As DataGridView = ActiveGrid()
        Dim hasData As Boolean = grid.Rows.Cast(Of DataGridViewRow)().Any(Function(r) Not r.IsNewRow)
        If Not hasData Then
            MessageBox.Show("There is nothing to export. Run a report first.", "No Data")
            Return
        End If

        Dim isTranscript As Boolean = (tabReports.SelectedTab Is tabTranscript)
        Dim reportTitle As String = If(isTranscript, "Student Transcript", "Course Performance Analytics")
        Dim reportSubtitle As String = BuildReportSubtitle(isTranscript)

        Using dialog As New SaveFileDialog()
            dialog.Filter = "PDF file (*.pdf)|*.pdf"
            dialog.FileName = If(isTranscript, "transcript_", "analytics_") & Date.Now.ToString("yyyyMMdd_HHmm") & ".pdf"
            If dialog.ShowDialog() <> DialogResult.OK Then Return

            Try
                PdfExport.ExportGrid(grid, reportTitle, dialog.FileName, reportSubtitle)
                MessageBox.Show("Report exported to PDF.", "Export Complete")
            Catch ex As Exception
                MessageBox.Show("Export failed: " & ex.Message, "Error")
            End Try
        End Using
    End Sub

    Private Function BuildReportSubtitle(isTranscript As Boolean) As String
        Dim parts As New List(Of String) From {
            "Generated: " & Date.Now.ToString("MMM dd, yyyy hh:mm tt")
        }

        If isTranscript Then
            If Not String.IsNullOrWhiteSpace(txtStudentSearch.Text) Then
                parts.Add("Search: " & txtStudentSearch.Text.Trim())
            End If
        Else
            If cmbCourseFilter.SelectedIndex > 0 Then
                parts.Add("Course: " & cmbCourseFilter.Text)
            End If
            If cmbInstructorFilter.SelectedIndex > 0 Then
                parts.Add("Instructor: " & cmbInstructorFilter.Text)
            End If
            If chkUseDates.Checked Then
                parts.Add($"Enrolled: {dtpFrom.Value:MMM dd, yyyy} - {dtpTo.Value:MMM dd, yyyy}")
            End If
        End If

        Return String.Join("  |  ", parts)
    End Function
End Class