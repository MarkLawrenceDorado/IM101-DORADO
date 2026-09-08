<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminForm
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
        instructorsTab = New TabPage()
        dgvInstructors = New DataGridView()
        studentsTab = New TabPage()
        dgvStudents = New DataGridView()
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
        defaultWeightLbl = New Label()
        typeNameLbl = New Label()
        GradesTab = New TabPage()
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
        tabsController.SuspendLayout()
        AddUserTab.SuspendLayout()
        usersTab1.SuspendLayout()
        resetPasswordTab.SuspendLayout()
        AssignmentTypesTab.SuspendLayout()
        GradesTab.SuspendLayout()
        SuspendLayout()
        ' 
        ' txtNewUsername
        ' 
        txtNewUsername.Location = New Point(203, 205)
        txtNewUsername.Name = "txtNewUsername"
        txtNewUsername.Size = New Size(215, 30)
        txtNewUsername.TabIndex = 0
        ' 
        ' txtNewPassword
        ' 
        txtNewPassword.Location = New Point(203, 267)
        txtNewPassword.Name = "txtNewPassword"
        txtNewPassword.PasswordChar = "*"c
        txtNewPassword.Size = New Size(215, 30)
        txtNewPassword.TabIndex = 1
        ' 
        ' txtFirstName
        ' 
        txtFirstName.Location = New Point(203, 326)
        txtFirstName.Name = "txtFirstName"
        txtFirstName.Size = New Size(215, 30)
        txtFirstName.TabIndex = 2
        ' 
        ' txtLastName
        ' 
        txtLastName.Location = New Point(203, 386)
        txtLastName.Name = "txtLastName"
        txtLastName.Size = New Size(215, 30)
        txtLastName.TabIndex = 3
        ' 
        ' txtDepartment
        ' 
        txtDepartment.Location = New Point(203, 447)
        txtDepartment.Name = "txtDepartment"
        txtDepartment.Size = New Size(215, 30)
        txtDepartment.TabIndex = 4
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Location = New Point(34, 537)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(0, 23)
        lblStatus.TabIndex = 5
        ' 
        ' btnAddUser
        ' 
        btnAddUser.Location = New Point(293, 577)
        btnAddUser.Name = "btnAddUser"
        btnAddUser.Size = New Size(125, 31)
        btnAddUser.TabIndex = 6
        btnAddUser.Text = "Add User"
        btnAddUser.UseVisualStyleBackColor = True
        ' 
        ' addTeacherPanel
        ' 
        addTeacherPanel.Controls.Add(txtEmail)
        addTeacherPanel.Controls.Add(lblEmail)
        addTeacherPanel.Controls.Add(dtpDateOfBirth)
        addTeacherPanel.Controls.Add(lblDateOfBirth)
        addTeacherPanel.Controls.Add(UserTypeLbl)
        addTeacherPanel.Controls.Add(cmbUserType)
        addTeacherPanel.Controls.Add(lblDepartment)
        addTeacherPanel.Controls.Add(addLastNameLbl)
        addTeacherPanel.Controls.Add(addFirstNameLbl)
        addTeacherPanel.Controls.Add(addNewPasswordLbl)
        addTeacherPanel.Controls.Add(addNewUsernameLbl)
        addTeacherPanel.Controls.Add(addUserLabel)
        addTeacherPanel.Controls.Add(txtNewUsername)
        addTeacherPanel.Controls.Add(lblStatus)
        addTeacherPanel.Controls.Add(btnAddUser)
        addTeacherPanel.Controls.Add(txtNewPassword)
        addTeacherPanel.Controls.Add(txtLastName)
        addTeacherPanel.Controls.Add(txtDepartment)
        addTeacherPanel.Controls.Add(txtFirstName)
        addTeacherPanel.Location = New Point(38, 23)
        addTeacherPanel.Name = "addTeacherPanel"
        addTeacherPanel.Size = New Size(456, 672)
        addTeacherPanel.TabIndex = 7
        ' 
        ' txtEmail
        ' 
        txtEmail.Location = New Point(203, 497)
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(215, 30)
        txtEmail.TabIndex = 18
        ' 
        ' lblEmail
        ' 
        lblEmail.AutoSize = True
        lblEmail.Location = New Point(34, 504)
        lblEmail.Name = "lblEmail"
        lblEmail.Size = New Size(60, 23)
        lblEmail.TabIndex = 17
        lblEmail.Text = "Email: "
        ' 
        ' dtpDateOfBirth
        ' 
        dtpDateOfBirth.Location = New Point(203, 448)
        dtpDateOfBirth.Name = "dtpDateOfBirth"
        dtpDateOfBirth.Size = New Size(215, 30)
        dtpDateOfBirth.TabIndex = 16
        ' 
        ' lblDateOfBirth
        ' 
        lblDateOfBirth.AutoSize = True
        lblDateOfBirth.Location = New Point(34, 450)
        lblDateOfBirth.Name = "lblDateOfBirth"
        lblDateOfBirth.Size = New Size(116, 23)
        lblDateOfBirth.TabIndex = 15
        lblDateOfBirth.Text = "Date of Birth: "
        ' 
        ' UserTypeLbl
        ' 
        UserTypeLbl.AutoSize = True
        UserTypeLbl.Location = New Point(34, 155)
        UserTypeLbl.Name = "UserTypeLbl"
        UserTypeLbl.Size = New Size(88, 23)
        UserTypeLbl.TabIndex = 14
        UserTypeLbl.Text = "User Type:"
        ' 
        ' cmbUserType
        ' 
        cmbUserType.FormattingEnabled = True
        cmbUserType.Location = New Point(203, 152)
        cmbUserType.Name = "cmbUserType"
        cmbUserType.Size = New Size(215, 31)
        cmbUserType.TabIndex = 13
        ' 
        ' lblDepartment
        ' 
        lblDepartment.AutoSize = True
        lblDepartment.Location = New Point(33, 447)
        lblDepartment.Name = "lblDepartment"
        lblDepartment.Size = New Size(106, 23)
        lblDepartment.TabIndex = 12
        lblDepartment.Text = "Department:"
        ' 
        ' addLastNameLbl
        ' 
        addLastNameLbl.AutoSize = True
        addLastNameLbl.Location = New Point(33, 389)
        addLastNameLbl.Name = "addLastNameLbl"
        addLastNameLbl.Size = New Size(95, 23)
        addLastNameLbl.TabIndex = 11
        addLastNameLbl.Text = "Last Name:"
        ' 
        ' addFirstNameLbl
        ' 
        addFirstNameLbl.AutoSize = True
        addFirstNameLbl.Location = New Point(34, 329)
        addFirstNameLbl.Name = "addFirstNameLbl"
        addFirstNameLbl.Size = New Size(96, 23)
        addFirstNameLbl.TabIndex = 10
        addFirstNameLbl.Text = "First Name:"
        ' 
        ' addNewPasswordLbl
        ' 
        addNewPasswordLbl.AutoSize = True
        addNewPasswordLbl.Location = New Point(34, 270)
        addNewPasswordLbl.Name = "addNewPasswordLbl"
        addNewPasswordLbl.Size = New Size(84, 23)
        addNewPasswordLbl.TabIndex = 9
        addNewPasswordLbl.Text = "Password:"
        ' 
        ' addNewUsernameLbl
        ' 
        addNewUsernameLbl.AutoSize = True
        addNewUsernameLbl.Location = New Point(33, 208)
        addNewUsernameLbl.Name = "addNewUsernameLbl"
        addNewUsernameLbl.Size = New Size(91, 23)
        addNewUsernameLbl.TabIndex = 8
        addNewUsernameLbl.Text = "Username:"
        ' 
        ' addUserLabel
        ' 
        addUserLabel.AutoSize = True
        addUserLabel.Location = New Point(186, 50)
        addUserLabel.Name = "addUserLabel"
        addUserLabel.Size = New Size(80, 23)
        addUserLabel.TabIndex = 7
        addUserLabel.Text = "Add User"
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(usersTab)
        TabControl1.Controls.Add(instructorsTab)
        TabControl1.Controls.Add(studentsTab)
        TabControl1.Location = New Point(6, 46)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(1536, 922)
        TabControl1.TabIndex = 8
        ' 
        ' usersTab
        ' 
        usersTab.Controls.Add(dgvUsers)
        usersTab.Location = New Point(4, 32)
        usersTab.Name = "usersTab"
        usersTab.Padding = New Padding(3)
        usersTab.Size = New Size(1528, 886)
        usersTab.TabIndex = 0
        usersTab.Text = "Users"
        usersTab.UseVisualStyleBackColor = True
        ' 
        ' dgvUsers
        ' 
        dgvUsers.AllowUserToAddRows = False
        dgvUsers.AllowUserToDeleteRows = False
        dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvUsers.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvUsers.Dock = DockStyle.Fill
        dgvUsers.Location = New Point(3, 3)
        dgvUsers.Name = "dgvUsers"
        dgvUsers.ReadOnly = True
        dgvUsers.RowHeadersWidth = 51
        dgvUsers.Size = New Size(1522, 880)
        dgvUsers.TabIndex = 0
        ' 
        ' instructorsTab
        ' 
        instructorsTab.Controls.Add(dgvInstructors)
        instructorsTab.Location = New Point(4, 29)
        instructorsTab.Name = "instructorsTab"
        instructorsTab.Padding = New Padding(3)
        instructorsTab.Size = New Size(1528, 889)
        instructorsTab.TabIndex = 1
        instructorsTab.Text = "Instructors"
        instructorsTab.UseVisualStyleBackColor = True
        ' 
        ' dgvInstructors
        ' 
        dgvInstructors.AllowUserToAddRows = False
        dgvInstructors.AllowUserToDeleteRows = False
        dgvInstructors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvInstructors.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvInstructors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvInstructors.Dock = DockStyle.Fill
        dgvInstructors.Location = New Point(3, 3)
        dgvInstructors.Name = "dgvInstructors"
        dgvInstructors.ReadOnly = True
        dgvInstructors.RowHeadersWidth = 51
        dgvInstructors.Size = New Size(1522, 883)
        dgvInstructors.TabIndex = 0
        ' 
        ' studentsTab
        ' 
        studentsTab.Controls.Add(dgvStudents)
        studentsTab.Location = New Point(4, 29)
        studentsTab.Name = "studentsTab"
        studentsTab.Size = New Size(1528, 889)
        studentsTab.TabIndex = 2
        studentsTab.Text = "Students"
        studentsTab.UseVisualStyleBackColor = True
        ' 
        ' dgvStudents
        ' 
        dgvStudents.AllowUserToAddRows = False
        dgvStudents.AllowUserToDeleteRows = False
        dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvStudents.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvStudents.Dock = DockStyle.Fill
        dgvStudents.Location = New Point(0, 0)
        dgvStudents.Name = "dgvStudents"
        dgvStudents.ReadOnly = True
        dgvStudents.RowHeadersWidth = 51
        dgvStudents.Size = New Size(1528, 889)
        dgvStudents.TabIndex = 0
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(resetPasswordLbl)
        Panel1.Controls.Add(newResetPasswordLbl)
        Panel1.Controls.Add(targetUsernameLbl)
        Panel1.Controls.Add(btnResetPassword)
        Panel1.Controls.Add(txtNewResetPassword)
        Panel1.Controls.Add(txtTargetUsername)
        Panel1.Location = New Point(38, 36)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(386, 472)
        Panel1.TabIndex = 9
        ' 
        ' resetPasswordLbl
        ' 
        resetPasswordLbl.AutoSize = True
        resetPasswordLbl.Location = New Point(122, 101)
        resetPasswordLbl.Name = "resetPasswordLbl"
        resetPasswordLbl.Size = New Size(172, 23)
        resetPasswordLbl.TabIndex = 5
        resetPasswordLbl.Text = "Reset Users Password"
        ' 
        ' newResetPasswordLbl
        ' 
        newResetPasswordLbl.AutoSize = True
        newResetPasswordLbl.Location = New Point(38, 286)
        newResetPasswordLbl.Name = "newResetPasswordLbl"
        newResetPasswordLbl.Size = New Size(123, 23)
        newResetPasswordLbl.TabIndex = 4
        newResetPasswordLbl.Text = "New Password:"
        ' 
        ' targetUsernameLbl
        ' 
        targetUsernameLbl.AutoSize = True
        targetUsernameLbl.Location = New Point(38, 218)
        targetUsernameLbl.Name = "targetUsernameLbl"
        targetUsernameLbl.Size = New Size(91, 23)
        targetUsernameLbl.TabIndex = 3
        targetUsernameLbl.Text = "Username:"
        ' 
        ' btnResetPassword
        ' 
        btnResetPassword.Location = New Point(156, 366)
        btnResetPassword.Name = "btnResetPassword"
        btnResetPassword.Size = New Size(187, 29)
        btnResetPassword.TabIndex = 2
        btnResetPassword.Text = "Reset Password"
        btnResetPassword.UseVisualStyleBackColor = True
        ' 
        ' txtNewResetPassword
        ' 
        txtNewResetPassword.Location = New Point(156, 283)
        txtNewResetPassword.Name = "txtNewResetPassword"
        txtNewResetPassword.Size = New Size(187, 30)
        txtNewResetPassword.TabIndex = 1
        ' 
        ' txtTargetUsername
        ' 
        txtTargetUsername.Location = New Point(156, 211)
        txtTargetUsername.Name = "txtTargetUsername"
        txtTargetUsername.Size = New Size(187, 30)
        txtTargetUsername.TabIndex = 0
        ' 
        ' btnRefresh
        ' 
        btnRefresh.Location = New Point(1404, 20)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(104, 35)
        btnRefresh.TabIndex = 10
        btnRefresh.Text = "Refresh"
        btnRefresh.UseVisualStyleBackColor = True
        ' 
        ' dgvAssignmentTypes
        ' 
        dgvAssignmentTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAssignmentTypes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvAssignmentTypes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAssignmentTypes.Location = New Point(3, 79)
        dgvAssignmentTypes.Name = "dgvAssignmentTypes"
        dgvAssignmentTypes.ReadOnly = True
        dgvAssignmentTypes.RowHeadersWidth = 51
        dgvAssignmentTypes.Size = New Size(1542, 892)
        dgvAssignmentTypes.TabIndex = 11
        ' 
        ' txtTypeName
        ' 
        txtTypeName.Location = New Point(612, 28)
        txtTypeName.Name = "txtTypeName"
        txtTypeName.Size = New Size(125, 30)
        txtTypeName.TabIndex = 12
        ' 
        ' txtDefaultWeight
        ' 
        txtDefaultWeight.Location = New Point(855, 28)
        txtDefaultWeight.Name = "txtDefaultWeight"
        txtDefaultWeight.Size = New Size(125, 30)
        txtDefaultWeight.TabIndex = 13
        ' 
        ' btnAddType
        ' 
        btnAddType.Location = New Point(1090, 19)
        btnAddType.Name = "btnAddType"
        btnAddType.Size = New Size(108, 39)
        btnAddType.TabIndex = 14
        btnAddType.Text = "Add"
        btnAddType.UseVisualStyleBackColor = True
        ' 
        ' btnUpdateType
        ' 
        btnUpdateType.Location = New Point(1245, 19)
        btnUpdateType.Name = "btnUpdateType"
        btnUpdateType.Size = New Size(107, 39)
        btnUpdateType.TabIndex = 15
        btnUpdateType.Text = "Update"
        btnUpdateType.UseVisualStyleBackColor = True
        ' 
        ' btnDeleteType
        ' 
        btnDeleteType.Location = New Point(1400, 19)
        btnDeleteType.Name = "btnDeleteType"
        btnDeleteType.Size = New Size(110, 39)
        btnDeleteType.TabIndex = 16
        btnDeleteType.Text = "Delete"
        btnDeleteType.UseVisualStyleBackColor = True
        ' 
        ' dgvAllGrades
        ' 
        dgvAllGrades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAllGrades.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvAllGrades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAllGrades.Location = New Point(3, 80)
        dgvAllGrades.Name = "dgvAllGrades"
        dgvAllGrades.ReadOnly = True
        dgvAllGrades.RowHeadersWidth = 51
        dgvAllGrades.Size = New Size(1542, 891)
        dgvAllGrades.TabIndex = 17
        ' 
        ' tabsController
        ' 
        tabsController.Alignment = TabAlignment.Left
        tabsController.Controls.Add(AddUserTab)
        tabsController.Controls.Add(usersTab1)
        tabsController.Controls.Add(resetPasswordTab)
        tabsController.Controls.Add(AssignmentTypesTab)
        tabsController.Controls.Add(GradesTab)
        tabsController.DrawMode = TabDrawMode.OwnerDrawFixed
        tabsController.Font = New Font("Segoe UI", 10F)
        tabsController.ItemSize = New Size(40, 150)
        tabsController.Location = New Point(12, 36)
        tabsController.Multiline = True
        tabsController.Name = "tabsController"
        tabsController.Padding = New Point(0, 0)
        tabsController.SelectedIndex = 0
        tabsController.Size = New Size(1706, 982)
        tabsController.SizeMode = TabSizeMode.Fixed
        tabsController.TabIndex = 18
        ' 
        ' AddUserTab
        ' 
        AddUserTab.BackColor = Color.Transparent
        AddUserTab.Controls.Add(addTeacherPanel)
        AddUserTab.Location = New Point(154, 4)
        AddUserTab.Name = "AddUserTab"
        AddUserTab.Padding = New Padding(3)
        AddUserTab.Size = New Size(1548, 974)
        AddUserTab.TabIndex = 0
        AddUserTab.Text = "Add User"
        ' 
        ' usersTab1
        ' 
        usersTab1.BackColor = Color.Transparent
        usersTab1.Controls.Add(TabControl1)
        usersTab1.Controls.Add(btnRefresh)
        usersTab1.Location = New Point(154, 4)
        usersTab1.Name = "usersTab1"
        usersTab1.Padding = New Padding(3)
        usersTab1.Size = New Size(1548, 974)
        usersTab1.TabIndex = 1
        usersTab1.Text = "Users"
        ' 
        ' resetPasswordTab
        ' 
        resetPasswordTab.BackColor = Color.Transparent
        resetPasswordTab.Controls.Add(Panel1)
        resetPasswordTab.ForeColor = SystemColors.ControlText
        resetPasswordTab.Location = New Point(154, 4)
        resetPasswordTab.Name = "resetPasswordTab"
        resetPasswordTab.Size = New Size(1548, 974)
        resetPasswordTab.TabIndex = 2
        resetPasswordTab.Text = "Reset Password"
        ' 
        ' AssignmentTypesTab
        ' 
        AssignmentTypesTab.Controls.Add(defaultWeightLbl)
        AssignmentTypesTab.Controls.Add(typeNameLbl)
        AssignmentTypesTab.Controls.Add(dgvAssignmentTypes)
        AssignmentTypesTab.Controls.Add(txtTypeName)
        AssignmentTypesTab.Controls.Add(btnDeleteType)
        AssignmentTypesTab.Controls.Add(txtDefaultWeight)
        AssignmentTypesTab.Controls.Add(btnUpdateType)
        AssignmentTypesTab.Controls.Add(btnAddType)
        AssignmentTypesTab.Location = New Point(154, 4)
        AssignmentTypesTab.Name = "AssignmentTypesTab"
        AssignmentTypesTab.Size = New Size(1548, 974)
        AssignmentTypesTab.TabIndex = 3
        AssignmentTypesTab.Text = "Assignment Types"
        AssignmentTypesTab.UseVisualStyleBackColor = True
        ' 
        ' defaultWeightLbl
        ' 
        defaultWeightLbl.AutoSize = True
        defaultWeightLbl.Location = New Point(774, 35)
        defaultWeightLbl.Name = "defaultWeightLbl"
        defaultWeightLbl.Size = New Size(68, 23)
        defaultWeightLbl.TabIndex = 18
        defaultWeightLbl.Text = "Weight:"
        ' 
        ' typeNameLbl
        ' 
        typeNameLbl.AutoSize = True
        typeNameLbl.Location = New Point(491, 35)
        typeNameLbl.Name = "typeNameLbl"
        typeNameLbl.Size = New Size(100, 23)
        typeNameLbl.TabIndex = 17
        typeNameLbl.Text = "Type Name:"
        ' 
        ' GradesTab
        ' 
        GradesTab.Controls.Add(dgvAllGrades)
        GradesTab.Location = New Point(154, 4)
        GradesTab.Name = "GradesTab"
        GradesTab.Size = New Size(1548, 974)
        GradesTab.TabIndex = 4
        GradesTab.Text = "Grades"
        GradesTab.UseVisualStyleBackColor = True
        ' 
        ' AdminForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1742, 1055)
        Controls.Add(tabsController)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Name = "AdminForm"
        Text = "AdminForm"
        WindowState = FormWindowState.Maximized
        addTeacherPanel.ResumeLayout(False)
        addTeacherPanel.PerformLayout()
        TabControl1.ResumeLayout(False)
        usersTab.ResumeLayout(False)
        CType(dgvUsers, ComponentModel.ISupportInitialize).EndInit()
        instructorsTab.ResumeLayout(False)
        CType(dgvInstructors, ComponentModel.ISupportInitialize).EndInit()
        studentsTab.ResumeLayout(False)
        CType(dgvStudents, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(dgvAssignmentTypes, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvAllGrades, ComponentModel.ISupportInitialize).EndInit()
        tabsController.ResumeLayout(False)
        AddUserTab.ResumeLayout(False)
        usersTab1.ResumeLayout(False)
        resetPasswordTab.ResumeLayout(False)
        AssignmentTypesTab.ResumeLayout(False)
        AssignmentTypesTab.PerformLayout()
        GradesTab.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents txtNewUsername As TextBox
    Friend WithEvents txtNewPassword As TextBox
    Friend WithEvents txtFirstName As TextBox
    Friend WithEvents txtLastName As TextBox
    Friend WithEvents txtDepartment As TextBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnAddUser As Button
    Friend WithEvents addTeacherPanel As Panel
    Friend WithEvents addUserLabel As Label
    Friend WithEvents lblDepartment As Label
    Friend WithEvents addLastNameLbl As Label
    Friend WithEvents addFirstNameLbl As Label
    Friend WithEvents addNewPasswordLbl As Label
    Friend WithEvents addNewUsernameLbl As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents usersTab As TabPage
    Friend WithEvents instructorsTab As TabPage
    Friend WithEvents dgvUsers As DataGridView
    Friend WithEvents dgvInstructors As DataGridView
    Friend WithEvents studentsTab As TabPage
    Friend WithEvents dgvStudents As DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtNewResetPassword As TextBox
    Friend WithEvents targetUsernameLbl As Label
    Friend WithEvents btnResetPassword As Button
    Friend WithEvents newResetPasswordLbl As Label
    Friend WithEvents btnRefresh As Button
    Friend WithEvents resetPasswordLbl As Label
    Friend WithEvents txtTargetUsername As TextBox
    Friend WithEvents dgvAssignmentTypes As DataGridView
    Friend WithEvents txtTypeName As TextBox
    Friend WithEvents txtDefaultWeight As TextBox
    Friend WithEvents btnAddType As Button
    Friend WithEvents btnUpdateType As Button
    Friend WithEvents btnDeleteType As Button
    Friend WithEvents dgvAllGrades As DataGridView
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents resetPasswordTab As TabPage
    Friend WithEvents GradesTab As TabPage
    Friend WithEvents tabsController As TabControl
    Friend WithEvents AddUserTab As TabPage
    Friend WithEvents usersTab1 As TabPage
    Friend WithEvents AssignmentTypesTab As TabPage
    Friend WithEvents defaultWeightLbl As Label
    Friend WithEvents typeNameLbl As Label
    Friend WithEvents UserTypeLbl As Label
    Friend WithEvents cmbUserType As ComboBox
    Friend WithEvents dtpDateOfBirth As DateTimePicker
    Friend WithEvents lblDateOfBirth As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents lblEmail As Label
End Class
