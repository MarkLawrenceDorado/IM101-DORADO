<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ReportsForm
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
        lblReportsTitle = New Label()
        btnExportCsv = New Button()
        btnExportPdf = New Button()
        tabReports = New TabControl()
        tabTranscript = New TabPage()
        lblSearchStudent = New Label()
        txtStudentSearch = New TextBox()
        btnSearchTranscript = New Button()
        lblTranscriptInfo = New Label()
        dgvTranscript = New DataGridView()
        tabAnalytics = New TabPage()
        lblCourseFilter = New Label()
        cmbCourseFilter = New ComboBox()
        lblInstructorFilter = New Label()
        cmbInstructorFilter = New ComboBox()
        chkUseDates = New CheckBox()
        lblFrom = New Label()
        dtpFrom = New DateTimePicker()
        lblTo = New Label()
        dtpTo = New DateTimePicker()
        btnApplyFilters = New Button()
        dgvAnalytics = New DataGridView()
        tabReports.SuspendLayout()
        tabTranscript.SuspendLayout()
        CType(dgvTranscript, ComponentModel.ISupportInitialize).BeginInit()
        tabAnalytics.SuspendLayout()
        CType(dgvAnalytics, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblReportsTitle
        ' 
        lblReportsTitle.AutoSize = True
        lblReportsTitle.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        lblReportsTitle.Location = New Point(16, 20)
        lblReportsTitle.Name = "lblReportsTitle"
        lblReportsTitle.Size = New Size(173, 21)
        lblReportsTitle.TabIndex = 3
        lblReportsTitle.Text = "Academic Reports"
        ' 
        ' btnExportCsv
        ' 
        btnExportCsv.Font = New Font("Segoe UI", 9.5F)
        btnExportCsv.Location = New Point(792, 18)
        btnExportCsv.Name = "btnExportCsv"
        btnExportCsv.Size = New Size(105, 30)
        btnExportCsv.TabIndex = 1
        btnExportCsv.Text = "Export CSV"
        btnExportCsv.UseVisualStyleBackColor = True
        ' 
        ' btnExportPdf
        ' 
        btnExportPdf.Font = New Font("Segoe UI", 9.5F)
        btnExportPdf.Location = New Point(906, 18)
        btnExportPdf.Name = "btnExportPdf"
        btnExportPdf.Size = New Size(105, 30)
        btnExportPdf.TabIndex = 2
        btnExportPdf.Text = "Export PDF"
        btnExportPdf.UseVisualStyleBackColor = True
        ' 
        ' tabReports
        ' 
        tabReports.Controls.Add(tabTranscript)
        tabReports.Controls.Add(tabAnalytics)
        tabReports.Font = New Font("Segoe UI", 9.5F)
        tabReports.Location = New Point(16, 58)
        tabReports.Name = "tabReports"
        tabReports.SelectedIndex = 0
        tabReports.Size = New Size(995, 725)
        tabReports.TabIndex = 0
        ' 
        ' tabTranscript
        ' 
        tabTranscript.Controls.Add(lblSearchStudent)
        tabTranscript.Controls.Add(txtStudentSearch)
        tabTranscript.Controls.Add(btnSearchTranscript)
        tabTranscript.Controls.Add(lblTranscriptInfo)
        tabTranscript.Controls.Add(dgvTranscript)
        tabTranscript.Location = New Point(4, 25)
        tabTranscript.Name = "tabTranscript"
        tabTranscript.Padding = New Padding(8)
        tabTranscript.Size = New Size(987, 696)
        tabTranscript.TabIndex = 0
        tabTranscript.Text = "Student Transcript"
        tabTranscript.UseVisualStyleBackColor = True
        ' 
        ' lblSearchStudent
        ' 
        lblSearchStudent.AutoSize = True
        lblSearchStudent.Location = New Point(20, 24)
        lblSearchStudent.Name = "lblSearchStudent"
        lblSearchStudent.Size = New Size(104, 17)
        lblSearchStudent.TabIndex = 4
        lblSearchStudent.Text = "Search Student:"
        ' 
        ' txtStudentSearch
        ' 
        txtStudentSearch.Location = New Point(132, 20)
        txtStudentSearch.Name = "txtStudentSearch"
        txtStudentSearch.Size = New Size(240, 25)
        txtStudentSearch.TabIndex = 0
        ' 
        ' btnSearchTranscript
        ' 
        btnSearchTranscript.Location = New Point(384, 19)
        btnSearchTranscript.Name = "btnSearchTranscript"
        btnSearchTranscript.Size = New Size(100, 28)
        btnSearchTranscript.TabIndex = 1
        btnSearchTranscript.Text = "Search"
        btnSearchTranscript.UseVisualStyleBackColor = True
        ' 
        ' lblTranscriptInfo
        ' 
        lblTranscriptInfo.AutoSize = True
        lblTranscriptInfo.Location = New Point(504, 24)
        lblTranscriptInfo.Name = "lblTranscriptInfo"
        lblTranscriptInfo.Size = New Size(0, 17)
        lblTranscriptInfo.TabIndex = 3
        ' 
        ' dgvTranscript
        ' 
        dgvTranscript.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvTranscript.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvTranscript.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTranscript.Location = New Point(16, 62)
        dgvTranscript.Name = "dgvTranscript"
        dgvTranscript.ReadOnly = True
        dgvTranscript.RowHeadersWidth = 45
        dgvTranscript.Size = New Size(955, 616)
        dgvTranscript.TabIndex = 2
        ' 
        ' tabAnalytics
        ' 
        tabAnalytics.Controls.Add(lblCourseFilter)
        tabAnalytics.Controls.Add(cmbCourseFilter)
        tabAnalytics.Controls.Add(lblInstructorFilter)
        tabAnalytics.Controls.Add(cmbInstructorFilter)
        tabAnalytics.Controls.Add(chkUseDates)
        tabAnalytics.Controls.Add(lblFrom)
        tabAnalytics.Controls.Add(dtpFrom)
        tabAnalytics.Controls.Add(lblTo)
        tabAnalytics.Controls.Add(dtpTo)
        tabAnalytics.Controls.Add(btnApplyFilters)
        tabAnalytics.Controls.Add(dgvAnalytics)
        tabAnalytics.Location = New Point(4, 25)
        tabAnalytics.Name = "tabAnalytics"
        tabAnalytics.Padding = New Padding(8)
        tabAnalytics.Size = New Size(987, 696)
        tabAnalytics.TabIndex = 1
        tabAnalytics.Text = "Course Analytics"
        tabAnalytics.UseVisualStyleBackColor = True
        ' 
        ' lblCourseFilter
        ' 
        lblCourseFilter.AutoSize = True
        lblCourseFilter.Location = New Point(20, 24)
        lblCourseFilter.Name = "lblCourseFilter"
        lblCourseFilter.Size = New Size(86, 17)
        lblCourseFilter.TabIndex = 9
        lblCourseFilter.Text = "Course Filter:"
        ' 
        ' cmbCourseFilter
        ' 
        cmbCourseFilter.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCourseFilter.FormattingEnabled = True
        cmbCourseFilter.Location = New Point(148, 20)
        cmbCourseFilter.Name = "cmbCourseFilter"
        cmbCourseFilter.Size = New Size(180, 25)
        cmbCourseFilter.TabIndex = 0
        ' 
        ' lblInstructorFilter
        ' 
        lblInstructorFilter.AutoSize = True
        lblInstructorFilter.Location = New Point(20, 62)
        lblInstructorFilter.Name = "lblInstructorFilter"
        lblInstructorFilter.Size = New Size(102, 17)
        lblInstructorFilter.TabIndex = 10
        lblInstructorFilter.Text = "Instructor Filter:"
        ' 
        ' cmbInstructorFilter
        ' 
        cmbInstructorFilter.DropDownStyle = ComboBoxStyle.DropDownList
        cmbInstructorFilter.FormattingEnabled = True
        cmbInstructorFilter.Location = New Point(148, 58)
        cmbInstructorFilter.Name = "cmbInstructorFilter"
        cmbInstructorFilter.Size = New Size(180, 25)
        cmbInstructorFilter.TabIndex = 1
        ' 
        ' chkUseDates
        ' 
        chkUseDates.AutoSize = True
        chkUseDates.Location = New Point(368, 22)
        chkUseDates.Name = "chkUseDates"
        chkUseDates.Size = New Size(140, 21)
        chkUseDates.TabIndex = 6
        chkUseDates.Text = "Filter by Enrollment"
        chkUseDates.UseVisualStyleBackColor = True
        ' 
        ' lblFrom
        ' 
        lblFrom.AutoSize = True
        lblFrom.Location = New Point(368, 54)
        lblFrom.Name = "lblFrom"
        lblFrom.Size = New Size(43, 17)
        lblFrom.TabIndex = 7
        lblFrom.Text = "From:"
        ' 
        ' dtpFrom
        ' 
        dtpFrom.Format = DateTimePickerFormat.Short
        dtpFrom.Location = New Point(422, 50)
        dtpFrom.Name = "dtpFrom"
        dtpFrom.Size = New Size(150, 25)
        dtpFrom.TabIndex = 2
        ' 
        ' lblTo
        ' 
        lblTo.AutoSize = True
        lblTo.Location = New Point(368, 86)
        lblTo.Name = "lblTo"
        lblTo.Size = New Size(26, 17)
        lblTo.TabIndex = 8
        lblTo.Text = "To:"
        ' 
        ' dtpTo
        ' 
        dtpTo.Format = DateTimePickerFormat.Short
        dtpTo.Location = New Point(422, 82)
        dtpTo.Name = "dtpTo"
        dtpTo.Size = New Size(150, 25)
        dtpTo.TabIndex = 3
        ' 
        ' btnApplyFilters
        ' 
        btnApplyFilters.Location = New Point(596, 48)
        btnApplyFilters.Name = "btnApplyFilters"
        btnApplyFilters.Size = New Size(120, 30)
        btnApplyFilters.TabIndex = 4
        btnApplyFilters.Text = "Apply Filters"
        btnApplyFilters.UseVisualStyleBackColor = True
        ' 
        ' dgvAnalytics
        ' 
        dgvAnalytics.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAnalytics.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvAnalytics.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAnalytics.Location = New Point(16, 122)
        dgvAnalytics.Name = "dgvAnalytics"
        dgvAnalytics.ReadOnly = True
        dgvAnalytics.RowHeadersWidth = 45
        dgvAnalytics.Size = New Size(955, 556)
        dgvAnalytics.TabIndex = 5
        ' 
        ' ReportsForm
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1027, 798)
        Controls.Add(lblReportsTitle)
        Controls.Add(btnExportPdf)
        Controls.Add(btnExportCsv)
        Controls.Add(tabReports)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "ReportsForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Reports"
        tabReports.ResumeLayout(False)
        tabTranscript.ResumeLayout(False)
        tabTranscript.PerformLayout()
        CType(dgvTranscript, ComponentModel.ISupportInitialize).EndInit()
        tabAnalytics.ResumeLayout(False)
        tabAnalytics.PerformLayout()
        CType(dgvAnalytics, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblReportsTitle As Label
    Friend WithEvents tabReports As TabControl
    Friend WithEvents tabTranscript As TabPage
    Friend WithEvents tabAnalytics As TabPage
    Friend WithEvents lblTranscriptInfo As Label
    Friend WithEvents dgvTranscript As DataGridView
    Friend WithEvents btnSearchTranscript As Button
    Friend WithEvents txtStudentSearch As TextBox
    Friend WithEvents chkUseDates As CheckBox
    Friend WithEvents dgvAnalytics As DataGridView
    Friend WithEvents btnApplyFilters As Button
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents cmbInstructorFilter As ComboBox
    Friend WithEvents cmbCourseFilter As ComboBox
    Friend WithEvents btnExportCsv As Button
    Friend WithEvents btnExportPdf As Button
    Friend WithEvents lblTo As Label
    Friend WithEvents lblFrom As Label
    Friend WithEvents lblInstructorFilter As Label
    Friend WithEvents lblCourseFilter As Label
    Friend WithEvents lblSearchStudent As Label
End Class