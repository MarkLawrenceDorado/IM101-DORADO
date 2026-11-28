Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Diagnostics
Imports System.IO
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

    Private ReadOnly BackupFolder As String = Path.Combine(Application.StartupPath, "Backups")

    Private Sub btnCreateBackup_Click(sender As Object, e As EventArgs) Handles btnCreateBackup.Click
        Dim description As String = txtBackupDescription.Text.Trim()
        If description = "" Then description = "Manual backup"

        If description.Length > 255 Then
            MessageBox.Show("The description must be 255 characters or fewer.", "Invalid Description")
            Return
        End If

        Dim mysqldumpPath As String = "C:\xampp\mysql\bin\mysqldump.exe"
        If Not File.Exists(mysqldumpPath) Then
            MessageBox.Show("mysqldump.exe was not found at: " & mysqldumpPath, "Path Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Directory.CreateDirectory(BackupFolder)
        Dim stamp As String = Date.Now.ToString("yyyyMMdd_HHmmss")
        Dim sqlFilePath As String = Path.Combine(BackupFolder, "DB_Dorado_" & stamp & ".sql")

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            ' 1. Dump DB_Dorado to a .sql file using mysqldump.exe
            Dim dbHost As String = "127.0.0.1"
            Dim dbUser As String = "root"
            Dim dbPassword As String = ""
            Dim dbName As String = "DB_Dorado"

            Dim psi As New ProcessStartInfo() With {
            .FileName = mysqldumpPath,
            .UseShellExecute = False,
            .RedirectStandardOutput = True,
            .RedirectStandardError = True,
            .CreateNoWindow = True
        }

            If String.IsNullOrEmpty(dbPassword) Then
                psi.Arguments = "-h " & dbHost & " -u " & dbUser & " --routines --triggers " & dbName
            Else
                psi.Arguments = "-h " & dbHost & " -u " & dbUser & " -p" & dbPassword & " --routines --triggers " & dbName
            End If

            Using proc As Process = Process.Start(psi)
                Dim sqlDump As String = proc.StandardOutput.ReadToEnd()
                Dim stderr As String = proc.StandardError.ReadToEnd()
                proc.WaitForExit()

                If proc.ExitCode <> 0 Then
                    Throw New Exception("mysqldump failed: " & stderr)
                End If

                File.WriteAllText(sqlFilePath, sqlDump)
            End Using

            ' 2. Log the backup in BackupLog via your stored procedure
            Using cmd As New MySqlCommand("sp_CreateDatabaseBackup", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@backup_description", description)
                cmd.ExecuteNonQuery()
            End Using

            lblBackupStatus.Text = "Backup saved (" & Path.GetFileName(sqlFilePath) & ") at " & Date.Now.ToString("HH:mm:ss") & "."
            lblBackupStatus.ForeColor = Color.Green
            txtBackupDescription.Clear()

        Catch ex As Exception
            Try
                Using failCmd As New MySqlCommand(
                "INSERT INTO BackupLog (backup_timestamp, description, status) " &
                "VALUES (DATE_FORMAT(NOW(), '%Y%m%d_%H%i%s'), @d, 'FAILED')", conn)
                    failCmd.Parameters.AddWithValue("@d", description)
                    failCmd.ExecuteNonQuery()
                End Using
            Catch
                ' Ignore secondary logging failure if DB connection is down
            End Try

            lblBackupStatus.Text = "Backup failed: " & ex.Message
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
    Public Sub RestoreDatabaseFromSqlFile(sqlFilePath As String)
        If String.IsNullOrWhiteSpace(sqlFilePath) OrElse Not File.Exists(sqlFilePath) Then
            MessageBox.Show("Select a valid .sql backup file first.", "Missing File", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Restoring a backup will overwrite current data in DB_Dorado. Continue?",
                           "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If

        Dim mysqlExePath As String = "C:\xampp\mysql\bin\mysql.exe"
        Dim dbHost As String = "127.0.0.1"
        Dim dbUser As String = "root"
        Dim dbPassword As String = ""
        Dim dbName As String = "DB_Dorado"

        If Not File.Exists(mysqlExePath) Then
            MessageBox.Show("mysql.exe was not found at: " & mysqlExePath, "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            Dim psi As New ProcessStartInfo() With {
                .FileName = mysqlExePath,
                .UseShellExecute = False,
                .RedirectStandardInput = True,
                .RedirectStandardError = True,
                .CreateNoWindow = True
            }

            If String.IsNullOrEmpty(dbPassword) Then
                psi.Arguments = "-h " & dbHost & " -u " & dbUser & " " & dbName
            Else
                psi.Arguments = "-h " & dbHost & " -u " & dbUser & " -p" & dbPassword & " " & dbName
            End If

            Using proc As Process = Process.Start(psi)
                Using sqlReader As New StreamReader(sqlFilePath)
                    proc.StandardInput.Write(sqlReader.ReadToEnd())
                End Using
                proc.StandardInput.Close()

                Dim stderr As String = proc.StandardError.ReadToEnd()
                proc.WaitForExit()

                If proc.ExitCode <> 0 Then
                    MessageBox.Show("Restore failed: " & stderr, "Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
            End Using

            MessageBox.Show("Database restored successfully from backup.", "Restore Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            For Each frm As Form In Application.OpenForms
                Dim admin As AdminForm = TryCast(frm, AdminForm)
                If admin IsNot Nothing AndAlso Not admin.IsDisposed Then
                    admin.LoadAllLists()
                    admin.LoadEnrollmentLists()
                    admin.LoadAssignmentTypes()
                    admin.LoadAllGrades()
                    admin.LoadCoursesAndInstructors()
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("Error during restore: " & ex.Message, "Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnBrowseRestore_Click(sender As Object, e As EventArgs) Handles btnBrowseRestore.Click
        Using ofd As New OpenFileDialog()
            ofd.Title = "Select SQL Backup File"
            ofd.Filter = "SQL Backup Files (*.sql)|*.sql|All Files (*.*)|*.*"
            ofd.CheckFileExists = True

            If ofd.ShowDialog() = DialogResult.OK Then
                RestoreDatabaseFromSqlFile(ofd.FileName)
            End If
        End Using
    End Sub

    Private Sub btnRestoreSelected_Click(sender As Object, e As EventArgs) Handles btnRestoreSelected.Click
        If dgvBackupHistory.SelectedRows.Count = 0 Then
            MessageBox.Show("Select a backup row in the history grid first, or use Browse to pick a .sql file.",
                        "No Backup Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim row As DataGridViewRow = dgvBackupHistory.SelectedRows(0)
        Dim stampObj As Object = row.Cells("backup_timestamp").Value
        Dim stamp As String = If(stampObj Is Nothing OrElse IsDBNull(stampObj), "", stampObj.ToString().Trim())

        If String.IsNullOrWhiteSpace(stamp) Then
            MessageBox.Show("Selected backup does not have a valid backup timestamp.",
                        "Missing Timestamp", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim expectedSqlPath As String = Path.Combine(BackupFolder, "DB_Dorado_" & stamp & ".sql")

        If File.Exists(expectedSqlPath) Then
            lblBackupStatus.Text = "Restoring " & Path.GetFileName(expectedSqlPath) & "..."
            RestoreDatabaseFromSqlFile(expectedSqlPath)
            lblBackupStatus.Text = "Ready."
        Else
            MessageBox.Show("No .sql file found for timestamp '" & stamp & "' in:" & Environment.NewLine &
                        BackupFolder & Environment.NewLine & Environment.NewLine &
                        "Use 'Browse & Restore .sql' to select a backup file manually.",
                        "SQL File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
End Class