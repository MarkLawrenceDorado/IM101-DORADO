<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainMDIParent
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusSession As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusRefresh As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusHint As System.Windows.Forms.ToolStripStatusLabel

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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusSession = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusRefresh = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusHint = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SuspendLayout()
        '
        'MainMDIParent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.IsMdiContainer = True
        Me.Name = "MainMDIParent"
        Me.Text = "Academic Management System"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
End Class