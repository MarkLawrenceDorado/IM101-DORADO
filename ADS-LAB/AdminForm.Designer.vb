<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AdminForm
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
        txtNewUsername = New TextBox()
        txtNewPassword = New TextBox()
        txtFirstName = New TextBox()
        txtLastName = New TextBox()
        txtDepartment = New TextBox()
        lblStatus = New Label()
        btnAddUser = New Button()
        addTeacherPanel = New Panel()
        txtEmail = New TextBox()
        lblEmail = New Label()
        dtpDateOfBirth = New DateTimePicker()
        lblDateOfBirth = New Label()
        UserTypeLbl = New Label()
        cmbUserType = New ComboBox()
        lblDepartment = New Label()
        addLastNameLbl = New Label()
        addFirstNameLbl = New Label()
        addNewPasswordLbl = New Label()
        addNewUsernameLbl = New Label()
        addUserLabel = New Label()
        TabControl1 = New TabControl()
        usersTab = New TabPage()
        dgvUsers = New DataGridView()
        txtSearchUsers = New TextBox()
        lblSearchUser = New Label()
        instructorsTab = New TabPage()
        dgvInstructors = New DataGridView()
        txtSearchInstructors = New TextBox()
        lblSearchInstructor = New Label()
        studentsTab = New TabPage()
        dgvStudents = New DataGridView()
        txtSearchStudents = New TextBox()
        lblSearchStudent = New Label()
        Panel1 = New Panel()
        resetPasswordLbl = New Label()
        newResetPasswordLbl = New Label()
        targetUsernameLbl = New Label()
        btnResetPassword = New Button()
        txtNewResetPassword = New TextBox()
        txtTargetUsername = New TextBox()
        btnRefresh = New Button()
        dgvAssignmentTypes = New DataGridView()
        txtTypeName = New TextBox()
        txtDefaultWeight = New TextBox()
        btnAddType = New Button()
        btnUpdateType = New Button()
        btnDeleteType = New Button()
        dgvAllGrades = New DataGridView()
        tabsController = New TabControl()
        AddUserTab = New TabPage()
        usersTab1 = New TabPage()
        resetPasswordTab = New TabPage()
        AssignmentTypesTab = New TabPage()
        lblSearchTypes = New Label()
        txtSearchTypes = New TextBox()
        defaultWeightLbl = New Label()
        typeNameLbl = New Label()
        GradesTab = New TabPage()
        lblCourseGrade = New Label()
        lblStudentGrade = New Label()
        btnClearGradeSearch = New Button()
        btnSearchGrades = New Button()
        txtGradeCourse = New TextBox()
        txtGradeStudent = New TextBox()
        Enrollment = New TabPage()
        lblEnrollStatus = New Label()
        btnBulkEnroll = New Button()
        cmbBulkCourse = New ComboBox()
        clbBulkStudents = New CheckedListBox()
        btnEnrollStudent = New Button()
        cmbEnrollCourse = New ComboBox()
        cmbEnrollStudent = New ComboBox()
        lblSingleEnroll = New Label()
        lblBulkEnroll = New Label()
        CoursesTab = New TabPage()
        lblCourseCode = New Label()
        txtCourseCode = New TextBox()
        lblCourseName = New Label()
        txtCourseName = New TextBox()
        lblCourseInstructor = New Label()
        cmbCourseInstructor = New ComboBox()
        Me.lblCourseFee = New System.Windows.Forms.Label()
        Me.txtCourseFee = New System.Windows.Forms.TextBox()
        btnAddCourse = New Button()
        Me.btnUpdateCourse = New System.Windows.Forms.Button()
        Me.btnDeleteCourse = New System.Windows.Forms.Button()
        lblCourseStatus = New Label()
        dgvCourses = New DataGridView()
        btnReportsForm = New Button()
        btnAuditLog = New Button()
        btnBackups = New Button()
        errProvider = New ErrorProvider(Me.components)
        statusStrip1 = New StatusStrip()
        lblSession = New ToolStripStatusLabel()
        addTeacherPanel.SuspendLayout()
        TabControl1.SuspendLayout()
        usersTab.SuspendLayout()
        CType(dgvUsers, ComponentModel.ISupportInitialize).BeginInit()
        instructorsTab.SuspendLayout()
        CType(dgvInstructors, ComponentModel.ISupportInitialize).BeginInit()
        studentsTab.SuspendLayout()
        CType(dgvStudents, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        CType(dgvAssignmentTypes, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvAllGrades, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvCourses, ComponentModel.ISupportInitialize).BeginInit()
        tabsController.SuspendLayout()
        AddUserTab.SuspendLayout()
        usersTab1.SuspendLayout()
        resetPasswordTab.SuspendLayout()
        AssignmentTypesTab.SuspendLayout()
        GradesTab.SuspendLayout()
        Enrollment.SuspendLayout()
        CoursesTab.SuspendLayout()
        CType(errProvider, ComponentModel.ISupportInitialize).BeginInit()
        statusStrip1.SuspendLayout()
        SuspendLayout()
        Me.btnUpdateCourse.Visible = True
        Me.btnDeleteCourse.Visible = True
        '
        ' errProvider
        '
        errProvider.ContainerControl = Me
        '
        ' btnAuditLog
        '
        btnAuditLog.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnAuditLog.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btnAuditLog.Location = New Point(1290, 18)
        btnAuditLog.Name = "btnAuditLog"
        btnAuditLog.Size = New Size(125, 34)
        btnAuditLog.TabIndex = 20
        btnAuditLog.Text = "Audit Log"
        btnAuditLog.UseVisualStyleBackColor = True
        '
        ' btnBackups
        '
        btnBackups.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnBackups.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btnBackups.Location = New Point(1425, 18)
        btnBackups.Name = "btnBackups"
        btnBackups.Size = New Size(125, 34)
        btnBackups.TabIndex = 21
        btnBackups.Text = "Backups"
        btnBackups.UseVisualStyleBackColor = True
        ' 
        ' btnReportsForm
        ' 
        btnReportsForm.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnReportsForm.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btnReportsForm.Location = New Point(1560, 18)
        btnReportsForm.Name = "btnReportsForm"
        btnReportsForm.Size = New Size(130, 34)
        btnReportsForm.TabIndex = 19
        btnReportsForm.Text = "Reports"
        btnReportsForm.UseVisualStyleBackColor = True
        ' 
        ' tabsController
        ' 
        tabsController.Alignment = TabAlignment.Left
        tabsController.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tabsController.Controls.Add(AddUserTab)
        tabsController.Controls.Add(usersTab1)
        tabsController.Controls.Add(resetPasswordTab)
        tabsController.Controls.Add(AssignmentTypesTab)
        tabsController.Controls.Add(GradesTab)
        tabsController.Controls.Add(Enrollment)
        tabsController.Controls.Add(CoursesTab)
        tabsController.DrawMode = TabDrawMode.OwnerDrawFixed
        tabsController.Font = New Font("Segoe UI", 10.0F)
        tabsController.ItemSize = New Size(42, 160)
        tabsController.Location = New Point(16, 62)
        tabsController.Multiline = True
        tabsController.Name = "tabsController"
        tabsController.Padding = New Point(0, 0)
        tabsController.SelectedIndex = 0
        tabsController.Size = New Size(1674, 838)
        tabsController.SizeMode = TabSizeMode.Fixed
        tabsController.TabIndex = 18
        ' 
        ' AddUserTab
        ' 
        AddUserTab.BackColor = Color.Transparent
        AddUserTab.Controls.Add(addTeacherPanel)
        AddUserTab.Location = New Point(164, 4)
        AddUserTab.Name = "AddUserTab"
        AddUserTab.Padding = New Padding(8)
        AddUserTab.Size = New Size(1506, 830)
        AddUserTab.TabIndex = 0
        AddUserTab.Text = "Add User"
        ' 
        ' addTeacherPanel
        ' 
        addTeacherPanel.Controls.Add(addUserLabel)
        addTeacherPanel.Controls.Add(UserTypeLbl)
        addTeacherPanel.Controls.Add(cmbUserType)
        addTeacherPanel.Controls.Add(addNewUsernameLbl)
        addTeacherPanel.Controls.Add(txtNewUsername)
        addTeacherPanel.Controls.Add(addNewPasswordLbl)
        addTeacherPanel.Controls.Add(txtNewPassword)
        addTeacherPanel.Controls.Add(addFirstNameLbl)
        addTeacherPanel.Controls.Add(txtFirstName)
        addTeacherPanel.Controls.Add(addLastNameLbl)
        addTeacherPanel.Controls.Add(txtLastName)
        addTeacherPanel.Controls.Add(lblDepartment)
        addTeacherPanel.Controls.Add(txtDepartment)
        addTeacherPanel.Controls.Add(lblDateOfBirth)
        addTeacherPanel.Controls.Add(dtpDateOfBirth)
        addTeacherPanel.Controls.Add(lblEmail)
        addTeacherPanel.Controls.Add(txtEmail)
        addTeacherPanel.Controls.Add(lblStatus)
        addTeacherPanel.Controls.Add(btnAddUser)
        addTeacherPanel.Location = New Point(28, 24)
        addTeacherPanel.Name = "addTeacherPanel"
        addTeacherPanel.Size = New Size(520, 620)
        addTeacherPanel.TabIndex = 7
        ' 
        ' addUserLabel
        ' 
        addUserLabel.AutoSize = True
        addUserLabel.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        addUserLabel.Location = New Point(24, 18)
        addUserLabel.Name = "addUserLabel"
        addUserLabel.Size = New Size(170, 21)
        addUserLabel.TabIndex = 7
        addUserLabel.Text = "Create Teacher / Student"
        ' 
        ' UserTypeLbl
        ' 
        UserTypeLbl.AutoSize = True
        UserTypeLbl.Location = New Point(24, 68)
        UserTypeLbl.Name = "UserTypeLbl"
        UserTypeLbl.Size = New Size(82, 19)
        UserTypeLbl.TabIndex = 14
        UserTypeLbl.Text = "User Type:"
        ' 
        ' cmbUserType
        ' 
        cmbUserType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUserType.FormattingEnabled = True
        cmbUserType.Location = New Point(185, 64)
        cmbUserType.Name = "cmbUserType"
        cmbUserType.Size = New Size(260, 27)
        cmbUserType.TabIndex = 13
        ' 
        ' addNewUsernameLbl
        ' 
        addNewUsernameLbl.AutoSize = True
        addNewUsernameLbl.Location = New Point(24, 114)
        addNewUsernameLbl.Name = "addNewUsernameLbl"
        addNewUsernameLbl.Size = New Size(86, 19)
        addNewUsernameLbl.TabIndex = 8
        addNewUsernameLbl.Text = "Username:"
        ' 
        ' txtNewUsername
        ' 
        txtNewUsername.Location = New Point(185, 110)
        txtNewUsername.Name = "txtNewUsername"
        txtNewUsername.Size = New Size(260, 27)
        txtNewUsername.TabIndex = 0
        ' 
        ' addNewPasswordLbl
        ' 
        addNewPasswordLbl.AutoSize = True
        addNewPasswordLbl.Location = New Point(24, 160)
        addNewPasswordLbl.Name = "addNewPasswordLbl"
        addNewPasswordLbl.Size = New Size(79, 19)
        addNewPasswordLbl.TabIndex = 9
        addNewPasswordLbl.Text = "Password:"
        ' 
        ' txtNewPassword
        ' 
        txtNewPassword.Location = New Point(185, 156)
        txtNewPassword.Name = "txtNewPassword"
        txtNewPassword.PasswordChar = "*"c
        txtNewPassword.Size = New Size(260, 27)
        txtNewPassword.TabIndex = 1
        ' 
        ' addFirstNameLbl
        ' 
        addFirstNameLbl.AutoSize = True
        addFirstNameLbl.Location = New Point(24, 206)
        addFirstNameLbl.Name = "addFirstNameLbl"
        addFirstNameLbl.Size = New Size(90, 19)
        addFirstNameLbl.TabIndex = 10
        addFirstNameLbl.Text = "First Name:"
        ' 
        ' txtFirstName
        ' 
        txtFirstName.Location = New Point(185, 202)
        txtFirstName.Name = "txtFirstName"
        txtFirstName.Size = New Size(260, 27)
        txtFirstName.TabIndex = 2
        ' 
        ' addLastNameLbl
        ' 
        addLastNameLbl.AutoSize = True
        addLastNameLbl.Location = New Point(24, 252)
        addLastNameLbl.Name = "addLastNameLbl"
        addLastNameLbl.Size = New Size(89, 19)
        addLastNameLbl.TabIndex = 11
        addLastNameLbl.Text = "Last Name:"
        ' 
        ' txtLastName
        ' 
        txtLastName.Location = New Point(185, 248)
        txtLastName.Name = "txtLastName"
        txtLastName.Size = New Size(260, 27)
        txtLastName.TabIndex = 3
        ' 
        ' lblDepartment
        ' 
        lblDepartment.AutoSize = True
        lblDepartment.Location = New Point(24, 298)
        lblDepartment.Name = "lblDepartment"
        lblDepartment.Size = New Size(101, 19)
        lblDepartment.TabIndex = 12
        lblDepartment.Text = "Department:"
        ' 
        ' txtDepartment
        ' 
        txtDepartment.Location = New Point(185, 294)
        txtDepartment.Name = "txtDepartment"
        txtDepartment.Size = New Size(260, 27)
        txtDepartment.TabIndex = 4
        ' 
        ' lblDateOfBirth
        ' 
        lblDateOfBirth.AutoSize = True
        lblDateOfBirth.Location = New Point(24, 298)
        lblDateOfBirth.Name = "lblDateOfBirth"
        lblDateOfBirth.Size = New Size(106, 19)
        lblDateOfBirth.TabIndex = 15
        lblDateOfBirth.Text = "Date of Birth:"
        ' 
        ' dtpDateOfBirth
        ' 
        dtpDateOfBirth.Format = DateTimePickerFormat.Short
        dtpDateOfBirth.Location = New Point(185, 294)
        dtpDateOfBirth.Name = "dtpDateOfBirth"
        dtpDateOfBirth.Size = New Size(260, 27)
        dtpDateOfBirth.TabIndex = 16
        ' 
        ' lblEmail
        ' 
        lblEmail.AutoSize = True
        lblEmail.Location = New Point(24, 344)
        lblEmail.Name = "lblEmail"
        lblEmail.Size = New Size(52, 19)
        lblEmail.TabIndex = 17
        lblEmail.Text = "Email:"
        ' 
        ' txtEmail
        ' 
        txtEmail.Location = New Point(185, 340)
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(260, 27)
        txtEmail.TabIndex = 18
        ' 
        ' btnAddUser
        ' 
        btnAddUser.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnAddUser.Location = New Point(185, 392)
        btnAddUser.Name = "btnAddUser"
        btnAddUser.Size = New Size(140, 36)
        btnAddUser.TabIndex = 6
        btnAddUser.Text = "Add User"
        btnAddUser.UseVisualStyleBackColor = True
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Location = New Point(24, 448)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(0, 19)
        lblStatus.TabIndex = 5
        ' 
        ' usersTab1
        ' 
        usersTab1.BackColor = Color.Transparent
        usersTab1.Controls.Add(btnRefresh)
        usersTab1.Controls.Add(TabControl1)
        usersTab1.Location = New Point(164, 4)
        usersTab1.Name = "usersTab1"
        usersTab1.Padding = New Padding(8)
        usersTab1.Size = New Size(1506, 830)
        usersTab1.TabIndex = 1
        usersTab1.Text = "Users"
        ' 
        ' btnRefresh
        ' 
        btnRefresh.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnRefresh.Location = New Point(1366, 16)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(120, 32)
        btnRefresh.TabIndex = 10
        btnRefresh.Text = "Refresh Lists"
        btnRefresh.UseVisualStyleBackColor = True
        ' 
        ' TabControl1
        ' 
        TabControl1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TabControl1.Controls.Add(usersTab)
        TabControl1.Controls.Add(instructorsTab)
        TabControl1.Controls.Add(studentsTab)
        TabControl1.Location = New Point(16, 58)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(1470, 756)
        TabControl1.TabIndex = 8
        ' 
        ' usersTab
        ' 
        usersTab.Controls.Add(lblSearchUser)
        usersTab.Controls.Add(txtSearchUsers)
        usersTab.Controls.Add(dgvUsers)
        usersTab.Location = New Point(4, 28)
        usersTab.Name = "usersTab"
        usersTab.Padding = New Padding(8)
        usersTab.Size = New Size(1462, 724)
        usersTab.TabIndex = 0
        usersTab.Text = "Users"
        usersTab.UseVisualStyleBackColor = True
        ' 
        ' lblSearchUser
        ' 
        lblSearchUser.AutoSize = True
        lblSearchUser.Location = New Point(16, 20)
        lblSearchUser.Name = "lblSearchUser"
        lblSearchUser.Size = New Size(97, 19)
        lblSearchUser.TabIndex = 13
        lblSearchUser.Text = "Search User:"
        ' 
        ' txtSearchUsers
        ' 
        txtSearchUsers.Location = New Point(122, 16)
        txtSearchUsers.Name = "txtSearchUsers"
        txtSearchUsers.Size = New Size(240, 27)
        txtSearchUsers.TabIndex = 12
        ' 
        ' dgvUsers
        ' 
        dgvUsers.AllowUserToAddRows = False
        dgvUsers.AllowUserToDeleteRows = False
        dgvUsers.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvUsers.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvUsers.Location = New Point(16, 56)
        dgvUsers.Name = "dgvUsers"
        dgvUsers.ReadOnly = True
        dgvUsers.RowHeadersWidth = 45
        dgvUsers.Size = New Size(1430, 652)
        dgvUsers.TabIndex = 0
        ' 
        ' instructorsTab
        ' 
        instructorsTab.Controls.Add(lblSearchInstructor)
        instructorsTab.Controls.Add(txtSearchInstructors)
        instructorsTab.Controls.Add(dgvInstructors)
        instructorsTab.Location = New Point(4, 28)
        instructorsTab.Name = "instructorsTab"
        instructorsTab.Padding = New Padding(8)
        instructorsTab.Size = New Size(1462, 724)
        instructorsTab.TabIndex = 1
        instructorsTab.Text = "Instructors"
        instructorsTab.UseVisualStyleBackColor = True
        ' 
        ' lblSearchInstructor
        ' 
        lblSearchInstructor.AutoSize = True
        lblSearchInstructor.Location = New Point(16, 20)
        lblSearchInstructor.Name = "lblSearchInstructor"
        lblSearchInstructor.Size = New Size(134, 19)
        lblSearchInstructor.TabIndex = 14
        lblSearchInstructor.Text = "Search Instructor:"
        ' 
        ' txtSearchInstructors
        ' 
        txtSearchInstructors.Location = New Point(158, 16)
        txtSearchInstructors.Name = "txtSearchInstructors"
        txtSearchInstructors.Size = New Size(240, 27)
        txtSearchInstructors.TabIndex = 12
        ' 
        ' dgvInstructors
        ' 
        dgvInstructors.AllowUserToAddRows = False
        dgvInstructors.AllowUserToDeleteRows = False
        dgvInstructors.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvInstructors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvInstructors.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvInstructors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvInstructors.Location = New Point(16, 56)
        dgvInstructors.Name = "dgvInstructors"
        dgvInstructors.ReadOnly = True
        dgvInstructors.RowHeadersWidth = 45
        dgvInstructors.Size = New Size(1430, 652)
        dgvInstructors.TabIndex = 0
        ' 
        ' studentsTab
        ' 
        studentsTab.Controls.Add(lblSearchStudent)
        studentsTab.Controls.Add(txtSearchStudents)
        studentsTab.Controls.Add(dgvStudents)
        studentsTab.Location = New Point(4, 28)
        studentsTab.Name = "studentsTab"
        studentsTab.Padding = New Padding(8)
        studentsTab.Size = New Size(1462, 724)
        studentsTab.TabIndex = 2
        studentsTab.Text = "Students"
        studentsTab.UseVisualStyleBackColor = True
        ' 
        ' lblSearchStudent
        ' 
        lblSearchStudent.AutoSize = True
        lblSearchStudent.Location = New Point(16, 20)
        lblSearchStudent.Name = "lblSearchStudent"
        lblSearchStudent.Size = New Size(118, 19)
        lblSearchStudent.TabIndex = 15
        lblSearchStudent.Text = "Search Student:"
        ' 
        ' txtSearchStudents
        ' 
        txtSearchStudents.Location = New Point(142, 16)
        txtSearchStudents.Name = "txtSearchStudents"
        txtSearchStudents.Size = New Size(240, 27)
        txtSearchStudents.TabIndex = 1
        ' 
        ' dgvStudents
        ' 
        dgvStudents.AllowUserToAddRows = False
        dgvStudents.AllowUserToDeleteRows = False
        dgvStudents.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvStudents.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvStudents.Location = New Point(16, 56)
        dgvStudents.Name = "dgvStudents"
        dgvStudents.ReadOnly = True
        dgvStudents.RowHeadersWidth = 45
        dgvStudents.Size = New Size(1430, 652)
        dgvStudents.TabIndex = 0
        ' 
        ' resetPasswordTab
        ' 
        resetPasswordTab.BackColor = Color.Transparent
        resetPasswordTab.Controls.Add(Panel1)
        resetPasswordTab.ForeColor = SystemColors.ControlText
        resetPasswordTab.Location = New Point(164, 4)
        resetPasswordTab.Name = "resetPasswordTab"
        resetPasswordTab.Padding = New Padding(8)
        resetPasswordTab.Size = New Size(1506, 830)
        resetPasswordTab.TabIndex = 2
        resetPasswordTab.Text = "Reset Password"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(resetPasswordLbl)
        Panel1.Controls.Add(targetUsernameLbl)
        Panel1.Controls.Add(txtTargetUsername)
        Panel1.Controls.Add(newResetPasswordLbl)
        Panel1.Controls.Add(txtNewResetPassword)
        Panel1.Controls.Add(btnResetPassword)
        Panel1.Location = New Point(28, 24)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(460, 320)
        Panel1.TabIndex = 9
        ' 
        ' resetPasswordLbl
        ' 
        resetPasswordLbl.AutoSize = True
        resetPasswordLbl.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        resetPasswordLbl.Location = New Point(20, 18)
        resetPasswordLbl.Name = "resetPasswordLbl"
        resetPasswordLbl.Size = New Size(185, 21)
        resetPasswordLbl.TabIndex = 5
        resetPasswordLbl.Text = "Reset User Password"
        ' 
        ' targetUsernameLbl
        ' 
        targetUsernameLbl.AutoSize = True
        targetUsernameLbl.Location = New Point(20, 74)
        targetUsernameLbl.Name = "targetUsernameLbl"
        targetUsernameLbl.Size = New Size(86, 19)
        targetUsernameLbl.TabIndex = 3
        targetUsernameLbl.Text = "Username:"
        ' 
        ' txtTargetUsername
        ' 
        txtTargetUsername.Location = New Point(170, 70)
        txtTargetUsername.Name = "txtTargetUsername"
        txtTargetUsername.Size = New Size(240, 27)
        txtTargetUsername.TabIndex = 0
        ' 
        ' newResetPasswordLbl
        ' 
        newResetPasswordLbl.AutoSize = True
        newResetPasswordLbl.Location = New Point(20, 124)
        newResetPasswordLbl.Name = "newResetPasswordLbl"
        newResetPasswordLbl.Size = New Size(118, 19)
        newResetPasswordLbl.TabIndex = 4
        newResetPasswordLbl.Text = "New Password:"
        ' 
        ' txtNewResetPassword
        ' 
        txtNewResetPassword.Location = New Point(170, 120)
        txtNewResetPassword.Name = "txtNewResetPassword"
        txtNewResetPassword.PasswordChar = "*"c
        txtNewResetPassword.Size = New Size(240, 27)
        txtNewResetPassword.TabIndex = 1
        ' 
        ' btnResetPassword
        ' 
        btnResetPassword.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btnResetPassword.Location = New Point(170, 172)
        btnResetPassword.Name = "btnResetPassword"
        btnResetPassword.Size = New Size(160, 34)
        btnResetPassword.TabIndex = 2
        btnResetPassword.Text = "Reset Password"
        btnResetPassword.UseVisualStyleBackColor = True
        ' 
        ' AssignmentTypesTab
        ' 
        AssignmentTypesTab.Controls.Add(lblSearchTypes)
        AssignmentTypesTab.Controls.Add(txtSearchTypes)
        AssignmentTypesTab.Controls.Add(typeNameLbl)
        AssignmentTypesTab.Controls.Add(txtTypeName)
        AssignmentTypesTab.Controls.Add(defaultWeightLbl)
        AssignmentTypesTab.Controls.Add(txtDefaultWeight)
        AssignmentTypesTab.Controls.Add(btnAddType)
        AssignmentTypesTab.Controls.Add(btnUpdateType)
        AssignmentTypesTab.Controls.Add(btnDeleteType)
        AssignmentTypesTab.Controls.Add(dgvAssignmentTypes)
        AssignmentTypesTab.Location = New Point(164, 4)
        AssignmentTypesTab.Name = "AssignmentTypesTab"
        AssignmentTypesTab.Padding = New Padding(8)
        AssignmentTypesTab.Size = New Size(1506, 830)
        AssignmentTypesTab.TabIndex = 3
        AssignmentTypesTab.Text = "Assignment Types"
        AssignmentTypesTab.UseVisualStyleBackColor = True
        ' 
        ' lblSearchTypes
        ' 
        lblSearchTypes.AutoSize = True
        lblSearchTypes.Location = New Point(20, 24)
        lblSearchTypes.Name = "lblSearchTypes"
        lblSearchTypes.Size = New Size(104, 19)
        lblSearchTypes.TabIndex = 20
        lblSearchTypes.Text = "Search Types:"
        ' 
        ' txtSearchTypes
        ' 
        txtSearchTypes.Location = New Point(134, 20)
        txtSearchTypes.Name = "txtSearchTypes"
        txtSearchTypes.Size = New Size(220, 27)
        txtSearchTypes.TabIndex = 19
        ' 
        ' typeNameLbl
        ' 
        typeNameLbl.AutoSize = True
        typeNameLbl.Location = New Point(390, 24)
        typeNameLbl.Name = "typeNameLbl"
        typeNameLbl.Size = New Size(94, 19)
        typeNameLbl.TabIndex = 17
        typeNameLbl.Text = "Type Name:"
        ' 
        ' txtTypeName
        ' 
        txtTypeName.Location = New Point(492, 20)
        txtTypeName.Name = "txtTypeName"
        txtTypeName.Size = New Size(190, 27)
        txtTypeName.TabIndex = 12
        ' 
        ' defaultWeightLbl
        ' 
        defaultWeightLbl.AutoSize = True
        defaultWeightLbl.Location = New Point(704, 24)
        defaultWeightLbl.Name = "defaultWeightLbl"
        defaultWeightLbl.Size = New Size(66, 19)
        defaultWeightLbl.TabIndex = 18
        defaultWeightLbl.Text = "Weight:"
        ' 
        ' txtDefaultWeight
        ' 
        txtDefaultWeight.Location = New Point(778, 20)
        txtDefaultWeight.Name = "txtDefaultWeight"
        txtDefaultWeight.Size = New Size(100, 27)
        txtDefaultWeight.TabIndex = 13
        ' 
        ' btnAddType
        ' 
        btnAddType.Location = New Point(904, 19)
        btnAddType.Name = "btnAddType"
        btnAddType.Size = New Size(96, 30)
        btnAddType.TabIndex = 14
        btnAddType.Text = "Add"
        btnAddType.UseVisualStyleBackColor = True
        ' 
        ' btnUpdateType
        ' 
        btnUpdateType.Location = New Point(1010, 19)
        btnUpdateType.Name = "btnUpdateType"
        btnUpdateType.Size = New Size(96, 30)
        btnUpdateType.TabIndex = 15
        btnUpdateType.Text = "Update"
        btnUpdateType.UseVisualStyleBackColor = True
        ' 
        ' btnDeleteType
        ' 
        btnDeleteType.Location = New Point(1116, 19)
        btnDeleteType.Name = "btnDeleteType"
        btnDeleteType.Size = New Size(96, 30)
        btnDeleteType.TabIndex = 16
        btnDeleteType.Text = "Delete"
        btnDeleteType.UseVisualStyleBackColor = True
        ' 
        ' dgvAssignmentTypes
        ' 
        dgvAssignmentTypes.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvAssignmentTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAssignmentTypes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvAssignmentTypes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAssignmentTypes.Location = New Point(20, 64)
        dgvAssignmentTypes.Name = "dgvAssignmentTypes"
        dgvAssignmentTypes.ReadOnly = True
        dgvAssignmentTypes.RowHeadersWidth = 45
        dgvAssignmentTypes.Size = New Size(1466, 746)
        dgvAssignmentTypes.TabIndex = 11
        ' 
        ' GradesTab
        ' 
        GradesTab.Controls.Add(lblStudentGrade)
        GradesTab.Controls.Add(txtGradeStudent)
        GradesTab.Controls.Add(lblCourseGrade)
        GradesTab.Controls.Add(txtGradeCourse)
        GradesTab.Controls.Add(btnSearchGrades)
        GradesTab.Controls.Add(btnClearGradeSearch)
        GradesTab.Controls.Add(dgvAllGrades)
        GradesTab.Location = New Point(164, 4)
        GradesTab.Name = "GradesTab"
        GradesTab.Padding = New Padding(8)
        GradesTab.Size = New Size(1506, 830)
        GradesTab.TabIndex = 4
        GradesTab.Text = "Grades"
        GradesTab.UseVisualStyleBackColor = True
        ' 
        ' lblStudentGrade
        ' 
        lblStudentGrade.AutoSize = True
        lblStudentGrade.Location = New Point(20, 24)
        lblStudentGrade.Name = "lblStudentGrade"
        lblStudentGrade.Size = New Size(68, 19)
        lblStudentGrade.TabIndex = 22
        lblStudentGrade.Text = "Student:"
        ' 
        ' txtGradeStudent
        ' 
        txtGradeStudent.Location = New Point(96, 20)
        txtGradeStudent.Name = "txtGradeStudent"
        txtGradeStudent.Size = New Size(260, 27)
        txtGradeStudent.TabIndex = 18
        ' 
        ' lblCourseGrade
        ' 
        lblCourseGrade.AutoSize = True
        lblCourseGrade.Location = New Point(380, 24)
        lblCourseGrade.Name = "lblCourseGrade"
        lblCourseGrade.Size = New Size(63, 19)
        lblCourseGrade.TabIndex = 23
        lblCourseGrade.Text = "Course:"
        ' 
        ' txtGradeCourse
        ' 
        txtGradeCourse.Location = New Point(450, 20)
        txtGradeCourse.Name = "txtGradeCourse"
        txtGradeCourse.Size = New Size(260, 27)
        txtGradeCourse.TabIndex = 19
        ' 
        ' btnSearchGrades
        ' 
        btnSearchGrades.Location = New Point(730, 19)
        btnSearchGrades.Name = "btnSearchGrades"
        btnSearchGrades.Size = New Size(105, 30)
        btnSearchGrades.TabIndex = 20
        btnSearchGrades.Text = "Search"
        btnSearchGrades.UseVisualStyleBackColor = True
        ' 
        ' btnClearGradeSearch
        ' 
        btnClearGradeSearch.Location = New Point(845, 19)
        btnClearGradeSearch.Name = "btnClearGradeSearch"
        btnClearGradeSearch.Size = New Size(105, 30)
        btnClearGradeSearch.TabIndex = 21
        btnClearGradeSearch.Text = "Clear"
        btnClearGradeSearch.UseVisualStyleBackColor = True
        ' 
        ' dgvAllGrades
        ' 
        dgvAllGrades.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvAllGrades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAllGrades.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvAllGrades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAllGrades.Location = New Point(20, 64)
        dgvAllGrades.Name = "dgvAllGrades"
        dgvAllGrades.ReadOnly = True
        dgvAllGrades.RowHeadersWidth = 45
        dgvAllGrades.Size = New Size(1466, 746)
        dgvAllGrades.TabIndex = 17
        ' 
        ' Enrollment
        ' 
        Enrollment.Controls.Add(lblSingleEnroll)
        Enrollment.Controls.Add(cmbEnrollStudent)
        Enrollment.Controls.Add(cmbEnrollCourse)
        Enrollment.Controls.Add(btnEnrollStudent)
        Enrollment.Controls.Add(lblBulkEnroll)
        Enrollment.Controls.Add(clbBulkStudents)
        Enrollment.Controls.Add(cmbBulkCourse)
        Enrollment.Controls.Add(btnBulkEnroll)
        Enrollment.Controls.Add(lblEnrollStatus)
        Enrollment.Location = New Point(164, 4)
        Enrollment.Name = "Enrollment"
        Enrollment.Padding = New Padding(8)
        Enrollment.Size = New Size(1506, 830)
        Enrollment.TabIndex = 5
        Enrollment.Text = "Enrollment"
        Enrollment.UseVisualStyleBackColor = True
        ' 
        ' lblSingleEnroll
        ' 
        lblSingleEnroll.AutoSize = True
        lblSingleEnroll.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)
        lblSingleEnroll.Location = New Point(24, 24)
        lblSingleEnroll.Name = "lblSingleEnroll"
        lblSingleEnroll.Size = New Size(154, 19)
        lblSingleEnroll.TabIndex = 7
        lblSingleEnroll.Text = "Single Enrollment"
        ' 
        ' cmbEnrollStudent
        ' 
        cmbEnrollStudent.DropDownStyle = ComboBoxStyle.DropDownList
        cmbEnrollStudent.FormattingEnabled = True
        cmbEnrollStudent.Location = New Point(24, 56)
        cmbEnrollStudent.Name = "cmbEnrollStudent"
        cmbEnrollStudent.Size = New Size(240, 27)
        cmbEnrollStudent.TabIndex = 0
        ' 
        ' cmbEnrollCourse
        ' 
        cmbEnrollCourse.DropDownStyle = ComboBoxStyle.DropDownList
        cmbEnrollCourse.FormattingEnabled = True
        cmbEnrollCourse.Location = New Point(280, 56)
        cmbEnrollCourse.Name = "cmbEnrollCourse"
        cmbEnrollCourse.Size = New Size(280, 27)
        cmbEnrollCourse.TabIndex = 1
        ' 
        ' btnEnrollStudent
        ' 
        btnEnrollStudent.Location = New Point(576, 55)
        btnEnrollStudent.Name = "btnEnrollStudent"
        btnEnrollStudent.Size = New Size(110, 29)
        btnEnrollStudent.TabIndex = 2
        btnEnrollStudent.Text = "Enroll"
        btnEnrollStudent.UseVisualStyleBackColor = True
        ' 
        ' lblBulkEnroll
        ' 
        lblBulkEnroll.AutoSize = True
        lblBulkEnroll.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)
        lblBulkEnroll.Location = New Point(24, 112)
        lblBulkEnroll.Name = "lblBulkEnroll"
        lblBulkEnroll.Size = New Size(140, 19)
        lblBulkEnroll.TabIndex = 8
        lblBulkEnroll.Text = "Bulk Enrollment"
        ' 
        ' clbBulkStudents
        ' 
        clbBulkStudents.CheckOnClick = True
        clbBulkStudents.FormattingEnabled = True
        clbBulkStudents.Location = New Point(24, 144)
        clbBulkStudents.Name = "clbBulkStudents"
        clbBulkStudents.Size = New Size(240, 220)
        clbBulkStudents.TabIndex = 3
        ' 
        ' cmbBulkCourse
        ' 
        cmbBulkCourse.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBulkCourse.FormattingEnabled = True
        cmbBulkCourse.Location = New Point(280, 144)
        cmbBulkCourse.Name = "cmbBulkCourse"
        cmbBulkCourse.Size = New Size(280, 27)
        cmbBulkCourse.TabIndex = 4
        ' 
        ' btnBulkEnroll
        ' 
        btnBulkEnroll.Location = New Point(576, 143)
        btnBulkEnroll.Name = "btnBulkEnroll"
        btnBulkEnroll.Size = New Size(125, 29)
        btnBulkEnroll.TabIndex = 5
        btnBulkEnroll.Text = "Bulk Enroll"
        btnBulkEnroll.UseVisualStyleBackColor = True
        ' 
        ' lblEnrollStatus
        ' 
        lblEnrollStatus.AutoSize = True
        lblEnrollStatus.Location = New Point(24, 384)
        lblEnrollStatus.Name = "lblEnrollStatus"
        lblEnrollStatus.Size = New Size(0, 19)
        lblEnrollStatus.TabIndex = 6
        ' 
        ' CoursesTab
        ' 
        CoursesTab.Controls.Add(lblCourseCode)
        CoursesTab.Controls.Add(txtCourseCode)
        CoursesTab.Controls.Add(lblCourseName)
        CoursesTab.Controls.Add(txtCourseName)
        CoursesTab.Controls.Add(lblCourseInstructor)
        CoursesTab.Controls.Add(cmbCourseInstructor)
        CoursesTab.Controls.Add(lblCourseFee)
        CoursesTab.Controls.Add(txtCourseFee)
        CoursesTab.Controls.Add(btnAddCourse)
        CoursesTab.Controls.Add(btnUpdateCourse)
        CoursesTab.Controls.Add(btnDeleteCourse)
        CoursesTab.Controls.Add(lblCourseStatus)
        CoursesTab.Controls.Add(dgvCourses)
        CoursesTab.Location = New Point(164, 4)
        CoursesTab.Name = "CoursesTab"
        CoursesTab.Padding = New Padding(8)
        CoursesTab.Size = New Size(1506, 830)
        CoursesTab.TabIndex = 6
        CoursesTab.Text = "Courses"
        CoursesTab.UseVisualStyleBackColor = True
        ' 
        ' lblCourseCode
        ' 
        lblCourseCode.AutoSize = True
        lblCourseCode.Location = New Point(24, 24)
        lblCourseCode.Name = "lblCourseCode"
        lblCourseCode.Size = New Size(118, 19)
        lblCourseCode.TabIndex = 0
        lblCourseCode.Text = "Course Code:"
        ' 
        ' txtCourseCode
        ' 
        txtCourseCode.Location = New Point(156, 20)
        txtCourseCode.Name = "txtCourseCode"
        txtCourseCode.Size = New Size(180, 27)
        txtCourseCode.TabIndex = 1
        ' 
        ' lblCourseName
        ' 
        lblCourseName.AutoSize = True
        lblCourseName.Location = New Point(24, 64)
        lblCourseName.Name = "lblCourseName"
        lblCourseName.Size = New Size(122, 19)
        lblCourseName.TabIndex = 2
        lblCourseName.Text = "Course Name:"
        ' 
        ' txtCourseName
        ' 
        txtCourseName.Location = New Point(156, 60)
        txtCourseName.Name = "txtCourseName"
        txtCourseName.Size = New Size(320, 27)
        txtCourseName.TabIndex = 3
        ' 
        ' lblCourseInstructor
        ' 
        lblCourseInstructor.AutoSize = True
        lblCourseInstructor.Location = New Point(24, 104)
        lblCourseInstructor.Name = "lblCourseInstructor"
        lblCourseInstructor.Size = New Size(94, 19)
        lblCourseInstructor.TabIndex = 4
        lblCourseInstructor.Text = "Instructor:"
        ' 
        ' cmbCourseInstructor
        ' 
        cmbCourseInstructor.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCourseInstructor.FormattingEnabled = True
        cmbCourseInstructor.Location = New Point(156, 100)
        cmbCourseInstructor.Name = "cmbCourseInstructor"
        cmbCourseInstructor.Size = New Size(260, 27)
        cmbCourseInstructor.TabIndex = 5
        '
        'lblCourseFee
        '
        Me.lblCourseFee.AutoSize = True
        Me.lblCourseFee.Location = New System.Drawing.Point(24, 146)
        Me.lblCourseFee.Name = "lblCourseFee"
        Me.lblCourseFee.Size = New System.Drawing.Size(72, 19)
        Me.lblCourseFee.TabIndex = 30
        Me.lblCourseFee.Text = "Course Fee:"
        '
        'txtCourseFee
        '
        Me.txtCourseFee.Location = New System.Drawing.Point(156, 142)
        Me.txtCourseFee.Name = "txtCourseFee"
        Me.txtCourseFee.Size = New System.Drawing.Size(140, 27)
        Me.txtCourseFee.TabIndex = 31
        Me.txtCourseFee.Text = "0.00"
        ' 
        ' btnAddCourse
        ' 
        btnAddCourse.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btnAddCourse.Location = New Point(156, 182)
        btnAddCourse.Name = "btnAddCourse"
        btnAddCourse.Size = New Size(135, 32)
        btnAddCourse.TabIndex = 6
        btnAddCourse.Text = "Add Course"
        btnAddCourse.UseVisualStyleBackColor = True
        '
        'btnUpdateCourse
        '
        Me.btnUpdateCourse.Location = New System.Drawing.Point(297, 182)
        Me.btnUpdateCourse.Name = "btnUpdateCourse"
        Me.btnUpdateCourse.Size = New System.Drawing.Size(120, 32)
        Me.btnUpdateCourse.TabIndex = 32
        Me.btnUpdateCourse.Text = "Update Course"
        Me.btnUpdateCourse.UseVisualStyleBackColor = True
        Me.btnUpdateCourse.BringToFront()
        '
        'btnDeleteCourse
        '
        Me.btnDeleteCourse.Location = New System.Drawing.Point(427, 182)
        Me.btnDeleteCourse.Name = "btnDeleteCourse"
        Me.btnDeleteCourse.Size = New System.Drawing.Size(120, 32)
        Me.btnDeleteCourse.TabIndex = 33
        Me.btnDeleteCourse.Text = "Delete Course"
        Me.btnDeleteCourse.UseVisualStyleBackColor = True
        Me.btnDeleteCourse.BringToFront()
        ' 
        ' lblCourseStatus
        ' 
        lblCourseStatus.AutoSize = True
        lblCourseStatus.Location = New Point(306, 148)
        lblCourseStatus.Name = "lblCourseStatus"
        lblCourseStatus.Size = New Size(0, 19)
        lblCourseStatus.TabIndex = 7
        ' 
        ' dgvCourses
        ' 
        dgvCourses.AllowUserToAddRows = False
        dgvCourses.AllowUserToDeleteRows = False
        dgvCourses.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvCourses.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvCourses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCourses.Location = New Point(24, 224)
        dgvCourses.Name = "dgvCourses"
        dgvCourses.ReadOnly = True
        dgvCourses.RowHeadersWidth = 45
        dgvCourses.Size = New Size(1458, 586)
        dgvCourses.TabIndex = 8
        ' 
        ' statusStrip1
        ' 
        statusStrip1.Items.AddRange(New ToolStripItem() {lblSession})
        statusStrip1.Location = New Point(0, 904)
        statusStrip1.Name = "statusStrip1"
        statusStrip1.Size = New Size(1706, 22)
        statusStrip1.TabIndex = 22
        statusStrip1.Text = "StatusStrip1"
        ' 
        ' lblSession
        ' 
        lblSession.Name = "lblSession"
        lblSession.Size = New Size(109, 17)
        lblSession.Text = "Session: Active"
        ' 
        ' AdminForm
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1706, 926)
        Controls.Add(statusStrip1)
        Controls.Add(btnBackups)
        Controls.Add(btnAuditLog)
        Controls.Add(btnReportsForm)
        Controls.Add(tabsController)
        MinimumSize = New Size(1100, 650)
        Name = "AdminForm"
        Text = "Admin Dashboard"
        WindowState = FormWindowState.Maximized
        addTeacherPanel.ResumeLayout(False)
        addTeacherPanel.PerformLayout()
        TabControl1.ResumeLayout(False)
        usersTab.ResumeLayout(False)
        usersTab.PerformLayout()
        CType(dgvUsers, ComponentModel.ISupportInitialize).EndInit()
        instructorsTab.ResumeLayout(False)
        instructorsTab.PerformLayout()
        CType(dgvInstructors, ComponentModel.ISupportInitialize).EndInit()
        studentsTab.ResumeLayout(False)
        studentsTab.PerformLayout()
        CType(dgvStudents, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(dgvAssignmentTypes, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvAllGrades, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvCourses, ComponentModel.ISupportInitialize).EndInit()
        tabsController.ResumeLayout(False)
        AddUserTab.ResumeLayout(False)
        usersTab1.ResumeLayout(False)
        resetPasswordTab.ResumeLayout(False)
        AssignmentTypesTab.ResumeLayout(False)
        AssignmentTypesTab.PerformLayout()
        GradesTab.ResumeLayout(False)
        GradesTab.PerformLayout()
        Enrollment.ResumeLayout(False)
        Enrollment.PerformLayout()
        CoursesTab.ResumeLayout(False)
        ' Example if added directly to AdminForm (or replace Me.Controls with your panel/tab):
        CoursesTab.PerformLayout()
        CType(errProvider, ComponentModel.ISupportInitialize).EndInit()
        statusStrip1.ResumeLayout(False)
        statusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txtNewUsername As TextBox
    Friend WithEvents txtNewPassword As TextBox
    Friend WithEvents txtFirstName As TextBox
    Friend WithEvents txtLastName As TextBox
    Friend WithEvents txtDepartment As TextBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnAddUser As Button
    Friend WithEvents addTeacherPanel As Panel
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents lblEmail As Label
    Friend WithEvents dtpDateOfBirth As DateTimePicker
    Friend WithEvents lblDateOfBirth As Label
    Friend WithEvents UserTypeLbl As Label
    Friend WithEvents cmbUserType As ComboBox
    Friend WithEvents lblDepartment As Label
    Friend WithEvents addLastNameLbl As Label
    Friend WithEvents addFirstNameLbl As Label
    Friend WithEvents addNewPasswordLbl As Label
    Friend WithEvents addNewUsernameLbl As Label
    Friend WithEvents addUserLabel As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents usersTab As TabPage
    Friend WithEvents dgvUsers As DataGridView
    Friend WithEvents txtSearchUsers As TextBox
    Friend WithEvents lblSearchUser As Label
    Friend WithEvents instructorsTab As TabPage
    Friend WithEvents dgvInstructors As DataGridView
    Friend WithEvents txtSearchInstructors As TextBox
    Friend WithEvents lblSearchInstructor As Label
    Friend WithEvents studentsTab As TabPage
    Friend WithEvents dgvStudents As DataGridView
    Friend WithEvents txtSearchStudents As TextBox
    Friend WithEvents lblSearchStudent As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents resetPasswordLbl As Label
    Friend WithEvents newResetPasswordLbl As Label
    Friend WithEvents targetUsernameLbl As Label
    Friend WithEvents btnResetPassword As Button
    Friend WithEvents txtNewResetPassword As TextBox
    Friend WithEvents txtTargetUsername As TextBox
    Friend WithEvents btnRefresh As Button
    Friend WithEvents dgvAssignmentTypes As DataGridView
    Friend WithEvents txtTypeName As TextBox
    Friend WithEvents txtDefaultWeight As TextBox
    Friend WithEvents btnAddType As Button
    Friend WithEvents btnUpdateType As Button
    Friend WithEvents btnDeleteType As Button
    Friend WithEvents dgvAllGrades As DataGridView
    Friend WithEvents tabsController As TabControl
    Friend WithEvents AddUserTab As TabPage
    Friend WithEvents usersTab1 As TabPage
    Friend WithEvents resetPasswordTab As TabPage
    Friend WithEvents AssignmentTypesTab As TabPage
    Friend WithEvents lblSearchTypes As Label
    Friend WithEvents txtSearchTypes As TextBox
    Friend WithEvents defaultWeightLbl As Label
    Friend WithEvents typeNameLbl As Label
    Friend WithEvents GradesTab As TabPage
    Friend WithEvents lblCourseGrade As Label
    Friend WithEvents lblStudentGrade As Label
    Friend WithEvents btnClearGradeSearch As Button
    Friend WithEvents btnSearchGrades As Button
    Friend WithEvents txtGradeCourse As TextBox
    Friend WithEvents txtGradeStudent As TextBox
    Friend WithEvents Enrollment As TabPage
    Friend WithEvents lblEnrollStatus As Label
    Friend WithEvents btnBulkEnroll As Button
    Friend WithEvents cmbBulkCourse As ComboBox
    Friend WithEvents clbBulkStudents As CheckedListBox
    Friend WithEvents btnEnrollStudent As Button
    Friend WithEvents cmbEnrollCourse As ComboBox
    Friend WithEvents cmbEnrollStudent As ComboBox
    Friend WithEvents lblSingleEnroll As Label
    Friend WithEvents lblBulkEnroll As Label
    Friend WithEvents CoursesTab As TabPage
    Friend WithEvents lblCourseCode As Label
    Friend WithEvents txtCourseCode As TextBox
    Friend WithEvents lblCourseName As Label
    Friend WithEvents txtCourseName As TextBox
    Friend WithEvents lblCourseInstructor As Label
    Friend WithEvents cmbCourseInstructor As ComboBox
    Friend WithEvents lblCourseFee As Label
    Friend WithEvents txtCourseFee As TextBox
    Friend WithEvents btnAddCourse As Button
    Friend WithEvents lblCourseStatus As Label
    Friend WithEvents dgvCourses As DataGridView
    Friend WithEvents btnUpdateCourse As Button
    Friend WithEvents btnDeleteCourse As Button
    Friend WithEvents btnReportsForm As Button
    Friend WithEvents btnAuditLog As Button
    Friend WithEvents btnBackups As Button
    Friend WithEvents errProvider As ErrorProvider
    Friend WithEvents statusStrip1 As StatusStrip
    Friend WithEvents lblSession As ToolStripStatusLabel
End Class