Imports MySql.Data.MySqlClient
Imports System.Data

Public Class StudentDashboard

    Private ReadOnly _studentUserId As Integer

    Public Sub New(studentUserId As Integer)
        InitializeComponent()
        _studentUserId = studentUserId

        dgvMyGrades.ReadOnly = True
        dgvMyGrades.AllowUserToAddRows = False
        dgvCourseAverages.ReadOnly = True
        dgvCourseAverages.AllowUserToAddRows = False

        LoadStudentHeader()
        LoadMyGrades()
        LoadCourseAverages()
    End Sub

    ' =========================================================
    '  STUDENT HEADER (name + user_id)
    ' =========================================================
    Private Sub LoadStudentHeader()
        Using conn As MySqlConnection = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String =
                    "SELECT CONCAT(first_name, ' ', last_name) AS full_name " &
                    "FROM Students WHERE user_id = @userId LIMIT 1"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@userId", _studentUserId)
                    Dim result As Object = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        lblWelcome.Text = "Student Dashboard — " & result.ToString() & " (User ID: " & _studentUserId & ")"
                    Else
                        lblWelcome.Text = "Student Dashboard — User ID: " & _studentUserId
                    End If
                End Using
            Catch ex As Exception
                lblWelcome.Text = "Student Dashboard"
            End Try
        End Using
    End Sub

    ' =========================================================
    '  FULL GRADE LIST (from StudentGradeSummary view)
    ' =========================================================
    Private Sub LoadMyGrades()
        Using conn As MySqlConnection = DBConnection.GetConnection()
            Try
                conn.Open()

                Dim query As String =
                    "SELECT course_code, course_name, type_name, grade, date_recorded " &
                    "FROM StudentGradeSummary WHERE user_id = @userId " &
                    "ORDER BY course_code, type_name"

                Using adapter As New MySqlDataAdapter(query, conn)
                    adapter.SelectCommand.Parameters.AddWithValue("@userId", _studentUserId)

                    Dim table As New DataTable()
                    adapter.Fill(table)
                    dgvMyGrades.DataSource = table

                    If dgvMyGrades.Columns.Contains("grade") Then
                        dgvMyGrades.Columns("grade").DefaultCellStyle.Format = "0.00"
                    End If

                    lblMyGrades.Text = If(table.Rows.Count = 0, "My Grades (no grades recorded yet)", "My Grades (" & table.Rows.Count & ")")
                End Using

            Catch ex As Exception
                MessageBox.Show("Error loading grades: " & ex.Message, "Error")
            End Try
        End Using
    End Sub

    ' =========================================================
    '  OVERALL AVERAGE PER COURSE
    ' =========================================================
    Private Sub LoadCourseAverages()
        Using conn As MySqlConnection = DBConnection.GetConnection()
            Try
                conn.Open()

                Dim query As String =
                    "SELECT course_code, course_name, " &
                    "       ROUND(AVG(grade), 2) AS overall_average, " &
                    "       COUNT(*) AS graded_items " &
                    "FROM StudentGradeSummary " &
                    "WHERE user_id = @userId " &
                    "GROUP BY course_code, course_name"

                Using adapter As New MySqlDataAdapter(query, conn)
                    adapter.SelectCommand.Parameters.AddWithValue("@userId", _studentUserId)

                    Dim table As New DataTable()
                    adapter.Fill(table)
                    dgvCourseAverages.DataSource = table

                    If dgvCourseAverages.Columns.Contains("overall_average") Then
                        dgvCourseAverages.Columns("overall_average").DefaultCellStyle.Format = "0.00"
                    End If
                End Using

            Catch ex As Exception
                MessageBox.Show("Error loading course averages: " & ex.Message, "Error")
            End Try
        End Using
    End Sub
    Private Sub btnMySoa_Click(sender As Object, e As EventArgs) Handles btnMySoa.Click
        Dim soaForm As New SoaForm(roleId:=3, loggedInUserId:=_studentUserId)
        soaForm.ShowDialog()
    End Sub
End Class