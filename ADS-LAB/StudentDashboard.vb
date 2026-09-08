Imports MySql.Data.MySqlClient
Imports System.Data

Public Class StudentDashboard

    Private ReadOnly _studentUserId As Integer

    Public Sub New(studentUserId As Integer)
        InitializeComponent()
        _studentUserId = studentUserId
        LoadMyGrades()
        LoadCourseAverages()
    End Sub

    ' =========================================================
    '  FULL GRADE LIST (from StudentGradeSummary view)
    ' =========================================================
    Private Sub LoadMyGrades()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            Dim query As String =
                "SELECT course_code, course_name, type_name, grade, date_recorded " &
                "FROM StudentGradeSummary WHERE user_id = @userId " &
                "ORDER BY course_code, type_name"

            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.SelectCommand.Parameters.AddWithValue("@userId", _studentUserId)

            Dim table As New DataTable()
            adapter.Fill(table)
            dgvMyGrades.DataSource = table

            lblWelcome.Text = "My Grades" & If(table.Rows.Count = 0, " (no grades recorded yet)", "")

        Catch ex As Exception
            MessageBox.Show("Error loading grades: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    ' =========================================================
    '  OVERALL AVERAGE PER COURSE
    ' =========================================================
    Private Sub LoadCourseAverages()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            ' Aggregate directly off the same view, grouped by course
            Dim query As String =
                "SELECT course_code, course_name, " &
                "       ROUND(AVG(grade), 2) AS overall_average, " &
                "       COUNT(*) AS graded_items " &
                "FROM StudentGradeSummary " &
                "WHERE user_id = @userId " &
                "GROUP BY course_code, course_name"

            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.SelectCommand.Parameters.AddWithValue("@userId", _studentUserId)

            Dim table As New DataTable()
            adapter.Fill(table)
            dgvCourseAverages.DataSource = table

        Catch ex As Exception
            MessageBox.Show("Error loading course averages: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

End Class