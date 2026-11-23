<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SplashForm
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Friend WithEvents lblSplashTitle As System.Windows.Forms.Label
    Friend WithEvents lblSplashSubtitle As System.Windows.Forms.Label
    Friend WithEvents pbSplash As System.Windows.Forms.ProgressBar
    Friend WithEvents lblSplashVersion As System.Windows.Forms.Label

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
        Me.lblSplashTitle = New System.Windows.Forms.Label()
        Me.lblSplashSubtitle = New System.Windows.Forms.Label()
        Me.pbSplash = New System.Windows.Forms.ProgressBar()
        Me.lblSplashVersion = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'SplashForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(480, 260)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SplashForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
    End Sub
End Class