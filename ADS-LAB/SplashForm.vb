Public Class SplashForm
    Inherits Form

    Public Sub New()
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.ClientSize = New Size(480, 260)
        Me.BackColor = Color.FromArgb(0, 90, 158)
        Me.ShowInTaskbar = False

        Dim title As New Label()
        title.Text = "Academic Management System"
        title.Font = New Font("Segoe UI", 22, FontStyle.Bold)
        title.ForeColor = Color.White
        title.AutoSize = False
        title.TextAlign = ContentAlignment.MiddleCenter
        title.SetBounds(0, 60, 480, 60)

        Dim subtitle As New Label()
        subtitle.Text = "Starting, please wait..."
        subtitle.Font = New Font("Segoe UI", 11)
        subtitle.ForeColor = Color.FromArgb(220, 235, 250)
        subtitle.AutoSize = False
        subtitle.TextAlign = ContentAlignment.MiddleCenter
        subtitle.SetBounds(0, 125, 480, 28)

        Dim bar As New ProgressBar()
        bar.Style = ProgressBarStyle.Marquee
        bar.MarqueeAnimationSpeed = 30
        bar.SetBounds(60, 190, 360, 10)

        Dim version As New Label()
        version.Text = "Version " & Application.ProductVersion.Split("+"c)(0)
        version.Font = New Font("Segoe UI", 8)
        version.ForeColor = Color.FromArgb(220, 235, 250)
        version.AutoSize = False
        version.TextAlign = ContentAlignment.MiddleCenter
        version.SetBounds(0, 230, 480, 20)

        Me.Controls.Add(title)
        Me.Controls.Add(subtitle)
        Me.Controls.Add(bar)
        Me.Controls.Add(version)
    End Sub

End Class