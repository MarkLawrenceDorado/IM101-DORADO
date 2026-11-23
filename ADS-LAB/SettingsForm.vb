Imports System.Diagnostics

Public Class SettingsForm

    Private ReadOnly _chkAuto As New CheckBox()
    Private ReadOnly _numInterval As New NumericUpDown()
    Private ReadOnly _chkColor As New CheckBox()
    Private ReadOnly _btnSave As New Button()
    Private ReadOnly _btnDefaults As New Button()
    Private ReadOnly _btnLog As New Button()
    Private ReadOnly _lblStatus As New Label()

    Public Sub New()
        InitializeComponent()
        BuildUi()
        LoadValues()
    End Sub

    Private Sub BuildUi()
        Me.Text = "Settings"
        Me.Size = New Size(520, 360)

        Dim panel As New FlowLayoutPanel()
        panel.Dock = DockStyle.Fill
        panel.FlowDirection = FlowDirection.TopDown
        panel.WrapContents = False
        panel.Padding = New Padding(20)

        _chkAuto.Text = "Refresh open windows automatically"
        _chkAuto.AutoSize = True
        AddHandler _chkAuto.CheckedChanged, Sub(s, e) _numInterval.Enabled = _chkAuto.Checked

        Dim intervalRow As New FlowLayoutPanel()
        intervalRow.AutoSize = True
        intervalRow.FlowDirection = FlowDirection.LeftToRight

        Dim lblEvery As New Label()
        lblEvery.Text = "Refresh every"
        lblEvery.AutoSize = True
        lblEvery.Margin = New Padding(0, 6, 4, 0)

        _numInterval.Minimum = 5
        _numInterval.Maximum = 3600
        _numInterval.Width = 70

        Dim lblSeconds As New Label()
        lblSeconds.Text = "seconds (5 to 3600)"
        lblSeconds.AutoSize = True
        lblSeconds.Margin = New Padding(4, 6, 0, 0)

        intervalRow.Controls.Add(lblEvery)
        intervalRow.Controls.Add(_numInterval)
        intervalRow.Controls.Add(lblSeconds)

        _chkColor.Text = "Color table rows by value (low grades, statuses, priorities)"
        _chkColor.AutoSize = True

        Dim lblSecurity As New Label()
        lblSecurity.Text = "Automatic logout after " & SessionManager.TimeoutMinutes &
                           " minutes of inactivity. This is set by the security policy and cannot be changed here."
        lblSecurity.MaximumSize = New Size(440, 0)
        lblSecurity.AutoSize = True
        lblSecurity.ForeColor = SystemColors.GrayText
        lblSecurity.Margin = New Padding(0, 12, 0, 12)

        Dim buttonRow As New FlowLayoutPanel()
        buttonRow.AutoSize = True
        buttonRow.FlowDirection = FlowDirection.LeftToRight

        _btnSave.Text = "Save"
        _btnSave.AutoSize = True
        AddHandler _btnSave.Click, Sub(s, e) SaveValues()

        _btnDefaults.Text = "Reset to defaults"
        _btnDefaults.AutoSize = True
        AddHandler _btnDefaults.Click, Sub(s, e) ResetToDefaults()

        _btnLog.Text = "Open log file"
        _btnLog.AutoSize = True
        AddHandler _btnLog.Click, Sub(s, e) OpenLog()

        buttonRow.Controls.Add(_btnSave)
        buttonRow.Controls.Add(_btnDefaults)
        buttonRow.Controls.Add(_btnLog)

        _lblStatus.AutoSize = True
        _lblStatus.Margin = New Padding(0, 10, 0, 0)

        panel.Controls.Add(_chkAuto)
        panel.Controls.Add(intervalRow)
        panel.Controls.Add(_chkColor)
        panel.Controls.Add(lblSecurity)
        panel.Controls.Add(buttonRow)
        panel.Controls.Add(_lblStatus)
        Me.Controls.Add(panel)

        Dim tip As New ToolTip()
        tip.SetToolTip(_chkAuto, "Turn the automatic reloading of dashboards and charts on or off")
        tip.SetToolTip(_numInterval, "How often open windows reload their data")
        tip.SetToolTip(_chkColor, "Highlight rows such as low grades and failed backups")
        tip.SetToolTip(_btnSave, "Save the settings and apply them now")
        tip.SetToolTip(_btnDefaults, "Fill in the default values (you still need to save)")
        tip.SetToolTip(_btnLog, "Open the application log in your default text editor")
    End Sub

    Private Sub LoadValues()
        _chkAuto.Checked = AppSettings.AutoRefreshEnabled
        _numInterval.Value = AppSettings.RefreshIntervalSeconds
        _numInterval.Enabled = _chkAuto.Checked
        _chkColor.Checked = AppSettings.RowColoringEnabled
    End Sub

    Private Sub SaveValues()
        AppSettings.Update(_chkAuto.Checked, CInt(_numInterval.Value), _chkColor.Checked)
        _lblStatus.Text = "Settings saved and applied."
        _lblStatus.ForeColor = Color.Green
        ApplicationLogger.LogInfo("Settings changed: auto-refresh=" & _chkAuto.Checked &
                                  ", interval=" & CInt(_numInterval.Value) & "s, row coloring=" & _chkColor.Checked)
    End Sub

    Private Sub ResetToDefaults()
        _chkAuto.Checked = True
        _numInterval.Value = 30
        _chkColor.Checked = True
        _lblStatus.Text = "Defaults filled in. Click Save to apply them."
        _lblStatus.ForeColor = SystemColors.ControlText
    End Sub

    Private Sub OpenLog()
        If Not IO.File.Exists(ApplicationLogger.LogPath) Then
            MessageBox.Show("No log entries have been written yet.", "Application Log")
            Return
        End If

        Process.Start(New ProcessStartInfo(ApplicationLogger.LogPath) With {.UseShellExecute = True})
    End Sub

End Class