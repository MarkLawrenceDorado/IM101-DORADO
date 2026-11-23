Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Text
Imports System.Text.Json

Public Class AuditLogForm

    Private _filterText As String = ""

    Public Sub New()
        InitializeComponent()
        SessionManager.AttachStatusLabel(lblSession, Me)

        dgvAudit.AllowUserToAddRows = False
        dgvAudit.ReadOnly = True
        dgvAudit.MultiSelect = False
        dgvAudit.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        txtDetails.Multiline = True
        txtDetails.ReadOnly = True
        txtDetails.ScrollBars = ScrollBars.Vertical

        dtpFrom.Value = Date.Today.AddDays(-30)
        dtpTo.Value = Date.Today
        chkUseDates.Checked = True
    End Sub

    Private Sub AuditLogForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If SessionManager.RoleId <> 1 Then
            MessageBox.Show("The audit log is available to administrators only.", "Access Denied",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
            Return
        End If

        LoadFilters()
        LoadAudit()
    End Sub

    Private Function Txt(value As Object) As String
        Return If(value Is Nothing OrElse IsDBNull(value), "", value.ToString())
    End Function

    ' ---------- Filters ----------
    Private Sub LoadFilters()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            Dim users As New DataTable()
            Dim usersAdapter As New MySqlDataAdapter(
                "SELECT DISTINCT u.user_id, u.username FROM AuditLog a " &
                "INNER JOIN Users u ON a.user_id = u.user_id ORDER BY u.username", conn)
            usersAdapter.Fill(users)
            Dim allUsers As DataRow = users.NewRow()
            allUsers("user_id") = 0
            allUsers("username") = "(All users)"
            users.Rows.InsertAt(allUsers, 0)
            cmbUser.DataSource = users
            cmbUser.DisplayMember = "username"
            cmbUser.ValueMember = "user_id"

            Dim tables As New DataTable()
            Dim tablesAdapter As New MySqlDataAdapter(
                "SELECT DISTINCT table_name FROM AuditLog ORDER BY table_name", conn)
            tablesAdapter.Fill(tables)
            Dim allTables As DataRow = tables.NewRow()
            allTables("table_name") = "(All tables)"
            tables.Rows.InsertAt(allTables, 0)
            cmbTable.DataSource = tables
            cmbTable.DisplayMember = "table_name"
            cmbTable.ValueMember = "table_name"

            cmbAction.Items.Clear()
            cmbAction.Items.AddRange({"(All actions)", "INSERT", "UPDATE", "DELETE"})
            cmbAction.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show("Error loading filters: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub LoadAudit()
        Dim sql As New StringBuilder(
            "SELECT a.audit_id, a.action_timestamp, u.username, a.table_name, a.record_id, " &
            "a.action_type, a.old_values, a.new_values, a.ip_address " &
            "FROM AuditLog a INNER JOIN Users u ON a.user_id = u.user_id WHERE 1=1")

        Dim params As New List(Of MySqlParameter)

        If chkUseDates.Checked Then
            sql.Append(" AND a.action_timestamp >= @fromDate AND a.action_timestamp < @toDate")
            params.Add(New MySqlParameter("@fromDate", dtpFrom.Value.Date))
            params.Add(New MySqlParameter("@toDate", dtpTo.Value.Date.AddDays(1)))
        End If

        If cmbUser.SelectedIndex > 0 Then
            sql.Append(" AND a.user_id = @userId")
            params.Add(New MySqlParameter("@userId", Convert.ToInt32(cmbUser.SelectedValue)))
        End If

        If cmbTable.SelectedIndex > 0 Then
            sql.Append(" AND a.table_name = @tableName")
            params.Add(New MySqlParameter("@tableName", cmbTable.Text))
        End If

        If cmbAction.SelectedIndex > 0 Then
            sql.Append(" AND a.action_type = @actionType")
            params.Add(New MySqlParameter("@actionType", cmbAction.Text))
        End If

        sql.Append(" ORDER BY a.action_timestamp DESC, a.audit_id DESC")

        _filterText = "Period: " &
            If(chkUseDates.Checked, dtpFrom.Value.ToString("yyyy-MM-dd") & " to " & dtpTo.Value.ToString("yyyy-MM-dd"), "All dates") &
            "  |  User: " & If(cmbUser.SelectedIndex > 0, cmbUser.Text, "All") &
            "  |  Table: " & If(cmbTable.SelectedIndex > 0, cmbTable.Text, "All") &
            "  |  Action: " & If(cmbAction.SelectedIndex > 0, cmbAction.Text, "All")

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter(sql.ToString(), conn)
            adapter.SelectCommand.Parameters.AddRange(params.ToArray())

            Dim table As New DataTable()
            adapter.Fill(table)

            table.Columns.Add("changes", GetType(String))
            For Each row As DataRow In table.Rows
                row("changes") = DescribeChange(Txt(row("action_type")), ParseJson(row("old_values")), ParseJson(row("new_values")))
            Next

            dgvAudit.DataSource = table

            dgvAudit.Columns("old_values").Visible = False
            dgvAudit.Columns("new_values").Visible = False
            dgvAudit.Columns("audit_id").HeaderText = "ID"
            dgvAudit.Columns("action_timestamp").HeaderText = "Date / Time"
            dgvAudit.Columns("username").HeaderText = "User"
            dgvAudit.Columns("table_name").HeaderText = "Table"
            dgvAudit.Columns("record_id").HeaderText = "Record"
            dgvAudit.Columns("action_type").HeaderText = "Action"
            dgvAudit.Columns("changes").HeaderText = "Changes"
            dgvAudit.Columns("ip_address").HeaderText = "IP Address"
            dgvAudit.Columns("changes").DisplayIndex = 6

            lblCount.Text = table.Rows.Count & " record(s)"
            txtDetails.Clear()

        Catch ex As Exception
            MessageBox.Show("Error loading audit log: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnApplyFilters_Click(sender As Object, e As EventArgs) Handles btnApplyFilters.Click
        LoadAudit()
    End Sub

    Private Sub btnResetFilters_Click(sender As Object, e As EventArgs) Handles btnResetFilters.Click
        dtpFrom.Value = Date.Today.AddDays(-30)
        dtpTo.Value = Date.Today
        chkUseDates.Checked = True
        LoadFilters()
        LoadAudit()
    End Sub

    ' ---------- Readable before / after values ----------
    Private Function ParseJson(json As Object) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)
        If json Is Nothing OrElse IsDBNull(json) Then Return result

        Try
            Using doc As JsonDocument = JsonDocument.Parse(json.ToString())
                For Each prop As JsonProperty In doc.RootElement.EnumerateObject()
                    result(prop.Name) = prop.Value.ToString()
                Next
            End Using
        Catch
        End Try

        Return result
    End Function

    Private Function DescribeChange(action As String, oldValues As Dictionary(Of String, String),
                                    newValues As Dictionary(Of String, String)) As String
        Select Case action
            Case "INSERT"
                Return String.Join(", ", newValues.Select(Function(p) p.Key & "=" & p.Value))

            Case "DELETE"
                Return String.Join(", ", oldValues.Select(Function(p) p.Key & "=" & p.Value))

            Case Else
                Dim changes As New List(Of String)
                For Each pair As KeyValuePair(Of String, String) In newValues
                    Dim before As String = oldValues.GetValueOrDefault(pair.Key, "")
                    If before <> pair.Value Then
                        changes.Add(pair.Key & ": " & before & " -> " & pair.Value)
                    End If
                Next
                Return If(changes.Count = 0, "(no field values changed)", String.Join(", ", changes))
        End Select
    End Function

    Private Sub dgvAudit_SelectionChanged(sender As Object, e As EventArgs) Handles dgvAudit.SelectionChanged
        If dgvAudit.CurrentRow Is Nothing Then
            txtDetails.Clear()
            Return
        End If

        Dim view As DataRowView = TryCast(dgvAudit.CurrentRow.DataBoundItem, DataRowView)
        If view Is Nothing Then Return

        Dim row As DataRow = view.Row
        Dim oldValues As Dictionary(Of String, String) = ParseJson(row("old_values"))
        Dim newValues As Dictionary(Of String, String) = ParseJson(row("new_values"))

        Dim sb As New StringBuilder()
        sb.AppendLine("Audit #" & Txt(row("audit_id")) & ": " & Txt(row("action_type")) &
                      " on " & Txt(row("table_name")) & " (record " & Txt(row("record_id")) & ")")
        sb.AppendLine("By " & Txt(row("username")) & " from " & Txt(row("ip_address")) &
                      " at " & Convert.ToDateTime(row("action_timestamp")).ToString("yyyy-MM-dd HH:mm:ss"))
        sb.AppendLine()

        If oldValues.Count > 0 Then
            sb.AppendLine("BEFORE")
            For Each pair As KeyValuePair(Of String, String) In oldValues
                sb.AppendLine("    " & pair.Key & ": " & pair.Value)
            Next
            sb.AppendLine()
        End If

        If newValues.Count > 0 Then
            sb.AppendLine("AFTER")
            For Each pair As KeyValuePair(Of String, String) In newValues
                sb.AppendLine("    " & pair.Key & ": " & pair.Value)
            Next
        End If

        txtDetails.Text = sb.ToString()
    End Sub

    ' ---------- PDF export ----------
    Private Sub btnExportPdf_Click(sender As Object, e As EventArgs) Handles btnExportPdf.Click
        If dgvAudit.Rows.Count = 0 Then
            MessageBox.Show("There are no audit records to export.", "No Data")
            Return
        End If

        Using dialog As New SaveFileDialog()
            dialog.Filter = "PDF file (*.pdf)|*.pdf"
            dialog.FileName = "audit_trail_" & Date.Now.ToString("yyyyMMdd_HHmm") & ".pdf"
            If dialog.ShowDialog() <> DialogResult.OK Then Return

            Try
                Dim subtitle As String = "Generated by " & SessionManager.Username & " on " &
                                         Date.Now.ToString("yyyy-MM-dd HH:mm") & "   " & _filterText
                PdfExport.ExportGrid(dgvAudit, "Audit Trail Report", dialog.FileName, subtitle)
                MessageBox.Show("Audit trail exported to PDF.", "Export Complete")
            Catch ex As Exception
                MessageBox.Show("Export failed: " & ex.Message, "Error")
            End Try
        End Using
    End Sub

End Class