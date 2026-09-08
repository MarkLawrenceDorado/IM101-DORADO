<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TeacherDashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
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
        CType(dgvCourseOverview, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvCourseOverview
        ' 
        dgvCourseOverview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvCourseOverview.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvCourseOverview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCourseOverview.Location = New Point(12, 245)
        dgvCourseOverview.Name = "dgvCourseOverview"
        dgvCourseOverview.RowHeadersWidth = 51
        dgvCourseOverview.Size = New Size(766, 382)
        dgvCourseOverview.TabIndex = 0
        ' 
        ' cmbCourse
        ' 
        cmbCourse.FormattingEnabled = True
        cmbCourse.Location = New Point(153, 51)
        cmbCourse.Name = "cmbCourse"
        cmbCourse.Size = New Size(151, 28)
        cmbCourse.TabIndex = 1
        ' 
        ' cmbAssignmentType
        ' 
        cmbAssignmentType.FormattingEnabled = True
        cmbAssignmentType.Location = New Point(627, 51)
        cmbAssignmentType.Name = "cmbAssignmentType"
        cmbAssignmentType.Size = New Size(151, 28)
        cmbAssignmentType.TabIndex = 2
        ' 
        ' cmbStudent
        ' 
        cmbStudent.FormattingEnabled = True
        cmbStudent.Location = New Point(153, 119)
        cmbStudent.Name = "cmbStudent"
        cmbStudent.Size = New Size(151, 28)
        cmbStudent.TabIndex = 3
        ' 
        ' txtGrade
        ' 
        txtGrade.Location = New Point(627, 119)
        txtGrade.Name = "txtGrade"
        txtGrade.Size = New Size(151, 27)
        txtGrade.TabIndex = 4
        ' 
        ' btnSaveGrade
        ' 
        btnSaveGrade.Location = New Point(684, 169)
        btnSaveGrade.Name = "btnSaveGrade"
        btnSaveGrade.Size = New Size(94, 29)
        btnSaveGrade.TabIndex = 5
        btnSaveGrade.Text = "Save"
        btnSaveGrade.UseVisualStyleBackColor = True
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Location = New Point(389, 169)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(56, 20)
        lblStatus.TabIndex = 6
        lblStatus.Text = "Status: "
        ' 
        ' CourseLbl
        ' 
        CourseLbl.AutoSize = True
        CourseLbl.Location = New Point(13, 54)
        CourseLbl.Name = "CourseLbl"
        CourseLbl.Size = New Size(61, 20)
        CourseLbl.TabIndex = 7
        CourseLbl.Text = "Course: "
        ' 
        ' StudentLbl
        ' 
        StudentLbl.AutoSize = True
        StudentLbl.Location = New Point(17, 122)
        StudentLbl.Name = "StudentLbl"
        StudentLbl.Size = New Size(67, 20)
        StudentLbl.TabIndex = 8
        StudentLbl.Text = "Student: "
        ' 
        ' AssignmentTypesLbl
        ' 
        AssignmentTypesLbl.AutoSize = True
        AssignmentTypesLbl.Location = New Point(389, 54)
        AssignmentTypesLbl.Name = "AssignmentTypesLbl"
        AssignmentTypesLbl.Size = New Size(128, 20)
        AssignmentTypesLbl.TabIndex = 9
        AssignmentTypesLbl.Text = "Assignment Type: "
        ' 
        ' GradeLbl
        ' 
        GradeLbl.AutoSize = True
        GradeLbl.Location = New Point(389, 122)
        GradeLbl.Name = "GradeLbl"
        GradeLbl.Size = New Size(56, 20)
        GradeLbl.TabIndex = 10
        GradeLbl.Text = "Grade: "
        ' 
        ' TeacherDashboard
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 639)
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
        FormBorderStyle = FormBorderStyle.Fixed3D
        Name = "TeacherDashboard"
        Text = "Teacher Dashboard"
        WindowState = FormWindowState.Maximized
        CType(dgvCourseOverview, ComponentModel.ISupportInitialize).EndInit()
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
End Class
