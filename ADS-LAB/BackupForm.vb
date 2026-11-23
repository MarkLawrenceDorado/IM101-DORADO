Imports MySql.Data.MySqlClient
Imports System.Data

Public Class BackupForm

    Public Sub New()
        InitializeComponent()
        SessionManager.AttachStatusLabel(lblSession, Me)

        dgvBackupHistory.AllowUserToAddRows = False
        dgvBackupHistory.ReadOnly = True
        dgvBackupHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub BackupForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If SessionManager.RoleId <> 1 Then
            MessageBox.Show("Backups are available to administrators only.", "Access Denied",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
            Return
        End If

        LoadHistory()
    End Sub

    Private Sub LoadHistory()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter(
                "SELECT backup_id, backup_timestamp, description, status, created_at " &
                "FROM BackupLog ORDER BY backup_id DESC", conn)
            Dim table As New DataTable()
            adapter.Fill(table)
            dgvBackupHistory.DataSource = table

            dgvBackupHistory.Columns("backup_id").HeaderText = "ID"
            dgvBackupHistory.Columns("backup_timestamp").HeaderText = "Backup Stamp"
            dgvBackupHistory.Columns("description").HeaderText = "Description"
            dgvBackupHistory.Columns("status").HeaderText = "Status"
            dgvBackupHistory.Columns("created_at").HeaderText = "Created"

            If table.Rows.Count = 0 Then
                lblHistorySummary.Text = "No backups recorded yet."
            Else
                lblHistorySummary.Text = table.Rows.Count & " backup(s). Last: " &
                    Convert.ToDateTime(table.Rows(0)("created_at")).ToString("yyyy-MM-dd HH:mm:ss")
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading backup history: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnCreateBackup_Click(sender As Object, e As EventArgs) Handles btnCreateBackup.Click
        Dim description As String = txtBackupDescription.Text.Trim()
        If description = "" Then description = "Manual backup"

        If description.Length > 255 Then
            MessageBox.Show("The description must be 255 characters or fewer.", "Invalid Description")
            Return
        End If

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            Try
                Using cmd As New MySqlCommand("sp_CreateDatabaseBackup", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@backup_description", description)
                    cmd.ExecuteNonQuery()
                End Using

                lblBackupStatus.Text = "Backup recorded at " & Date.Now.ToString("HH:mm:ss") & "."
                lblBackupStatus.ForeColor = Color.Green
                txtBackupDescription.Clear()

            Catch ex As Exception
                ' Keep a record of the failed attempt in the history
                Using failCmd As New MySqlCommand(
                    "INSERT INTO BackupLog (backup_timestamp, description, status) " &
                    "VALUES (DATE_FORMAT(NOW(), '%Y%m%d_%H%i%s'), @d, 'FAILED')", conn)
                    failCmd.Parameters.AddWithValue("@d", description)
                    failCmd.ExecuteNonQuery()
                End Using

                lblBackupStatus.Text = "Backup failed: " & ex.Message
                lblBackupStatus.ForeColor = Color.Red
            End Try

        Catch ex As Exception
            lblBackupStatus.Text = "Database error: " & ex.Message
            lblBackupStatus.ForeColor = Color.Red
        Finally
            conn.Close()
        End Try

        LoadHistory()
    End Sub

    Private Sub btnRefreshHistory_Click(sender As Object, e As EventArgs) Handles btnRefreshHistory.Click
        LoadHistory()
    End Sub

    Private Sub dgvBackupHistory_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvBackupHistory.CellFormatting
        If e.Value Is Nothing Then Return
        If dgvBackupHistory.Columns(e.ColumnIndex).Name <> "status" Then Return

        Select Case e.Value.ToString().ToUpperInvariant()
            Case "COMPLETED"
                e.CellStyle.ForeColor = Color.Green
            Case "STARTED"
                e.CellStyle.ForeColor = Color.DarkOrange
            Case "FAILED"
                e.CellStyle.ForeColor = Color.Firebrick
        End Select
    End Sub

End Class