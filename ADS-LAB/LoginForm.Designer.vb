<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LoginForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        usernameLbl = New Label()
        passwordLbl = New Label()
        txtUsername = New TextBox()
        txtPassword = New TextBox()
        btnLogin = New Button()
        loginLbl = New Label()
        SuspendLayout()
        ' 
        ' usernameLbl
        ' 
        usernameLbl.AutoSize = True
        usernameLbl.Location = New Point(84, 181)
        usernameLbl.Name = "usernameLbl"
        usernameLbl.Size = New Size(78, 20)
        usernameLbl.TabIndex = 0
        usernameLbl.Text = "Username:"
        ' 
        ' passwordLbl
        ' 
        passwordLbl.AutoSize = True
        passwordLbl.Location = New Point(84, 265)
        passwordLbl.Name = "passwordLbl"
        passwordLbl.Size = New Size(73, 20)
        passwordLbl.TabIndex = 1
        passwordLbl.Text = "Password:"
        ' 
        ' txtUsername
        ' 
        txtUsername.Location = New Point(189, 174)
        txtUsername.Name = "txtUsername"
        txtUsername.Size = New Size(272, 27)
        txtUsername.TabIndex = 2
        ' 
        ' txtPassword
        ' 
        txtPassword.Location = New Point(189, 258)
        txtPassword.Name = "txtPassword"
        txtPassword.PasswordChar = "*"c
        txtPassword.Size = New Size(272, 27)
        txtPassword.TabIndex = 3
        ' 
        ' btnLogin
        ' 
        btnLogin.Location = New Point(367, 336)
        btnLogin.Name = "btnLogin"
        btnLogin.Size = New Size(94, 29)
        btnLogin.TabIndex = 4
        btnLogin.Text = "Login"
        btnLogin.UseVisualStyleBackColor = True
        ' 
        ' loginLbl
        ' 
        loginLbl.AutoSize = True
        loginLbl.Location = New Point(254, 89)
        loginLbl.Name = "loginLbl"
        loginLbl.Size = New Size(46, 20)
        loginLbl.TabIndex = 5
        loginLbl.Text = "Login"
        ' 
        ' LoginForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(564, 450)
        Controls.Add(loginLbl)
        Controls.Add(btnLogin)
        Controls.Add(txtPassword)
        Controls.Add(txtUsername)
        Controls.Add(passwordLbl)
        Controls.Add(usernameLbl)
        Name = "LoginForm"
        Text = "Login - Academic Management System"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents usernameLbl As Label
    Friend WithEvents passwordLbl As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents btnLogin As Button
    Friend WithEvents loginLbl As Label

End Class
