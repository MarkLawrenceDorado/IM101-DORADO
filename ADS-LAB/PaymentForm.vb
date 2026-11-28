Imports MySql.Data.MySqlClient

Public Class PaymentForm
    Inherits Form

    Private cmbStudent As ComboBox
    Private cmbCourse As ComboBox
    Private txtAmount As TextBox
    Private cmbMethod As ComboBox
    Private txtReference As TextBox
    Private txtRemarks As TextBox
    Private lblSummary As Label
    Private dgvPayments As DataGridView
    Private btnSave As Button

    Public Sub New()
        InitializeLayout()
    End Sub

    Private Sub InitializeLayout()
        Me.Text = "Student Payments & Statement of Account"
        Me.Size = New Size(860, 560)
        Me.StartPosition = FormStartPosition.CenterParent

        Dim pnlTop As New Panel With {.Dock = DockStyle.Top, .Height = 235, .Padding = New Padding(12)}

        Dim lblSt As New Label With {.Text = "Student:", .Location = New Point(15, 18), .AutoSize = True}
        cmbStudent = New ComboBox With {.Location = New Point(130, 14), .Width = 280, .DropDownStyle = ComboBoxStyle.DropDownList}
        AddHandler cmbStudent.SelectedIndexChanged, AddressOf OnStudentChanged

        Dim lblCo As New Label With {.Text = "Course (Optional):", .Location = New Point(15, 52), .AutoSize = True}
        cmbCourse = New ComboBox With {.Location = New Point(130, 48), .Width = 280, .DropDownStyle = ComboBoxStyle.DropDownList}

        Dim lblAmt As New Label With {.Text = "Amount Paid:", .Location = New Point(15, 86), .AutoSize = True}
        txtAmount = New TextBox With {.Location = New Point(130, 82), .Width = 150}

        Dim lblMet As New Label With {.Text = "Payment Method:", .Location = New Point(15, 120), .AutoSize = True}
        cmbMethod = New ComboBox With {.Location = New Point(130, 116), .Width = 180, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbMethod.Items.AddRange(New Object() {"Cash", "GCash", "Bank Transfer", "Card"})
        cmbMethod.SelectedIndex = 0

        Dim lblRef As New Label With {.Text = "Reference / OR #:", .Location = New Point(15, 154), .AutoSize = True}
        txtReference = New TextBox With {.Location = New Point(130, 150), .Width = 200}

        Dim lblRem As New Label With {.Text = "Remarks:", .Location = New Point(15, 188), .AutoSize = True}
        txtRemarks = New TextBox With {.Location = New Point(130, 184), .Width = 280}

        btnSave = New Button With {.Text = "Record Payment", .Location = New Point(440, 182), .Width = 140, .Height = 30}
        AddHandler btnSave.Click, AddressOf BtnSave_Click

        lblSummary = New Label With {
            .Location = New Point(440, 16),
            .Size = New Size(390, 150),
            .BorderStyle = BorderStyle.FixedSingle,
            .Padding = New Padding(8),
            .Font = New Font("Segoe UI", 9.5F, FontStyle.Regular),
            .Text = "Select a student to view SOA summary."
        }

        pnlTop.Controls.AddRange(New Control() {
            lblSt, cmbStudent, lblCo, cmbCourse, lblAmt, txtAmount,
            lblMet, cmbMethod, lblRef, txtReference, lblRem, txtRemarks,
            btnSave, lblSummary
        })

        dgvPayments = New DataGridView With {
            .Dock = DockStyle.Fill,
            .ReadOnly = True,
            .AllowUserToAddRows = False,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        }

        Me.Controls.Add(dgvPayments)
        Me.Controls.Add(pnlTop)
    End Sub

    Private Sub PaymentForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudents()
    End Sub

    Private Sub LoadStudents()
        Using conn As MySqlConnection = DBConnection.GetConnection()
            conn.Open()
            Dim sql As String = "SELECT student_id, CONCAT(student_id, ' - ', first_name, ' ', last_name) AS full_name FROM Students ORDER BY last_name, first_name"
            Using da As New MySqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)
                cmbStudent.DisplayMember = "full_name"
                cmbStudent.ValueMember = "student_id"
                cmbStudent.DataSource = dt
            End Using
        End Using
    End Sub

    Private Sub OnStudentChanged(sender As Object, e As EventArgs)
        If cmbStudent.SelectedValue Is Nothing OrElse TypeOf cmbStudent.SelectedValue Is DataRowView Then Return
        Dim studentId As Integer = Convert.ToInt32(cmbStudent.SelectedValue)
        LoadEnrolledCourses(studentId)
        LoadStudentSoaAndHistory(studentId)
    End Sub

    Private Sub LoadEnrolledCourses(studentId As Integer)
        Using conn As MySqlConnection = DBConnection.GetConnection()
            conn.Open()
            Dim sql As String =
                "SELECT 0 AS course_id, '-- General / Tuition Payment --' AS label " &
                "UNION ALL " &
                "SELECT c.course_id, CONCAT(c.course_code, ' - ', c.course_name, ' (₱', FORMAT(c.course_fee, 2), ')') AS label " &
                "FROM Enrollments e INNER JOIN Courses c ON e.course_id = c.course_id " &
                "WHERE e.student_id = @sid"

            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@sid", studentId)
                Using da As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    cmbCourse.DisplayMember = "label"
                    cmbCourse.ValueMember = "course_id"
                    cmbCourse.DataSource = dt
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadStudentSoaAndHistory(studentId As Integer)
        Using conn As MySqlConnection = DBConnection.GetConnection()
            conn.Open()

            ' 1. Load SOA Summary
            Dim soaSql As String =
                "SELECT student_name, enrolled_courses, total_assessed, total_paid, balance_due, payment_status " &
                "FROM StudentSOA WHERE student_id = @sid"
            Using cmd As New MySqlCommand(soaSql, conn)
                cmd.Parameters.AddWithValue("@sid", studentId)
                Using rdr As MySqlDataReader = cmd.ExecuteReader()
                    If rdr.Read() Then
                        lblSummary.Text =
                            $"STATEMENT OF ACCOUNT{Environment.NewLine}" &
                            $"Student: {rdr("student_name")}{Environment.NewLine}" &
                            $"Enrolled Courses: {rdr("enrolled_courses")}{Environment.NewLine}" &
                            $"Total Assessed: ₱{Convert.ToDecimal(rdr("total_assessed")):N2}{Environment.NewLine}" &
                            $"Total Paid: ₱{Convert.ToDecimal(rdr("total_paid")):N2}{Environment.NewLine}" &
                            $"Balance Due: ₱{Convert.ToDecimal(rdr("balance_due")):N2}  [{rdr("payment_status")}]"
                    End If
                End Using
            End Using

            ' 2. Load Payment History
            Dim histSql As String =
                "SELECT p.payment_id AS `OR/ID`, COALESCE(c.course_code, 'General') AS `Course`, " &
                "p.amount_paid AS `Amount`, p.payment_method AS `Method`, p.reference_no AS `Reference`, " &
                "p.payment_date AS `Date`, p.remarks AS `Remarks` " &
                "FROM Payments p LEFT JOIN Courses c ON p.course_id = c.course_id " &
                "WHERE p.student_id = @sid ORDER BY p.payment_date DESC"
            Using cmdHist As New MySqlCommand(histSql, conn)
                cmdHist.Parameters.AddWithValue("@sid", studentId)
                Using da As New MySqlDataAdapter(cmdHist)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    dgvPayments.DataSource = dt
                End Using
            End Using
        End Using
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs)
        If cmbStudent.SelectedValue Is Nothing Then
            MessageBox.Show("Please select a student.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim amount As Decimal
        If Not Decimal.TryParse(txtAmount.Text.Trim(), amount) OrElse amount <= 0D Then
            MessageBox.Show("Enter a valid payment amount greater than 0.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim studentId As Integer = Convert.ToInt32(cmbStudent.SelectedValue)
        Dim courseId As Integer = Convert.ToInt32(cmbCourse.SelectedValue)

        Try
            Using conn As MySqlConnection = DBConnection.GetConnection()
                conn.Open()
                Dim sql As String =
                    "INSERT INTO Payments (student_id, course_id, amount_paid, payment_method, reference_no, remarks) " &
                    "VALUES (@sid, @cid, @amt, @method, @ref, @rem)"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@sid", studentId)
                    cmd.Parameters.AddWithValue("@cid", If(courseId = 0, DBNull.Value, CObj(courseId)))
                    cmd.Parameters.AddWithValue("@amt", amount)
                    cmd.Parameters.AddWithValue("@method", cmbMethod.SelectedItem.ToString())
                    cmd.Parameters.AddWithValue("@ref", If(String.IsNullOrWhiteSpace(txtReference.Text), DBNull.Value, CObj(txtReference.Text.Trim())))
                    cmd.Parameters.AddWithValue("@rem", If(String.IsNullOrWhiteSpace(txtRemarks.Text), DBNull.Value, CObj(txtRemarks.Text.Trim())))
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Payment recorded successfully.", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtAmount.Clear()
            txtReference.Clear()
            txtRemarks.Clear()
            LoadStudentSoaAndHistory(studentId)
        Catch ex As Exception
            ApplicationLogger.LogError("Payment insert failed: " & ex.Message, ex)
            MessageBox.Show("Unable to record payment: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class