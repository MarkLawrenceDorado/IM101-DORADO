<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NotificationCenterForm
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Friend WithEvents flpNotificationsTop As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnRefreshNotifications As System.Windows.Forms.Button
    Friend WithEvents lblNotificationsSummary As System.Windows.Forms.Label
    Friend WithEvents dgvNotifications As System.Windows.Forms.DataGridView

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
        Me.flpNotificationsTop = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnRefreshNotifications = New System.Windows.Forms.Button()
        Me.lblNotificationsSummary = New System.Windows.Forms.Label()
        Me.dgvNotifications = New System.Windows.Forms.DataGridView()
        CType(Me.dgvNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NotificationCenterForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(900, 560)
        Me.Name = "NotificationCenterForm"
        Me.Text = "Notification Center"
        CType(Me.dgvNotifications, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub
End Class