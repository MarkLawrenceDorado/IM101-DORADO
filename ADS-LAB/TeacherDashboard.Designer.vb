<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TeacherDashboard
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
        Me.components = New System.ComponentModel.Container()
        dgvCourseOverview = New DataGridView()
        cmbCourse = New ComboBox()
        cmbAssignmentType = New ComboBox()
        cmbStudent = New ComboBox()
        txtGrade = New TextBox()
        btnSaveGrade = New Button()
        lblStatus = New Label()
        CourseLbl = New Label()
        StudentLbl = New Label()
        AssignmentTypesLbl = New Label()
        GradeLbl = New Label()
        btnCalculateFinal = New Button()
        dgvFinalGrades = New DataGridView()
        txtCourseSearch = New TextBox()
        btnCourseSearch = New Button()
        btnCourseSearchClear = New Button()
        lblCourseSearch = New Label()
        btnReportsForm = New Button()
        lblOverviewTitle = New Label()
        lblFinalGradesTitle = New Label()
        lblSearchStatus = New Label()
        errProvider = New ErrorProvider(Me.components)
        statusStrip1 = New StatusStrip()
        lblSession = New ToolStripStatusLabel()
        CType(dgvCourseOverview, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvFinalGrades, ComponentModel.ISupportInitialize).BeginInit()
        CType(errProvider, ComponentModel.ISupportInitialize).BeginInit()
        statusStrip1.SuspendLayout()
        SuspendLayout()
        '
        ' errProvider
        '
        errProvider.ContainerControl = Me
        ' 
        ' CourseLbl
        ' 
        CourseLbl.AutoSize = True
        CourseLbl.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        CourseLbl.Location = New Point(24, 28)
        CourseLbl.Name = "CourseLbl"
        CourseLbl.Size = New Size(62, 17)
        CourseLbl.TabIndex = 7
        CourseLbl.Text = "Course:"
        ' 
        ' cmbCourse
        ' 
        cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCourse.Font = New Font("Segoe UI", 9.5F)
        cmbCourse.FormattingEnabled = True
        cmbCourse.Location = New Point(156, 24)
        cmbCourse.Name = "cmbCourse"
        cmbCourse.Size = New Size(180, 25)
        cmbCourse.TabIndex = 1
        ' 
        ' AssignmentTypesLbl
        ' 
        AssignmentTypesLbl.AutoSize = True
        AssignmentTypesLbl.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        AssignmentTypesLbl.Location = New Point(368, 28)
        AssignmentTypesLbl.Name = "AssignmentTypesLbl"
        AssignmentTypesLbl.Size = New Size(127, 17)
        AssignmentTypesLbl.TabIndex = 9
        AssignmentTypesLbl.Text = "Assignment Type:"
        ' 
        ' cmbAssignmentType
        ' 
        cmbAssignmentType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbAssignmentType.Font = New Font("Segoe UI", 9.5F)
        cmbAssignmentType.FormattingEnabled = True
        cmbAssignmentType.Location = New Point(505, 24)
        cmbAssignmentType.Name = "cmbAssignmentType"
        cmbAssignmentType.Size = New Size(180, 25)
        cmbAssignmentType.TabIndex = 2
        ' 
        ' StudentLbl
        ' 
        StudentLbl.AutoSize = True
        StudentLbl.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        StudentLbl.Location = New Point(24, 68)
        StudentLbl.Name = "StudentLbl"
        StudentLbl.Size = New Size(66, 17)
        StudentLbl.TabIndex = 8
        StudentLbl.Text = "Student:"
        ' 
        ' cmbStudent
        ' 
        cmbStudent.DropDownStyle = ComboBoxStyle.DropDownList
        cmbStudent.Font = New Font("Segoe UI", 9.5F)
        cmbStudent.FormattingEnabled = True
        cmbStudent.Location = New Point(156, 64)
        cmbStudent.Name = "cmbStudent"
        cmbStudent.Size = New Size(180, 25)
        cmbStudent.TabIndex = 3
        ' 
        ' GradeLbl
        ' 
        GradeLbl.AutoSize = True
        GradeLbl.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        GradeLbl.Location = New Point(368, 68)
        GradeLbl.Name = "GradeLbl"
        GradeLbl.Size = New Size(56, 17)
        GradeLbl.TabIndex = 10
        GradeLbl.Text = "Grade:"
        ' 
        ' txtGrade
        ' 
        txtGrade.Font = New Font("Segoe UI", 9.5F)
        txtGrade.Location = New Point(505, 64)
        txtGrade.Name = "txtGrade"
        txtGrade.Size = New Size(180, 25)
        txtGrade.TabIndex = 4
        ' 
        ' btnSaveGrade
        ' 
        btnSaveGrade.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btnSaveGrade.Location = New Point(505, 102)
        btnSaveGrade.Name = "btnSaveGrade"
        btnSaveGrade.Size = New Size(110, 32)
        btnSaveGrade.TabIndex = 5
        btnSaveGrade.Text = "Save Grade"
        btnSaveGrade.UseVisualStyleBackColor = True
        ' 
        ' lblStatus (Left side — for Save Grade messages)
        ' 
        lblStatus.AutoSize = True
        lblStatus.Font = New Font("Segoe UI", 9.5F)
        lblStatus.Location = New Point(24, 108)
        lblStatus.MaximumSize = New Size(460, 0)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(0, 17)
        lblStatus.TabIndex = 6
        ' 
        ' lblSearchStatus (Right side — for Search Course messages)
        ' 
        lblSearchStatus.AutoSize = True
        lblSearchStatus.Font = New Font("Segoe UI", 9.5F)
        lblSearchStatus.Location = New Point(936, 56)
        lblSearchStatus.Name = "lblSearchStatus"
        lblSearchStatus.Size = New Size(0, 17)
        lblSearchStatus.TabIndex = 15
        ' 
        ' lblCourseSearch
        ' 
        lblCourseSearch.AutoSize = True
        lblCourseSearch.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblCourseSearch.Location = New Point(820, 28)
        lblCourseSearch.Name = "lblCourseSearch"
        lblCourseSearch.Size = New Size(106, 17)
        lblCourseSearch.TabIndex = 16
        lblCourseSearch.Text = "Search Course:"
        ' 
        ' txtCourseSearch
        ' 
        txtCourseSearch.Font = New Font("Segoe UI", 9.5F)
        txtCourseSearch.Location = New Point(936, 24)
        txtCourseSearch.Name = "txtCourseSearch"
        txtCourseSearch.Size = New Size(180, 25)
        txtCourseSearch.TabIndex = 13
        ' 
        ' btnCourseSearch
        ' 
        btnCourseSearch.Font = New Font("Segoe UI", 9.5F)
        btnCourseSearch.Location = New Point(1126, 23)
        btnCourseSearch.Name = "btnCourseSearch"
        btnCourseSearch.Size = New Size(90, 28)
        btnCourseSearch.TabIndex = 14
        btnCourseSearch.Text = "Search"
        btnCourseSearch.UseVisualStyleBackColor = True
        ' 
        ' btnCourseSearchClear
        ' 
        btnCourseSearchClear.Font = New Font("Segoe UI", 9.5F)
        btnCourseSearchClear.Location = New Point(1224, 23)
        btnCourseSearchClear.Name = "btnCourseSearchClear"
        btnCourseSearchClear.Size = New Size(90, 28)
        btnCourseSearchClear.TabIndex = 15
        btnCourseSearchClear.Text = "Clear"
        btnCourseSearchClear.UseVisualStyleBackColor = True
        ' 
        ' btnReportsForm
        ' 
        btnReportsForm.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btnReportsForm.Location = New Point(1340, 23)
        btnReportsForm.Name = "btnReportsForm"
        btnReportsForm.Size = New Size(120, 28)
        btnReportsForm.TabIndex = 17
        btnReportsForm.Text = "Reports"
        btnReportsForm.UseVisualStyleBackColor = True
        ' 
        ' lblOverviewTitle
        ' 
        lblOverviewTitle.AutoSize = True
        lblOverviewTitle.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)
        lblOverviewTitle.Location = New Point(24, 152)
        lblOverviewTitle.Name = "lblOverviewTitle"
        lblOverviewTitle.Size = New Size(141, 19)
        lblOverviewTitle.TabIndex = 18
        lblOverviewTitle.Text = "Course Overview"
        ' 
        ' dgvCourseOverview
        ' 
        dgvCourseOverview.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        dgvCourseOverview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvCourseOverview.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvCourseOverview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCourseOverview.Location = New Point(24, 180)
        dgvCourseOverview.Name = "dgvCourseOverview"
        dgvCourseOverview.ReadOnly = True
        dgvCourseOverview.RowHeadersWidth = 45
        dgvCourseOverview.Size = New Size(760, 456)
        dgvCourseOverview.TabIndex = 0
        ' 
        ' lblFinalGradesTitle
        ' 
        lblFinalGradesTitle.AutoSize = True
        lblFinalGradesTitle.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)
        lblFinalGradesTitle.Location = New Point(820, 152)
        lblFinalGradesTitle.Name = "lblFinalGradesTitle"
        lblFinalGradesTitle.Size = New Size(106, 19)
        lblFinalGradesTitle.TabIndex = 19
        lblFinalGradesTitle.Text = "Final Grades"
        ' 
        ' btnCalculateFinal
        ' 
        btnCalculateFinal.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnCalculateFinal.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btnCalculateFinal.Location = New Point(1320, 147)
        btnCalculateFinal.Name = "btnCalculateFinal"
        btnCalculateFinal.Size = New Size(140, 28)
        btnCalculateFinal.TabIndex = 11
        btnCalculateFinal.Text = "Calculate Final"
        btnCalculateFinal.UseVisualStyleBackColor = True
        ' 
        ' dgvFinalGrades
        ' 
        dgvFinalGrades.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvFinalGrades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvFinalGrades.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvFinalGrades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvFinalGrades.Location = New Point(820, 180)
        dgvFinalGrades.Name = "dgvFinalGrades"
        dgvFinalGrades.RowHeadersWidth = 45
        dgvFinalGrades.Size = New Size(640, 456)
        dgvFinalGrades.TabIndex = 12
        '
        ' statusStrip1
        '
        statusStrip1.Items.AddRange(New ToolStripItem() {lblSession})
        statusStrip1.Location = New Point(0, 662)
        statusStrip1.Name = "statusStrip1"
        statusStrip1.Size = New Size(1484, 22)
        statusStrip1.TabIndex = 20
        statusStrip1.Text = "StatusStrip1"
        '
        ' lblSession
        '
        lblSession.Name = "lblSession"
        lblSession.Size = New Size(109, 17)
        lblSession.Text = "Session: Active"
        ' 
        ' TeacherDashboard
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1484, 684)
        Controls.Add(statusStrip1)
        Controls.Add(lblSearchStatus)
        Controls.Add(lblFinalGradesTitle)
        Controls.Add(lblOverviewTitle)
        Controls.Add(btnReportsForm)
        Controls.Add(lblCourseSearch)
        Controls.Add(btnCourseSearchClear)
        Controls.Add(btnCourseSearch)
        Controls.Add(txtCourseSearch)
        Controls.Add(dgvFinalGrades)
        Controls.Add(btnCalculateFinal)
        Controls.Add(GradeLbl)
        Controls.Add(AssignmentTypesLbl)
        Controls.Add(StudentLbl)
        Controls.Add(CourseLbl)
        Controls.Add(lblStatus)
        Controls.Add(btnSaveGrade)
        Controls.Add(txtGrade)
        Controls.Add(cmbStudent)
        Controls.Add(cmbAssignmentType)
        Controls.Add(cmbCourse)
        Controls.Add(dgvCourseOverview)
        FormBorderStyle = FormBorderStyle.Sizable
        MinimumSize = New Size(1100, 550)
        Name = "TeacherDashboard"
        Text = "Teacher Dashboard"
        WindowState = FormWindowState.Maximized
        CType(dgvCourseOverview, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvFinalGrades, ComponentModel.ISupportInitialize).EndInit()
        CType(errProvider, ComponentModel.ISupportInitialize).EndInit()
        statusStrip1.ResumeLayout(False)
        statusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents dgvCourseOverview As DataGridView
    Friend WithEvents cmbCourse As ComboBox
    Friend WithEvents cmbAssignmentType As ComboBox
    Friend WithEvents cmbStudent As ComboBox
    Friend WithEvents txtGrade As TextBox
    Friend WithEvents btnSaveGrade As Button
    Friend WithEvents lblStatus As Label
    Friend WithEvents CourseLbl As Label
    Friend WithEvents StudentLbl As Label
    Friend WithEvents AssignmentTypesLbl As Label
    Friend WithEvents GradeLbl As Label
    Friend WithEvents btnCalculateFinal As Button
    Friend WithEvents dgvFinalGrades As DataGridView
    Friend WithEvents txtCourseSearch As TextBox
    Friend WithEvents btnCourseSearch As Button
    Friend WithEvents btnCourseSearchClear As Button
    Friend WithEvents lblCourseSearch As Label
    Friend WithEvents btnReportsForm As Button
    Friend WithEvents lblOverviewTitle As Label
    Friend WithEvents lblFinalGradesTitle As Label
    Friend WithEvents lblSearchStatus As Label
    Friend WithEvents errProvider As ErrorProvider
    Friend WithEvents statusStrip1 As StatusStrip
    Friend WithEvents lblSession As ToolStripStatusLabel
End Class