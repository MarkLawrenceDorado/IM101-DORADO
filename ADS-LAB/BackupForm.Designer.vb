<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BackupForm
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.txtBackupDescription = New System.Windows.Forms.TextBox()
        Me.btnCreateBackup = New System.Windows.Forms.Button()
        Me.btnRefreshHistory = New System.Windows.Forms.Button()
        Me.lblBackupStatus = New System.Windows.Forms.Label()
        Me.lblHistorySummary = New System.Windows.Forms.Label()
        Me.dgvBackupHistory = New System.Windows.Forms.DataGridView()
        Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblSession = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.dgvBackupHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.statusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        Me.lblDescription.Location = New System.Drawing.Point(24, 24)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(87, 17)
        Me.lblDescription.TabIndex = 0
        Me.lblDescription.Text = "Description:"
        '
        'txtBackupDescription
        '
        Me.txtBackupDescription.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        Me.txtBackupDescription.Location = New System.Drawing.Point(120, 20)
        Me.txtBackupDescription.Name = "txtBackupDescription"
        Me.txtBackupDescription.Size = New System.Drawing.Size(420, 25)
        Me.txtBackupDescription.TabIndex = 1
        '
        'btnCreateBackup
        '
        Me.btnCreateBackup.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        Me.btnCreateBackup.Location = New System.Drawing.Point(554, 19)
        Me.btnCreateBackup.Name = "btnCreateBackup"
        Me.btnCreateBackup.Size = New System.Drawing.Size(135, 28)
        Me.btnCreateBackup.TabIndex = 2
        Me.btnCreateBackup.Text = "Create Backup"
        Me.btnCreateBackup.UseVisualStyleBackColor = True
        '
        'btnRefreshHistory
        '
        Me.btnRefreshHistory.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        Me.btnRefreshHistory.Location = New System.Drawing.Point(699, 19)
        Me.btnRefreshHistory.Name = "btnRefreshHistory"
        Me.btnRefreshHistory.Size = New System.Drawing.Size(125, 28)
        Me.btnRefreshHistory.TabIndex = 3
        Me.btnRefreshHistory.Text = "Refresh History"
        Me.btnRefreshHistory.UseVisualStyleBackColor = True
        ' Inside InitializeComponent() — right after btnRefreshHistory setup:
        Me.btnBrowseRestore = New System.Windows.Forms.Button()
        Me.btnBrowseRestore.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        Me.btnBrowseRestore.Location = New System.Drawing.Point(24, 52)
        Me.btnBrowseRestore.Name = "btnBrowseRestore"
        Me.btnBrowseRestore.Size = New System.Drawing.Size(175, 28)
        Me.btnBrowseRestore.TabIndex = 8
        Me.btnBrowseRestore.Text = "Browse && Restore .sql"
        Me.btnBrowseRestore.UseVisualStyleBackColor = True

        Me.btnRestoreSelected = New System.Windows.Forms.Button()
        Me.btnRestoreSelected.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        Me.btnRestoreSelected.Location = New System.Drawing.Point(208, 52)
        Me.btnRestoreSelected.Name = "btnRestoreSelected"
        Me.btnRestoreSelected.Size = New System.Drawing.Size(155, 28)
        Me.btnRestoreSelected.TabIndex = 9
        Me.btnRestoreSelected.Text = "Restore Selected"
        Me.btnRestoreSelected.UseVisualStyleBackColor = True
        '
        'lblBackupStatus
        '
        Me.lblBackupStatus.AutoSize = True
        Me.lblBackupStatus.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        Me.lblBackupStatus.Location = New System.Drawing.Point(24, 56)
        Me.lblBackupStatus.Name = "lblBackupStatus"
        Me.lblBackupStatus.Size = New System.Drawing.Size(0, 17)
        Me.lblBackupStatus.TabIndex = 4
        '
        'lblHistorySummary
        '
        Me.lblHistorySummary.AutoSize = True
        Me.lblHistorySummary.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        Me.lblHistorySummary.Location = New System.Drawing.Point(24, 84)
        Me.lblHistorySummary.Name = "lblHistorySummary"
        Me.lblHistorySummary.Size = New System.Drawing.Size(113, 17)
        Me.lblHistorySummary.TabIndex = 5
        Me.lblHistorySummary.Text = "Backup History"
        '
        'dgvBackupHistory
        '
        Me.dgvBackupHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvBackupHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvBackupHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBackupHistory.Location = New System.Drawing.Point(24, 110)
        Me.dgvBackupHistory.Name = "dgvBackupHistory"
        Me.dgvBackupHistory.RowHeadersWidth = 40
        Me.dgvBackupHistory.Size = New System.Drawing.Size(936, 440)
        Me.dgvBackupHistory.TabIndex = 6
        '
        'statusStrip1
        '
        Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblSession})
        Me.statusStrip1.Location = New System.Drawing.Point(0, 564)
        Me.statusStrip1.Name = "statusStrip1"
        Me.statusStrip1.Size = New System.Drawing.Size(984, 22)
        Me.statusStrip1.TabIndex = 7
        Me.statusStrip1.Text = "StatusStrip1"
        '
        'lblSession
        '
        Me.lblSession.Name = "lblSession"
        Me.lblSession.Size = New System.Drawing.Size(109, 17)
        Me.lblSession.Text = "Session: Active"
        '
        'BackupForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 586)
        Me.Controls.Add(Me.statusStrip1)
        Me.Controls.Add(Me.dgvBackupHistory)
        Me.Controls.Add(Me.lblHistorySummary)
        Me.Controls.Add(Me.lblBackupStatus)
        Me.Controls.Add(Me.btnRefreshHistory)
        Me.Controls.Add(Me.btnCreateBackup)
        Me.Controls.Add(Me.txtBackupDescription)
        Me.Controls.Add(Me.lblDescription)
        ' Inside Me.Controls.Add(...) block:
        Me.Controls.Add(Me.btnRestoreSelected)
        Me.Controls.Add(Me.btnBrowseRestore)
        Me.MinimumSize = New System.Drawing.Size(860, 480)
        Me.Name = "BackupForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Database Backup Utility"
        CType(Me.dgvBackupHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.statusStrip1.ResumeLayout(False)
        Me.statusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents txtBackupDescription As System.Windows.Forms.TextBox
    Friend WithEvents btnCreateBackup As System.Windows.Forms.Button
    Friend WithEvents btnRefreshHistory As System.Windows.Forms.Button
    Friend WithEvents lblBackupStatus As System.Windows.Forms.Label
    Friend WithEvents lblHistorySummary As System.Windows.Forms.Label
    Friend WithEvents dgvBackupHistory As System.Windows.Forms.DataGridView
    Friend WithEvents statusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblSession As System.Windows.Forms.ToolStripStatusLabel
    ' At the bottom of BackupForm.Designer.vb:
    Friend WithEvents btnBrowseRestore As System.Windows.Forms.Button
    Friend WithEvents btnRestoreSelected As System.Windows.Forms.Button
End Class