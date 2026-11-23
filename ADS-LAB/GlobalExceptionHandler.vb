Imports MySql.Data.MySqlClient
Imports System.Threading

Public Class GlobalExceptionHandler

    Private Shared _isShowing As Boolean

    Public Shared Sub SetupGlobalExceptionHandling()
        Try
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)
        Catch
            ' Already set by the host; the handlers below still work
        End Try

        AddHandler Application.ThreadException, AddressOf Application_ThreadException
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CurrentDomain_UnhandledException
    End Sub

    Private Shared Sub Application_ThreadException(sender As Object, e As ThreadExceptionEventArgs)
        HandleException(e.Exception, False)
    End Sub

    Private Shared Sub CurrentDomain_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs)
        Dim ex As Exception = TryCast(e.ExceptionObject, Exception)
        If ex Is Nothing Then ex = New Exception("A non-standard exception was thrown: " & e.ExceptionObject.ToString())
        HandleException(ex, e.IsTerminating)
    End Sub

    Private Shared Sub HandleException(ex As Exception, isTerminating As Boolean)
        ApplicationLogger.LogError("Unhandled exception occurred", ex)

        If _isShowing Then Return
        _isShowing = True

        Try
            Dim footer As String = If(isTerminating,
                "The application has to close.",
                "The application will continue running.")

            MessageBox.Show(FriendlyMessage(ex) & vbCrLf & vbCrLf & footer & vbCrLf &
                            "Technical details were saved to the application log.",
                            "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _isShowing = False
        End Try
    End Sub

    ' Turns technical exceptions into messages a user can act on
    Public Shared Function FriendlyMessage(ex As Exception) As String
        Dim dbError As MySqlException = TryCast(ex, MySqlException)

        If dbError IsNot Nothing Then
            Select Case dbError.Number
                Case 1045
                    Return "Database access was denied for this account. Please contact an administrator."
                Case 1062
                    Return "That value already exists, so the record could not be saved."
                Case 1142, 1143, 1370
                    Return "You do not have permission to perform this action."
                Case 1451
                    Return "This record is still used by other data, so it cannot be deleted."
                Case 1452
                    Return "The record refers to data that does not exist."
                Case 1644
                    Return dbError.Message
            End Select

            If dbError.Message.Contains("Unable to connect") OrElse dbError.Number = 1042 Then
                Return "The application cannot reach the database. Check that MySQL is running and try again."
            End If

            Return "A database error occurred: " & dbError.Message
        End If

        If TypeOf ex Is TimeoutException Then
            Return "The operation took too long and was cancelled. Please try again."
        End If

        Return "An unexpected error occurred: " & ex.Message
    End Function

End Class