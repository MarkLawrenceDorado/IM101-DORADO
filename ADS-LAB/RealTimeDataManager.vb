' Forms that can reload their own data implement this interface
Public Interface IRefreshable
    Sub RefreshData()
End Interface

Public Class RealTimeDataManager
    Implements IDisposable

    Private WithEvents refreshTimer As New System.Windows.Forms.Timer()
    Private ReadOnly currentForm As Form

    Public Event Refreshed(refreshedAt As Date)

    Public Sub New(form As Form)
        currentForm = form
        ApplySettings()
    End Sub

    ' Reads the interval and on/off switch from the saved settings
    Public Sub ApplySettings()
        refreshTimer.Interval = Math.Max(5, AppSettings.RefreshIntervalSeconds) * 1000
        refreshTimer.Enabled = AppSettings.AutoRefreshEnabled
    End Sub

    Public Sub SetRefreshInterval(interval As Integer)
        refreshTimer.Interval = Math.Max(1000, interval)
    End Sub

    Public Sub RefreshNow()
        RefreshData()
    End Sub

    Private Sub RefreshTimer_Tick(sender As Object, e As EventArgs) Handles refreshTimer.Tick
        RefreshData()
    End Sub

    Private Sub RefreshData()
        If currentForm.IsDisposed Then Return

        If currentForm.InvokeRequired Then
            currentForm.Invoke(Sub() RefreshData())
            Return
        End If

        If Not SessionManager.IsActive Then Return

        For Each child As Form In currentForm.MdiChildren
            Dim target As IRefreshable = TryCast(child, IRefreshable)
            If target Is Nothing Then Continue For

            Try
                target.RefreshData()
            Catch ex As Exception
                ApplicationLogger.LogError("Auto-refresh failed for " & child.GetType().Name, ex)
            End Try
        Next

        RaiseEvent Refreshed(Date.Now)
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        refreshTimer.Stop()
        refreshTimer.Dispose()
    End Sub

End Class