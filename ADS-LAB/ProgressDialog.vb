Imports System.Threading
Imports System.Threading.Tasks

Public Class ProgressResult
    Public Property Completed As Boolean
    Public Property Cancelled As Boolean
    Public Property Failure As Exception
End Class

Public Module ProgressRunner

    ' Runs work on a background task while a progress dialog is shown.
    ' The work must not touch any controls; use ReportProgress to update the dialog.
    Public Function Run(owner As IWin32Window, title As String, work As Action(Of ProgressDialog)) As ProgressResult
        Using dialog As New ProgressDialog(title, work)
            dialog.ShowDialog(owner)

            Dim result As New ProgressResult()
            result.Cancelled = dialog.IsCancelled
            result.Failure = If(dialog.IsCancelled, Nothing, dialog.FailureException)
            result.Completed = (dialog.FailureException Is Nothing AndAlso Not dialog.IsCancelled)
            Return result
        End Using
    End Function

End Module

Public Class ProgressDialog
    Inherits Form

    Private ReadOnly _bar As New ProgressBar()
    Private ReadOnly _lblMessage As New Label()
    Private ReadOnly _btnCancel As New Button()
    Private ReadOnly _cts As New CancellationTokenSource()
    Private ReadOnly _work As Action(Of ProgressDialog)
    Private _error As Exception
    Private _finished As Boolean

    Public Sub New(title As String, work As Action(Of ProgressDialog))
        _work = work

        Me.Text = title
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterParent
        Me.ControlBox = False
        Me.MinimizeBox = False
        Me.MaximizeBox = False
        Me.ShowInTaskbar = False
        Me.ClientSize = New Size(400, 130)

        _lblMessage.Text = "Working..."
        _lblMessage.AutoSize = False
        _lblMessage.SetBounds(16, 14, 368, 36)

        _bar.Minimum = 0
        _bar.Maximum = 100
        _bar.SetBounds(16, 56, 368, 22)

        _btnCancel.Text = "Cancel"
        _btnCancel.SetBounds(304, 90, 80, 28)
        AddHandler _btnCancel.Click, AddressOf OnCancelClicked

        Me.Controls.Add(_lblMessage)
        Me.Controls.Add(_bar)
        Me.Controls.Add(_btnCancel)
    End Sub

    Public ReadOnly Property IsCancelled As Boolean
        Get
            Return _cts.IsCancellationRequested
        End Get
    End Property

    Public ReadOnly Property FailureException As Exception
        Get
            Return _error
        End Get
    End Property

    ' Safe to call from the background task
    Public Sub ReportProgress(percent As Integer, message As String)
        Try
            If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return

            Me.BeginInvoke(Sub()
                               _bar.Value = Math.Min(100, Math.Max(0, percent))
                               _lblMessage.Text = message
                           End Sub)
        Catch
        End Try
    End Sub

    Private Sub OnCancelClicked(sender As Object, e As EventArgs)
        _cts.Cancel()
        _btnCancel.Enabled = False
        _lblMessage.Text = "Cancelling..."
    End Sub

    Private Async Sub ProgressDialog_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            Await Task.Run(Sub() _work(Me))
        Catch ex As Exception
            _error = ex
        End Try

        _finished = True
        Me.DialogResult = If(_error Is Nothing AndAlso Not IsCancelled, DialogResult.OK, DialogResult.Cancel)
        Me.Close()
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If Not _finished Then e.Cancel = True
        MyBase.OnFormClosing(e)
    End Sub

End Class