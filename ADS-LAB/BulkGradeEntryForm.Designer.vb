<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BulkGradeEntryForm
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Friend WithEvents flpGradeTop As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents cmbGradeCourse As System.Windows.Forms.ComboBox
    Friend WithEvents cmbGradeType As System.Windows.Forms.ComboBox
    Friend WithEvents btnLoadStudents As System.Windows.Forms.Button
    Friend WithEvents flpGradeToolbar As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnUndoGrade As System.Windows.Forms.Button
    Friend WithEvents btnRedoGrade As System.Windows.Forms.Button
    Friend WithEvents btnPasteGrades As System.Windows.Forms.Button
    Friend WithEvents btnClearGrades As System.Windows.Forms.Button
    Friend WithEvents btnSaveAllGrades As System.Windows.Forms.Button
    Friend WithEvents dgvBulkGrades As System.Windows.Forms.DataGridView
    Friend WithEvents lblGradeStatus As System.Windows.Forms.Label

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
        Me.flpGradeTop = New System.Windows.Forms.FlowLayoutPanel()
        Me.cmbGradeCourse = New System.Windows.Forms.ComboBox()
        Me.cmbGradeType = New System.Windows.Forms.ComboBox()
        Me.btnLoadStudents = New System.Windows.Forms.Button()
        Me.flpGradeToolbar = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnUndoGrade = New System.Windows.Forms.Button()
        Me.btnRedoGrade = New System.Windows.Forms.Button()
        Me.btnPasteGrades = New System.Windows.Forms.Button()
        Me.btnClearGrades = New System.Windows.Forms.Button()
        Me.btnSaveAllGrades = New System.Windows.Forms.Button()
        Me.dgvBulkGrades = New System.Windows.Forms.DataGridView()
        Me.lblGradeStatus = New System.Windows.Forms.Label()
        Me.flpGradeTop.SuspendLayout()
        Me.flpGradeToolbar.SuspendLayout()
        CType(Me.dgvBulkGrades, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'flpGradeTop
        '
        Me.flpGradeTop.AutoSize = True
        Me.flpGradeTop.Controls.Add(Me.cmbGradeCourse)
        Me.flpGradeTop.Controls.Add(Me.cmbGradeType)
        Me.flpGradeTop.Controls.Add(Me.btnLoadStudents)
        Me.flpGradeTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.flpGradeTop.Location = New System.Drawing.Point(0, 0)
        Me.flpGradeTop.Name = "flpGradeTop"
        Me.flpGradeTop.Padding = New System.Windows.Forms.Padding(8)
        Me.flpGradeTop.Size = New System.Drawing.Size(900, 45)
        '
        'cmbGradeCourse
        '
        Me.cmbGradeCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGradeCourse.Name = "cmbGradeCourse"
        Me.cmbGradeCourse.Size = New System.Drawing.Size(220, 23)
        '
        'cmbGradeType
        '
        Me.cmbGradeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGradeType.Name = "cmbGradeType"
        Me.cmbGradeType.Size = New System.Drawing.Size(180, 23)
        '
        'btnLoadStudents
        '
        Me.btnLoadStudents.AutoSize = True
        Me.btnLoadStudents.Name = "btnLoadStudents"
        Me.btnLoadStudents.Text = "Load students"
        '
        'flpGradeToolbar
        '
        Me.flpGradeToolbar.AutoSize = True
        Me.flpGradeToolbar.Controls.Add(Me.btnUndoGrade)
        Me.flpGradeToolbar.Controls.Add(Me.btnRedoGrade)
        Me.flpGradeToolbar.Controls.Add(Me.btnPasteGrades)
        Me.flpGradeToolbar.Controls.Add(Me.btnClearGrades)
        Me.flpGradeToolbar.Controls.Add(Me.btnSaveAllGrades)
        Me.flpGradeToolbar.Dock = System.Windows.Forms.DockStyle.Top
        Me.flpGradeToolbar.Location = New System.Drawing.Point(0, 45)
        Me.flpGradeToolbar.Name = "flpGradeToolbar"
        Me.flpGradeToolbar.Padding = New System.Windows.Forms.Padding(8, 0, 8, 8)
        Me.flpGradeToolbar.Size = New System.Drawing.Size(900, 37)
        '
        'btnUndoGrade
        '
        Me.btnUndoGrade.AutoSize = True
        Me.btnUndoGrade.Name = "btnUndoGrade"
        Me.btnUndoGrade.Text = "Undo"
        '
        'btnRedoGrade
        '
        Me.btnRedoGrade.AutoSize = True
        Me.btnRedoGrade.Name = "btnRedoGrade"
        Me.btnRedoGrade.Text = "Redo"
        '
        'btnPasteGrades
        '
        Me.btnPasteGrades.AutoSize = True
        Me.btnPasteGrades.Name = "btnPasteGrades"
        Me.btnPasteGrades.Text = "Paste grades"
        '
        'btnClearGrades
        '
        Me.btnClearGrades.AutoSize = True
        Me.btnClearGrades.Name = "btnClearGrades"
        Me.btnClearGrades.Text = "Clear"
        '
        'btnSaveAllGrades
        '
        Me.btnSaveAllGrades.AutoSize = True
        Me.btnSaveAllGrades.Name = "btnSaveAllGrades"
        Me.btnSaveAllGrades.Text = "Save all"
        '
        'dgvBulkGrades
        '
        Me.dgvBulkGrades.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvBulkGrades.Name = "dgvBulkGrades"
        '
        'lblGradeStatus
        '
        Me.lblGradeStatus.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblGradeStatus.Name = "lblGradeStatus"
        Me.lblGradeStatus.Size = New System.Drawing.Size(900, 28)
        Me.lblGradeStatus.Text = "Ready"
        '
        'BulkGradeEntryForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(900, 600)
        Me.Controls.Add(Me.dgvBulkGrades)
        Me.Controls.Add(Me.lblGradeStatus)
        Me.Controls.Add(Me.flpGradeToolbar)
        Me.Controls.Add(Me.flpGradeTop)
        Me.Name = "BulkGradeEntryForm"
        Me.Text = "Bulk Grade Entry"
        Me.flpGradeTop.ResumeLayout(False)
        Me.flpGradeTop.PerformLayout()
        Me.flpGradeToolbar.ResumeLayout(False)
        Me.flpGradeToolbar.PerformLayout()
        CType(Me.dgvBulkGrades, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
End Class