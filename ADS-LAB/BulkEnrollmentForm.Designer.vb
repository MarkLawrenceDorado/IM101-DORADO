<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BulkEnrollmentForm
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Friend WithEvents flpEnrollTop As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents cmbEnrollTargetCourse As System.Windows.Forms.ComboBox
    Friend WithEvents btnRefreshEnrollStudents As System.Windows.Forms.Button
    Friend WithEvents flpEnrollToolbar As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnUndoEnroll As System.Windows.Forms.Button
    Friend WithEvents btnRedoEnroll As System.Windows.Forms.Button
    Friend WithEvents btnSelectAllStudents As System.Windows.Forms.Button
    Friend WithEvents btnUncheckAllStudents As System.Windows.Forms.Button
    Friend WithEvents btnEnrollSelected As System.Windows.Forms.Button
    Friend WithEvents clbEnrollStudents As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblEnrollStatus As System.Windows.Forms.Label

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

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.flpEnrollTop = New System.Windows.Forms.FlowLayoutPanel()
        Me.cmbEnrollTargetCourse = New System.Windows.Forms.ComboBox()
        Me.btnRefreshEnrollStudents = New System.Windows.Forms.Button()
        Me.flpEnrollToolbar = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnUndoEnroll = New System.Windows.Forms.Button()
        Me.btnRedoEnroll = New System.Windows.Forms.Button()
        Me.btnSelectAllStudents = New System.Windows.Forms.Button()
        Me.btnUncheckAllStudents = New System.Windows.Forms.Button()
        Me.btnEnrollSelected = New System.Windows.Forms.Button()
        Me.clbEnrollStudents = New System.Windows.Forms.CheckedListBox()
        Me.lblEnrollStatus = New System.Windows.Forms.Label()
        Me.flpEnrollTop.SuspendLayout()
        Me.flpEnrollToolbar.SuspendLayout()
        Me.SuspendLayout()
        '
        'flpEnrollTop
        '
        Me.flpEnrollTop.AutoSize = True
        Me.flpEnrollTop.Controls.Add(Me.cmbEnrollTargetCourse)
        Me.flpEnrollTop.Controls.Add(Me.btnRefreshEnrollStudents)
        Me.flpEnrollTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.flpEnrollTop.Location = New System.Drawing.Point(0, 0)
        Me.flpEnrollTop.Name = "flpEnrollTop"
        Me.flpEnrollTop.Padding = New System.Windows.Forms.Padding(8)
        Me.flpEnrollTop.Size = New System.Drawing.Size(760, 45)
        '
        'cmbEnrollTargetCourse
        '
        Me.cmbEnrollTargetCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEnrollTargetCourse.Name = "cmbEnrollTargetCourse"
        Me.cmbEnrollTargetCourse.Size = New System.Drawing.Size(220, 23)
        '
        'btnRefreshEnrollStudents
        '
        Me.btnRefreshEnrollStudents.AutoSize = True
        Me.btnRefreshEnrollStudents.Name = "btnRefreshEnrollStudents"
        Me.btnRefreshEnrollStudents.Text = "Refresh students"
        '
        'flpEnrollToolbar
        '
        Me.flpEnrollToolbar.AutoSize = True
        Me.flpEnrollToolbar.Controls.Add(Me.btnUndoEnroll)
        Me.flpEnrollToolbar.Controls.Add(Me.btnRedoEnroll)
        Me.flpEnrollToolbar.Controls.Add(Me.btnSelectAllStudents)
        Me.flpEnrollToolbar.Controls.Add(Me.btnUncheckAllStudents)
        Me.flpEnrollToolbar.Controls.Add(Me.btnEnrollSelected)
        Me.flpEnrollToolbar.Dock = System.Windows.Forms.DockStyle.Top
        Me.flpEnrollToolbar.Location = New System.Drawing.Point(0, 45)
        Me.flpEnrollToolbar.Name = "flpEnrollToolbar"
        Me.flpEnrollToolbar.Padding = New System.Windows.Forms.Padding(8, 0, 8, 8)
        Me.flpEnrollToolbar.Size = New System.Drawing.Size(760, 37)
        '
        'btnUndoEnroll
        '
        Me.btnUndoEnroll.AutoSize = True
        Me.btnUndoEnroll.Name = "btnUndoEnroll"
        Me.btnUndoEnroll.Text = "Undo"
        '
        'btnRedoEnroll
        '
        Me.btnRedoEnroll.AutoSize = True
        Me.btnRedoEnroll.Name = "btnRedoEnroll"
        Me.btnRedoEnroll.Text = "Redo"
        '
        'btnSelectAllStudents
        '
        Me.btnSelectAllStudents.AutoSize = True
        Me.btnSelectAllStudents.Name = "btnSelectAllStudents"
        Me.btnSelectAllStudents.Text = "Select all"
        '
        'btnUncheckAllStudents
        '
        Me.btnUncheckAllStudents.AutoSize = True
        Me.btnUncheckAllStudents.Name = "btnUncheckAllStudents"
        Me.btnUncheckAllStudents.Text = "Uncheck all"
        '
        'btnEnrollSelected
        '
        Me.btnEnrollSelected.AutoSize = True
        Me.btnEnrollSelected.Name = "btnEnrollSelected"
        Me.btnEnrollSelected.Text = "Enroll selected"
        '
        'clbEnrollStudents
        '
        Me.clbEnrollStudents.CheckOnClick = True
        Me.clbEnrollStudents.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbEnrollStudents.Name = "clbEnrollStudents"
        '
        'lblEnrollStatus
        '
        Me.lblEnrollStatus.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblEnrollStatus.Name = "lblEnrollStatus"
        Me.lblEnrollStatus.Size = New System.Drawing.Size(760, 28)
        Me.lblEnrollStatus.Text = "Ready"
        '
        'BulkEnrollmentForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(760, 520)
        Me.Controls.Add(Me.clbEnrollStudents)
        Me.Controls.Add(Me.lblEnrollStatus)
        Me.Controls.Add(Me.flpEnrollToolbar)
        Me.Controls.Add(Me.flpEnrollTop)
        Me.Name = "BulkEnrollmentForm"
        Me.Text = "Bulk Enrollment"
        Me.flpEnrollTop.ResumeLayout(False)
        Me.flpEnrollTop.PerformLayout()
        Me.flpEnrollToolbar.ResumeLayout(False)
        Me.flpEnrollToolbar.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
End Class