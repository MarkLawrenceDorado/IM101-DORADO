<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SearchAdvancedForm
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Friend WithEvents flpSearchCriteria As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents cmbSearchEntity As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchKeyword As System.Windows.Forms.TextBox
    Friend WithEvents cmbSearchCourse As System.Windows.Forms.ComboBox
    Friend WithEvents chkFilterGrade As System.Windows.Forms.CheckBox
    Friend WithEvents numMinGrade As System.Windows.Forms.NumericUpDown
    Friend WithEvents numMaxGrade As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkFilterDates As System.Windows.Forms.CheckBox
    Friend WithEvents dtpSearchFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpSearchTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnRunSearch As System.Windows.Forms.Button
    Friend WithEvents btnClearSearch As System.Windows.Forms.Button
    Friend WithEvents lblSearchCount As System.Windows.Forms.Label
    Friend WithEvents dgvSearchResults As System.Windows.Forms.DataGridView

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
        Me.flpSearchCriteria = New System.Windows.Forms.FlowLayoutPanel()
        Me.cmbSearchEntity = New System.Windows.Forms.ComboBox()
        Me.txtSearchKeyword = New System.Windows.Forms.TextBox()
        Me.cmbSearchCourse = New System.Windows.Forms.ComboBox()
        Me.chkFilterGrade = New System.Windows.Forms.CheckBox()
        Me.numMinGrade = New System.Windows.Forms.NumericUpDown()
        Me.numMaxGrade = New System.Windows.Forms.NumericUpDown()
        Me.chkFilterDates = New System.Windows.Forms.CheckBox()
        Me.dtpSearchFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpSearchTo = New System.Windows.Forms.DateTimePicker()
        Me.btnRunSearch = New System.Windows.Forms.Button()
        Me.btnClearSearch = New System.Windows.Forms.Button()
        Me.lblSearchCount = New System.Windows.Forms.Label()
        Me.dgvSearchResults = New System.Windows.Forms.DataGridView()
        CType(Me.numMinGrade, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMaxGrade, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSearchResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SearchAdvancedForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1000, 620)
        Me.Name = "SearchAdvancedForm"
        Me.Text = "Advanced Search"
        CType(Me.numMinGrade, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMaxGrade, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSearchResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub
End Class