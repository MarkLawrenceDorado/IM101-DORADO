<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProgressDialog
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Friend WithEvents lblProgressMessage As System.Windows.Forms.Label
    Friend WithEvents pbProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents btnCancelProgress As System.Windows.Forms.Button

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
        Me.lblProgressMessage = New System.Windows.Forms.Label()
        Me.pbProgress = New System.Windows.Forms.ProgressBar()
        Me.btnCancelProgress = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ProgressDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 130)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "ProgressDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.ResumeLayout(False)
    End Sub
End Class