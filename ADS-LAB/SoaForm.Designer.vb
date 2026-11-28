<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SoaForm
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
        lblTitle = New Label()
        lblSelectStudent = New Label()
        cmbSelectStudent = New ComboBox()
        btnRefreshSoa = New Button()
        dgvSoaItems = New DataGridView()
        pnlSummary = New Panel()
        lblTotalAssessed = New Label()
        lblTotalPaid = New Label()
        lblBalanceDue = New Label()
        CType(dgvSoaItems, ComponentModel.ISupportInitialize).BeginInit()
        pnlSummary.SuspendLayout()
        SuspendLayout()
        '
        ' lblTitle
        '
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 13.0F, FontStyle.Bold)
        lblTitle.Location = New Point(24, 18)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(245, 23)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Statement of Account (SOA)"
        '
        ' lblSelectStudent
        '
        lblSelectStudent.AutoSize = True
        lblSelectStudent.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblSelectStudent.Location = New Point(24, 58)
        lblSelectStudent.Name = "lblSelectStudent"
        lblSelectStudent.Size = New Size(73, 17)
        lblSelectStudent.TabIndex = 1
        lblSelectStudent.Text = "Student:"
        '
        ' cmbSelectStudent
        '
        cmbSelectStudent.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSelectStudent.Font = New Font("Segoe UI", 9.5F)
        cmbSelectStudent.FormattingEnabled = True
        cmbSelectStudent.Location = New Point(104, 54)
        cmbSelectStudent.Name = "cmbSelectStudent"
        cmbSelectStudent.Size = New Size(320, 25)
        cmbSelectStudent.TabIndex = 2
        '
        ' btnRefreshSoa
        '
        btnRefreshSoa.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnRefreshSoa.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btnRefreshSoa.Location = New Point(912, 52)
        btnRefreshSoa.Name = "btnRefreshSoa"
        btnRefreshSoa.Size = New Size(120, 30)
        btnRefreshSoa.TabIndex = 3
        btnRefreshSoa.Text = "Refresh"
        btnRefreshSoa.UseVisualStyleBackColor = True
        '
        ' dgvSoaItems
        '
        dgvSoaItems.AllowUserToAddRows = False
        dgvSoaItems.AllowUserToDeleteRows = False
        dgvSoaItems.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvSoaItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvSoaItems.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvSoaItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvSoaItems.Location = New Point(24, 92)
        dgvSoaItems.Name = "dgvSoaItems"
        dgvSoaItems.ReadOnly = True
        dgvSoaItems.RowHeadersWidth = 45
        dgvSoaItems.Size = New Size(1008, 440)
        dgvSoaItems.TabIndex = 4
        '
        ' pnlSummary
        '
        pnlSummary.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlSummary.BackColor = Color.FromArgb(245, 247, 250)
        pnlSummary.BorderStyle = BorderStyle.FixedSingle
        pnlSummary.Controls.Add(lblTotalAssessed)
        pnlSummary.Controls.Add(lblTotalPaid)
        pnlSummary.Controls.Add(lblBalanceDue)
        pnlSummary.Location = New Point(24, 544)
        pnlSummary.Name = "pnlSummary"
        pnlSummary.Size = New Size(1008, 86)
        pnlSummary.TabIndex = 5
        '
        ' lblTotalAssessed
        '
        lblTotalAssessed.AutoSize = True
        lblTotalAssessed.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)
        lblTotalAssessed.Location = New Point(20, 16)
        lblTotalAssessed.Name = "lblTotalAssessed"
        lblTotalAssessed.Size = New Size(185, 19)
        lblTotalAssessed.TabIndex = 0
        lblTotalAssessed.Text = "Total Assessment: 0.00"
        '
        ' lblTotalPaid
        '
        lblTotalPaid.AutoSize = True
        lblTotalPaid.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)
        lblTotalPaid.ForeColor = Color.Green
        lblTotalPaid.Location = New Point(340, 16)
        lblTotalPaid.Name = "lblTotalPaid"
        lblTotalPaid.Size = New Size(135, 19)
        lblTotalPaid.TabIndex = 1
        lblTotalPaid.Text = "Total Paid: 0.00"
        '
        ' lblBalanceDue
        '
        lblBalanceDue.AutoSize = True
        lblBalanceDue.Font = New Font("Segoe UI", 11.5F, FontStyle.Bold)
        lblBalanceDue.ForeColor = Color.FromArgb(180, 30, 30)
        lblBalanceDue.Location = New Point(20, 48)
        lblBalanceDue.Name = "lblBalanceDue"
        lblBalanceDue.Size = New Size(165, 21)
        lblBalanceDue.TabIndex = 2
        lblBalanceDue.Text = "Balance Due: 0.00"
        '
        ' SoaForm
        '
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1056, 650)
        Controls.Add(pnlSummary)
        Controls.Add(dgvSoaItems)
        Controls.Add(btnRefreshSoa)
        Controls.Add(cmbSelectStudent)
        Controls.Add(lblSelectStudent)
        Controls.Add(lblTitle)
        MinimumSize = New Size(780, 480)
        Name = "SoaForm"
        StartPosition = FormStartPosition.CenterParent
        Text = "Statement of Account"
        CType(dgvSoaItems, ComponentModel.ISupportInitialize).EndInit()
        pnlSummary.ResumeLayout(False)
        pnlSummary.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSelectStudent As Label
    Friend WithEvents cmbSelectStudent As ComboBox
    Friend WithEvents btnRefreshSoa As Button
    Friend WithEvents dgvSoaItems As DataGridView
    Friend WithEvents pnlSummary As Panel
    Friend WithEvents lblTotalAssessed As Label
    Friend WithEvents lblTotalPaid As Label
    Friend WithEvents lblBalanceDue As Label
End Class