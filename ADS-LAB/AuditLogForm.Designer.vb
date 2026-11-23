<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AuditLogForm
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
        Me.chkUseDates = New System.Windows.Forms.CheckBox()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.lblUserFilter = New System.Windows.Forms.Label()
        Me.cmbUser = New System.Windows.Forms.ComboBox()
        Me.lblTableFilter = New System.Windows.Forms.Label()
        Me.cmbTable = New System.Windows.Forms.ComboBox()
        Me.lblActionFilter = New System.Windows.Forms.Label()
        Me.cmbAction = New System.Windows.Forms.ComboBox()
        Me.btnApplyFilters = New System.Windows.Forms.Button()
        Me.btnResetFilters = New System.Windows.Forms.Button()
        Me.btnExportPdf = New System.Windows.Forms.Button()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.dgvAudit = New System.Windows.Forms.DataGridView()
        Me.lblDetailsTitle = New System.Windows.Forms.Label()
        Me.txtDetails = New System.Windows.Forms.TextBox()
        Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblSession = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.dgvAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.statusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkUseDates
        '
        Me.chkUseDates.AutoSize = True
        Me.chkUseDates.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        Me.chkUseDates.Location = New System.Drawing.Point(20, 20)
        Me.chkUseDates.Name = "chkUseDates"
        Me.chkUseDates.Size = New System.Drawing.Size(102, 21)
        Me.chkUseDates.TabIndex = 0
        Me.chkUseDates.Text = "Filter Dates"
        Me.chkUseDates.UseVisualStyleBackColor = True
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        Me.lblFrom.Location = New System.Drawing.Point(132, 22)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(44, 17)
        Me.lblFrom.TabIndex = 1
        Me.lblFrom.Text = "From:"
        '
        'dtpFrom
        '
        Me.dtpFrom.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(180, 18)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(125, 24)
        Me.dtpFrom.TabIndex = 2
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        Me.lblTo.Location = New System.Drawing.Point(315, 22)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(27, 17)
        Me.lblTo.TabIndex = 3
        Me.lblTo.Text = "To:"
        '
        'dtpTo
        '
        Me.dtpTo.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(348, 18)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(125, 24)
        Me.dtpTo.TabIndex = 4
        '
        'lblUserFilter
        '
        Me.lblUserFilter.AutoSize = True
        Me.lblUserFilter.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        Me.lblUserFilter.Location = New System.Drawing.Point(490, 22)
        Me.lblUserFilter.Name = "lblUserFilter"
        Me.lblUserFilter.Size = New System.Drawing.Size(41, 17)
        Me.lblUserFilter.TabIndex = 5
        Me.lblUserFilter.Text = "User:"
        '
        'cmbUser
        '
        Me.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUser.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        Me.cmbUser.FormattingEnabled = True
        Me.cmbUser.Location = New System.Drawing.Point(536, 18)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(150, 25)
        Me.cmbUser.TabIndex = 6
        '
        'lblTableFilter
        '
        Me.lblTableFilter.AutoSize = True
        Me.lblTableFilter.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        Me.lblTableFilter.Location = New System.Drawing.Point(702, 22)
        Me.lblTableFilter.Name = "lblTableFilter"
        Me.lblTableFilter.Size = New System.Drawing.Size(46, 17)
        Me.lblTableFilter.TabIndex = 7
        Me.lblTableFilter.Text = "Table:"
        '
        'cmbTable
        '
        Me.cmbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTable.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        Me.cmbTable.FormattingEnabled = True
        Me.cmbTable.Location = New System.Drawing.Point(752, 18)
        Me.cmbTable.Name = "cmbTable"
        Me.cmbTable.Size = New System.Drawing.Size(150, 25)
        Me.cmbTable.TabIndex = 8
        '
        'lblActionFilter
        '
        Me.lblActionFilter.AutoSize = True
        Me.lblActionFilter.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        Me.lblActionFilter.Location = New System.Drawing.Point(918, 22)
        Me.lblActionFilter.Name = "lblActionFilter"
        Me.lblActionFilter.Size = New System.Drawing.Size(53, 17)
        Me.lblActionFilter.TabIndex = 9
        Me.lblActionFilter.Text = "Action:"
        '
        'cmbAction
        '
        Me.cmbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAction.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        Me.cmbAction.FormattingEnabled = True
        Me.cmbAction.Location = New System.Drawing.Point(976, 18)
        Me.cmbAction.Name = "cmbAction"
        Me.cmbAction.Size = New System.Drawing.Size(130, 25)
        Me.cmbAction.TabIndex = 10
        '
        'btnApplyFilters
        '
        Me.btnApplyFilters.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        Me.btnApplyFilters.Location = New System.Drawing.Point(20, 56)
        Me.btnApplyFilters.Name = "btnApplyFilters"
        Me.btnApplyFilters.Size = New System.Drawing.Size(115, 30)
        Me.btnApplyFilters.TabIndex = 11
        Me.btnApplyFilters.Text = "Apply Filters"
        Me.btnApplyFilters.UseVisualStyleBackColor = True
        '
        'btnResetFilters
        '
        Me.btnResetFilters.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        Me.btnResetFilters.Location = New System.Drawing.Point(145, 56)
        Me.btnResetFilters.Name = "btnResetFilters"
        Me.btnResetFilters.Size = New System.Drawing.Size(115, 30)
        Me.btnResetFilters.TabIndex = 12
        Me.btnResetFilters.Text = "Reset Filters"
        Me.btnResetFilters.UseVisualStyleBackColor = True
        '
        'btnExportPdf
        '
        Me.btnExportPdf.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExportPdf.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        Me.btnExportPdf.Location = New System.Drawing.Point(1166, 56)
        Me.btnExportPdf.Name = "btnExportPdf"
        Me.btnExportPdf.Size = New System.Drawing.Size(120, 30)
        Me.btnExportPdf.TabIndex = 13
        Me.btnExportPdf.Text = "Export PDF"
        Me.btnExportPdf.UseVisualStyleBackColor = True
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        Me.lblCount.Location = New System.Drawing.Point(276, 63)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(76, 17)
        Me.lblCount.TabIndex = 14
        Me.lblCount.Text = "0 record(s)"
        '
        'dgvAudit
        '
        Me.dgvAudit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvAudit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvAudit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAudit.Location = New System.Drawing.Point(20, 98)
        Me.dgvAudit.Name = "dgvAudit"
        Me.dgvAudit.RowHeadersWidth = 40
        Me.dgvAudit.Size = New System.Drawing.Size(1266, 340)
        Me.dgvAudit.TabIndex = 15
        '
        'lblDetailsTitle
        '
        Me.lblDetailsTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDetailsTitle.AutoSize = True
        Me.lblDetailsTitle.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        Me.lblDetailsTitle.Location = New System.Drawing.Point(20, 448)
        Me.lblDetailsTitle.Name = "lblDetailsTitle"
        Me.lblDetailsTitle.Size = New System.Drawing.Size(122, 17)
        Me.lblDetailsTitle.TabIndex = 16
        Me.lblDetailsTitle.Text = "Before / After Details"
        '
        'txtDetails
        '
        Me.txtDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDetails.Font = New System.Drawing.Font("Consolas", 9.0F)
        Me.txtDetails.Location = New System.Drawing.Point(20, 470)
        Me.txtDetails.Multiline = True
        Me.txtDetails.Name = "txtDetails"
        Me.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDetails.Size = New System.Drawing.Size(1266, 155)
        Me.txtDetails.TabIndex = 17
        '
        'statusStrip1
        '
        Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblSession})
        Me.statusStrip1.Location = New System.Drawing.Point(0, 636)
        Me.statusStrip1.Name = "statusStrip1"
        Me.statusStrip1.Size = New System.Drawing.Size(1306, 22)
        Me.statusStrip1.TabIndex = 18
        Me.statusStrip1.Text = "StatusStrip1"
        '
        'lblSession
        '
        Me.lblSession.Name = "lblSession"
        Me.lblSession.Size = New System.Drawing.Size(109, 17)
        Me.lblSession.Text = "Session: Active"
        '
        'AuditLogForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1306, 658)
        Me.Controls.Add(Me.statusStrip1)
        Me.Controls.Add(Me.txtDetails)
        Me.Controls.Add(Me.lblDetailsTitle)
        Me.Controls.Add(Me.dgvAudit)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.btnExportPdf)
        Me.Controls.Add(Me.btnResetFilters)
        Me.Controls.Add(Me.btnApplyFilters)
        Me.Controls.Add(Me.cmbAction)
        Me.Controls.Add(Me.lblActionFilter)
        Me.Controls.Add(Me.cmbTable)
        Me.Controls.Add(Me.lblTableFilter)
        Me.Controls.Add(Me.cmbUser)
        Me.Controls.Add(Me.lblUserFilter)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.lblTo)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.lblFrom)
        Me.Controls.Add(Me.chkUseDates)
        Me.MinimumSize = New System.Drawing.Size(1120, 550)
        Me.Name = "AuditLogForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Audit Log Viewer"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvAudit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.statusStrip1.ResumeLayout(False)
        Me.statusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents chkUseDates As System.Windows.Forms.CheckBox
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUserFilter As System.Windows.Forms.Label
    Friend WithEvents cmbUser As System.Windows.Forms.ComboBox
    Friend WithEvents lblTableFilter As System.Windows.Forms.Label
    Friend WithEvents cmbTable As System.Windows.Forms.ComboBox
    Friend WithEvents lblActionFilter As System.Windows.Forms.Label
    Friend WithEvents cmbAction As System.Windows.Forms.ComboBox
    Friend WithEvents btnApplyFilters As System.Windows.Forms.Button
    Friend WithEvents btnResetFilters As System.Windows.Forms.Button
    Friend WithEvents btnExportPdf As System.Windows.Forms.Button
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents dgvAudit As System.Windows.Forms.DataGridView
    Friend WithEvents lblDetailsTitle As System.Windows.Forms.Label
    Friend WithEvents txtDetails As System.Windows.Forms.TextBox
    Friend WithEvents statusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblSession As System.Windows.Forms.ToolStripStatusLabel
End Class