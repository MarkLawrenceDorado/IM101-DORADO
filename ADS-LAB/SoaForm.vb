Imports System.Data
Imports MySql.Data.MySqlClient

Public Class SoaForm
    Private ReadOnly _roleId As Integer
    Private ReadOnly _loggedInUserId As Integer
    Private _studentId As Integer

    ' roleId: 1 = Admin, 2 = Teacher, 3 = Student
    Public Sub New(roleId As Integer, loggedInUserId As Integer)
        InitializeComponent()
        _roleId = roleId
        _loggedInUserId = loggedInUserId
    End Sub


    Private Sub SoaForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim isStudent As Boolean = (_roleId = 3)

        ' Students only see their own SOA; Admins can switch students
        cmbSelectStudent.Visible = Not isStudent
        lblSelectStudent.Visible = Not isStudent

        If Not isStudent Then
            LoadStudentsForAdmin()
            If cmbSelectStudent.SelectedValue IsNot Nothing AndAlso Not (TypeOf cmbSelectStudent.SelectedValue Is DataRowView) Then
                _studentId = Convert.ToInt32(cmbSelectStudent.SelectedValue)
            End If
        End If

        LoadStudentSoa(_studentId)
    End Sub

    Private Sub LoadStudentsForAdmin()
        Using conn As MySqlConnection = DBConnection.GetConnection()
            conn.Open()
            Dim sql As String =
                "SELECT student_id, CONCAT(student_id, ' - ', last_name, ', ', first_name) AS student_label " &
                "FROM Students ORDER BY last_name, first_name"
            Dim adapter As New MySqlDataAdapter(sql, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            cmbSelectStudent.DataSource = dt
            cmbSelectStudent.DisplayMember = "student_label"
            cmbSelectStudent.ValueMember = "student_id"
        End Using
    End Sub

    Private Sub cmbSelectStudent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSelectStudent.SelectedIndexChanged
        If _roleId = 3 Then Return
        If cmbSelectStudent.SelectedValue Is Nothing OrElse TypeOf cmbSelectStudent.SelectedValue Is DataRowView Then Return

        _studentId = Convert.ToInt32(cmbSelectStudent.SelectedValue)
        LoadStudentSoa(_studentId)
    End Sub

    Private Sub LoadStudentSoa(studentId As Integer)
        If studentId <= 0 Then Return

        Using conn As MySqlConnection = DBConnection.GetConnection()
            Try
                conn.Open()

                ' 1. Course charges / SOA lines for the student
                Dim soaSql As String =
                    "SELECT c.course_code, c.course_name, COALESCE(c.course_fee, 0) AS course_fee " &
                    "FROM Enrollments e " &
                    "INNER JOIN Courses c ON e.course_id = c.course_id " &
                    "WHERE e.student_id = @studentId " &
                    "ORDER BY c.course_code"

                Dim soaTable As New DataTable()
                Using soaCmd As New MySqlCommand(soaSql, conn)
                    soaCmd.Parameters.AddWithValue("@studentId", studentId)
                    Using adapter As New MySqlDataAdapter(soaCmd)
                        adapter.Fill(soaTable)
                    End Using
                End Using
                dgvSoaItems.DataSource = soaTable

                ' 2. Total assessed fees
                Dim totalFees As Decimal = 0D
                For Each row As DataRow In soaTable.Rows
                    totalFees += Convert.ToDecimal(row("course_fee"))
                Next

                ' 3. Total payments made by this student
                Dim totalPaid As Decimal = 0D
                Dim paidSql As String = "SELECT COALESCE(SUM(amount_paid), 0) FROM Payments WHERE student_id = @studentId"
                Using paidCmd As New MySqlCommand(paidSql, conn)
                    paidCmd.Parameters.AddWithValue("@studentId", studentId)
                    totalPaid = Convert.ToDecimal(paidCmd.ExecuteScalar())
                End Using

                Dim balance As Decimal = totalFees - totalPaid

                lblTotalAssessed.Text = "Total Assessment: " & totalFees.ToString("N2")
                lblTotalPaid.Text = "Total Paid: " & totalPaid.ToString("N2")
                lblBalanceDue.Text = "Balance Due: " & balance.ToString("N2")

            Catch ex As Exception
                MessageBox.Show("Error loading Statement of Account: " & ex.Message, "SOA Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub
End Class