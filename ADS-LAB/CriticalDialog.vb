Public Module CriticalDialog

    ' Asks the user to confirm a critical action. Cancel is the default button.
    ' requireTyping: when set, the user must type that word before the confirm button works.
    Public Function Confirm(owner As IWin32Window, title As String, message As String, confirmText As String,
                            Optional details As String = "", Optional requireTyping As String = "") As Boolean
        Using dialog As New CriticalDialogForm(title, message, confirmText, details, requireTyping)
            Return dialog.ShowDialog(owner) = DialogResult.OK
        End Using
    End Function

End Module

Friend Class CriticalDialogForm
    Inherits Form

    Public Sub New(title As String, message As String, confirmText As String, details As String, requireTyping As String)
        Me.Text = title
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MinimizeBox = False
        Me.MaximizeBox = False
        Me.ShowInTaskbar = False
        Me.AutoSize = True
        Me.AutoSizeMode = AutoSizeMode.GrowAndShrink
        Me.Padding = New Padding(14)

        Dim layout As New FlowLayoutPanel()
        layout.AutoSize = True
        layout.FlowDirection = FlowDirection.TopDown
        layout.WrapContents = False

        Dim header As New FlowLayoutPanel()
        header.AutoSize = True
        header.FlowDirection = FlowDirection.LeftToRight

        Dim icon As New PictureBox()
        icon.Image = SystemIcons.Warning.ToBitmap()
        icon.SizeMode = PictureBoxSizeMode.AutoSize
        icon.Margin = New Padding(0, 0, 12, 0)

        Dim lblMessage As New Label()
        lblMessage.Text = message
        lblMessage.AutoSize = True
        lblMessage.MaximumSize = New Size(380, 0)
        lblMessage.Font = New Font("Segoe UI", 10)

        header.Controls.Add(icon)
        header.Controls.Add(lblMessage)
        layout.Controls.Add(header)

        If details <> "" Then
            Dim box As New TextBox()
            box.Multiline = True
            box.ReadOnly = True
            box.ScrollBars = ScrollBars.Vertical
            box.Width = 430
            box.Height = 90
            box.Text = details
            box.Margin = New Padding(0, 10, 0, 0)
            layout.Controls.Add(box)
        End If

        Dim btnConfirm As New Button()
        btnConfirm.Text = confirmText
        btnConfirm.AutoSize = True
        btnConfirm.DialogResult = DialogResult.OK
        btnConfirm.BackColor = Color.Firebrick
        btnConfirm.ForeColor = Color.White
        btnConfirm.FlatStyle = FlatStyle.Flat

        Dim btnCancel As New Button()
        btnCancel.Text = "Cancel"
        btnCancel.AutoSize = True
        btnCancel.DialogResult = DialogResult.Cancel

        If requireTyping <> "" Then
            Dim lblType As New Label()
            lblType.Text = "Type " & requireTyping & " to confirm:"
            lblType.AutoSize = True
            lblType.Margin = New Padding(0, 12, 0, 2)

            Dim txtType As New TextBox()
            txtType.Width = 200

            btnConfirm.Enabled = False
            AddHandler txtType.TextChanged,
                Sub(s, e) btnConfirm.Enabled = String.Equals(txtType.Text.Trim(), requireTyping, StringComparison.OrdinalIgnoreCase)

            layout.Controls.Add(lblType)
            layout.Controls.Add(txtType)
        End If

        Dim buttons As New FlowLayoutPanel()
        buttons.AutoSize = True
        buttons.FlowDirection = FlowDirection.RightToLeft
        buttons.Width = 430
        buttons.Margin = New Padding(0, 14, 0, 0)
        buttons.Controls.Add(btnCancel)
        buttons.Controls.Add(btnConfirm)
        layout.Controls.Add(buttons)

        Me.Controls.Add(layout)
        Me.AcceptButton = btnCancel
        Me.CancelButton = btnCancel
    End Sub

End Class