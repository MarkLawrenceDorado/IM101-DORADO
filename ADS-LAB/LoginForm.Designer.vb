<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LoginForm
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
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 14.0F, System.Drawing.FontStyle.Bold)
        Me.lblTitle.Location = New System.Drawing.Point(36, 24)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(141, 25)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "System Login"
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        Me.lblUsername.Location = New System.Drawing.Point(36, 74)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(81, 17)
        Me.lblUsername.TabIndex = 1
        Me.lblUsername.Text = "Username:"
        '
        'txtUsername
        '
        Me.txtUsername.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        Me.txtUsername.Location = New System.Drawing.Point(145, 70)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(210, 25)
        Me.txtUsername.TabIndex = 2
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        Me.lblPassword.Location = New System.Drawing.Point(36, 118)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(75, 17)
        Me.lblPassword.TabIndex = 3
        Me.lblPassword.Text = "Password:"
        '
        'txtPassword
        '
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        Me.txtPassword.Location = New System.Drawing.Point(145, 114)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = "*"c
        Me.txtPassword.Size = New System.Drawing.Size(210, 25)
        Me.txtPassword.TabIndex = 4
        '
        'btnLogin
        '
        Me.btnLogin.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        Me.btnLogin.Location = New System.Drawing.Point(145, 158)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New Size(120, 34)
        Me.btnLogin.TabIndex = 5
        Me.btnLogin.Text = "Login"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'LoginForm
        '
        Me.AcceptButton = Me.btnLogin
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 224)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.lblTitle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "LoginForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblUsername As System.Windows.Forms.Label
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents btnLogin As System.Windows.Forms.Button
End Class