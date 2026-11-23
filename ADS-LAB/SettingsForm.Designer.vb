<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SettingsForm
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Friend WithEvents chkAutoRefresh As System.Windows.Forms.CheckBox
    Friend WithEvents numRefreshInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkRowColoring As System.Windows.Forms.CheckBox
    Friend WithEvents btnSaveSettings As System.Windows.Forms.Button
    Friend WithEvents btnResetDefaults As System.Windows.Forms.Button
    Friend WithEvents btnOpenLog As System.Windows.Forms.Button
    Friend WithEvents lblSettingsStatus As System.Windows.Forms.Label

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
        Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()
        Me.numRefreshInterval = New System.Windows.Forms.NumericUpDown()
        Me.chkRowColoring = New System.Windows.Forms.CheckBox()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.btnResetDefaults = New System.Windows.Forms.Button()
        Me.btnOpenLog = New System.Windows.Forms.Button()
        Me.lblSettingsStatus = New System.Windows.Forms.Label()
        CType(Me.numRefreshInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'numRefreshInterval
        '
        Me.numRefreshInterval.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.numRefreshInterval.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        '
        'SettingsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(520, 360)
        Me.Name = "SettingsForm"
        Me.Text = "Settings"
        CType(Me.numRefreshInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub
End Class