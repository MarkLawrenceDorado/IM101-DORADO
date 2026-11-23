Imports System.IO

Public Class ApplicationLogger

    Private Shared ReadOnly LockObject As New Object()

    Public Shared ReadOnly LogPath As String = Path.Combine(AppContext.BaseDirectory, "ApplicationLog.txt")

    Public Shared Sub LogInfo(message As String)
        Log("INFO", message)
    End Sub

    Public Shared Sub LogWarning(message As String)
        Log("WARNING", message)
    End Sub

    Public Shared Sub LogError(message As String, ex As Exception)
        Log("ERROR", $"{message} - {ex.Message} - StackTrace: {ex.StackTrace}")
    End Sub

    Private Shared Sub Log(level As String, message As String)
        Try
            Dim who As String = If(SessionManager.IsActive, SessionManager.Username, "-")
            Dim line As String = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] [{who}] {message}"

            SyncLock LockObject
                File.AppendAllText(LogPath, line & Environment.NewLine)
            End SyncLock
        Catch
            ' Logging must never crash the application
        End Try
    End Sub

End Class