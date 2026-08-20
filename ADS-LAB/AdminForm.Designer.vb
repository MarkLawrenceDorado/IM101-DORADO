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
        btnAddTeacher = New Button()
        addTeacherPanel = New Panel()
        addDepartmentLbl = New Label()
        addLastNameLbl = New Label()
        addFirstNameLbl = New Label()
        addNewPasswordLbl = New Label()
        addNewUsernameLbl = New Label()
        addTeacherLabel = New Label()
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
        addTeacherPanel.SuspendLayout()
        TabControl1.SuspendLayout()
        usersTab.SuspendLayout()
        CType(dgvUsers, ComponentModel.ISupportInitialize).BeginInit()
        instructorsTab.SuspendLayout()
        CType(dgvInstructors, ComponentModel.ISupportInitialize).BeginInit()
        studentsTab.SuspendLayout()
        CType(dgvStudents, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' txtNewUsername
        ' 
        txtNewUsername.Location = New Point(203, 117)
        txtNewUsername.Name = "txtNewUsername"
        txtNewUsername.Size = New Size(215, 27)
        txtNewUsername.TabIndex = 0
        ' 
        ' txtNewPassword
        ' 
        txtNewPassword.Location = New Point(202, 177)
        txtNewPassword.Name = "txtNewPassword"
        txtNewPassword.PasswordChar = "*"c
        txtNewPassword.Size = New Size(215, 27)
        txtNewPassword.TabIndex = 1
        ' 
        ' txtFirstName
        ' 
        txtFirstName.Location = New Point(203, 236)
        txtFirstName.Name = "txtFirstName"
        txtFirstName.Size = New Size(215, 27)
        txtFirstName.TabIndex = 2
        ' 
        ' txtLastName
        ' 
        txtLastName.Location = New Point(203, 290)
        txtLastName.Name = "txtLastName"
        txtLastName.Size = New Size(215, 27)
        txtLastName.TabIndex = 3
        ' 
        ' txtDepartment
        ' 
        txtDepartment.Location = New Point(203, 350)
        txtDepartment.Name = "txtDepartment"
        txtDepartment.Size = New Size(215, 27)
        txtDepartment.TabIndex = 4
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Location = New Point(33, 447)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(0, 20)
        lblStatus.TabIndex = 5
        ' 
        ' btnAddTeacher
        ' 
        btnAddTeacher.Location = New Point(293, 401)
        btnAddTeacher.Name = "btnAddTeacher"
        btnAddTeacher.Size = New Size(125, 29)
        btnAddTeacher.TabIndex = 6
        btnAddTeacher.Text = "Add Teacher"
        btnAddTeacher.UseVisualStyleBackColor = True
        ' 
        ' addTeacherPanel
        ' 
        addTeacherPanel.Controls.Add(addDepartmentLbl)
        addTeacherPanel.Controls.Add(addLastNameLbl)
        addTeacherPanel.Controls.Add(addFirstNameLbl)
        addTeacherPanel.Controls.Add(addNewPasswordLbl)
        addTeacherPanel.Controls.Add(addNewUsernameLbl)
        addTeacherPanel.Controls.Add(addTeacherLabel)
        addTeacherPanel.Controls.Add(txtNewUsername)
        addTeacherPanel.Controls.Add(lblStatus)
        addTeacherPanel.Controls.Add(btnAddTeacher)
        addTeacherPanel.Controls.Add(txtNewPassword)
        addTeacherPanel.Controls.Add(txtLastName)
        addTeacherPanel.Controls.Add(txtDepartment)
        addTeacherPanel.Controls.Add(txtFirstName)
        addTeacherPanel.Location = New Point(39, 59)
        addTeacherPanel.Name = "addTeacherPanel"
        addTeacherPanel.Size = New Size(456, 483)
        addTeacherPanel.TabIndex = 7
        ' 
        ' addDepartmentLbl
        ' 
        addDepartmentLbl.AutoSize = True
        addDepartmentLbl.Location = New Point(33, 357)
        addDepartmentLbl.Name = "addDepartmentLbl"
        addDepartmentLbl.Size = New Size(92, 20)
        addDepartmentLbl.TabIndex = 12
        addDepartmentLbl.Text = "Department:"
        ' 
        ' addLastNameLbl
        ' 
        addLastNameLbl.AutoSize = True
        addLastNameLbl.Location = New Point(33, 297)
        addLastNameLbl.Name = "addLastNameLbl"
        addLastNameLbl.Size = New Size(82, 20)
        addLastNameLbl.TabIndex = 11
        addLastNameLbl.Text = "Last Name:"
        ' 
        ' addFirstNameLbl
        ' 
        addFirstNameLbl.AutoSize = True
        addFirstNameLbl.Location = New Point(33, 243)
        addFirstNameLbl.Name = "addFirstNameLbl"
        addFirstNameLbl.Size = New Size(83, 20)
        addFirstNameLbl.TabIndex = 10
        addFirstNameLbl.Text = "First Name:"
        ' 
        ' addNewPasswordLbl
        ' 
        addNewPasswordLbl.AutoSize = True
        addNewPasswordLbl.Location = New Point(33, 184)
        addNewPasswordLbl.Name = "addNewPasswordLbl"
        addNewPasswordLbl.Size = New Size(73, 20)
        addNewPasswordLbl.TabIndex = 9
        addNewPasswordLbl.Text = "Password:"
        ' 
        ' addNewUsernameLbl
        ' 
        addNewUsernameLbl.AutoSize = True
        addNewUsernameLbl.Location = New Point(33, 124)
        addNewUsernameLbl.Name = "addNewUsernameLbl"
        addNewUsernameLbl.Size = New Size(78, 20)
        addNewUsernameLbl.TabIndex = 8
        addNewUsernameLbl.Text = "Username:"
        ' 
        ' addTeacherLabel
        ' 
        addTeacherLabel.AutoSize = True
        addTeacherLabel.Location = New Point(171, 29)
        addTeacherLabel.Name = "addTeacherLabel"
        addTeacherLabel.Size = New Size(126, 20)
        addTeacherLabel.TabIndex = 7
        addTeacherLabel.Text = "Add New Teacher"
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(usersTab)
        TabControl1.Controls.Add(instructorsTab)
        TabControl1.Controls.Add(studentsTab)
        TabControl1.Location = New Point(541, 59)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(478, 483)
        TabControl1.TabIndex = 8
        ' 
        ' usersTab
        ' 
        usersTab.Controls.Add(dgvUsers)
        usersTab.Location = New Point(4, 29)
        usersTab.Name = "usersTab"
        usersTab.Padding = New Padding(3)
        usersTab.Size = New Size(470, 450)
        usersTab.TabIndex = 0
        usersTab.Text = "Users"
        usersTab.UseVisualStyleBackColor = True
        ' 
        ' dgvUsers
        ' 
        dgvUsers.AllowUserToAddRows = False
        dgvUsers.AllowUserToDeleteRows = False
        dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvUsers.Location = New Point(-4, 0)
        dgvUsers.Name = "dgvUsers"
        dgvUsers.ReadOnly = True
        dgvUsers.RowHeadersWidth = 51
        dgvUsers.Size = New Size(478, 454)
        dgvUsers.TabIndex = 0
        ' 
        ' instructorsTab
        ' 
        instructorsTab.Controls.Add(dgvInstructors)
        instructorsTab.Location = New Point(4, 29)
        instructorsTab.Name = "instructorsTab"
        instructorsTab.Padding = New Padding(3)
        instructorsTab.Size = New Size(470, 450)
        instructorsTab.TabIndex = 1
        instructorsTab.Text = "Instructors"
        instructorsTab.UseVisualStyleBackColor = True
        ' 
        ' dgvInstructors
        ' 
        dgvInstructors.AllowUserToAddRows = False
        dgvInstructors.AllowUserToDeleteRows = False
        dgvInstructors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvInstructors.Location = New Point(-4, 0)
        dgvInstructors.Name = "dgvInstructors"
        dgvInstructors.ReadOnly = True
        dgvInstructors.RowHeadersWidth = 51
        dgvInstructors.Size = New Size(475, 452)
        dgvInstructors.TabIndex = 0
        ' 
        ' studentsTab
        ' 
        studentsTab.Controls.Add(dgvStudents)
        studentsTab.Location = New Point(4, 29)
        studentsTab.Name = "studentsTab"
        studentsTab.Size = New Size(470, 450)
        studentsTab.TabIndex = 2
        studentsTab.Text = "Students"
        studentsTab.UseVisualStyleBackColor = True
        ' 
        ' dgvStudents
        ' 
        dgvStudents.AllowUserToAddRows = False
        dgvStudents.AllowUserToDeleteRows = False
        dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvStudents.Location = New Point(-4, 0)
        dgvStudents.Name = "dgvStudents"
        dgvStudents.ReadOnly = True
        dgvStudents.RowHeadersWidth = 51
        dgvStudents.Size = New Size(478, 454)
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
        Panel1.Location = New Point(1062, 70)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(386, 472)
        Panel1.TabIndex = 9
        ' 
        ' resetPasswordLbl
        ' 
        resetPasswordLbl.AutoSize = True
        resetPasswordLbl.Location = New Point(122, 101)
        resetPasswordLbl.Name = "resetPasswordLbl"
        resetPasswordLbl.Size = New Size(149, 20)
        resetPasswordLbl.TabIndex = 5
        resetPasswordLbl.Text = "Reset Users Password"
        ' 
        ' newResetPasswordLbl
        ' 
        newResetPasswordLbl.AutoSize = True
        newResetPasswordLbl.Location = New Point(38, 286)
        newResetPasswordLbl.Name = "newResetPasswordLbl"
        newResetPasswordLbl.Size = New Size(107, 20)
        newResetPasswordLbl.TabIndex = 4
        newResetPasswordLbl.Text = "New Password:"
        ' 
        ' targetUsernameLbl
        ' 
        targetUsernameLbl.AutoSize = True
        targetUsernameLbl.Location = New Point(38, 218)
        targetUsernameLbl.Name = "targetUsernameLbl"
        targetUsernameLbl.Size = New Size(78, 20)
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
        txtNewResetPassword.Size = New Size(187, 27)
        txtNewResetPassword.TabIndex = 1
        ' 
        ' txtTargetUsername
        ' 
        txtTargetUsername.Location = New Point(156, 211)
        txtTargetUsername.Name = "txtTargetUsername"
        txtTargetUsername.Size = New Size(187, 27)
        txtTargetUsername.TabIndex = 0
        ' 
        ' btnRefresh
        ' 
        btnRefresh.Location = New Point(925, 24)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(94, 29)
        btnRefresh.TabIndex = 10
        btnRefresh.Text = "Refresh"
        btnRefresh.UseVisualStyleBackColor = True
        ' 
        ' AdminForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1557, 699)
        Controls.Add(btnRefresh)
        Controls.Add(Panel1)
        Controls.Add(TabControl1)
        Controls.Add(addTeacherPanel)
        Name = "AdminForm"
        Text = "AdminForm"
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
        ResumeLayout(False)
    End Sub

    Friend WithEvents txtNewUsername As TextBox
    Friend WithEvents txtNewPassword As TextBox
    Friend WithEvents txtFirstName As TextBox
    Friend WithEvents txtLastName As TextBox
    Friend WithEvents txtDepartment As TextBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnAddTeacher As Button
    Friend WithEvents addTeacherPanel As Panel
    Friend WithEvents addTeacherLabel As Label
    Friend WithEvents addDepartmentLbl As Label
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
End Class
