Imports System.Diagnostics
Imports System.IO

Module BackupHelper

    Public Sub RestoreDatabaseFromSqlFile(sqlFilePath As String)
        If String.IsNullOrWhiteSpace(sqlFilePath) OrElse Not File.Exists(sqlFilePath) Then
            MessageBox.Show("Select a valid .sql backup file first.", "Missing File", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Restoring a backup will overwrite current data in DB_Dorado. Continue?",
                           "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If

        Dim mysqlExePath As String = "C:\xampp\mysql\bin\mysql.exe"
        Dim dbHost As String = "127.0.0.1"
        Dim dbUser As String = "root"
        Dim dbPassword As String = ""
        Dim dbName As String = "DB_Dorado"

        If Not File.Exists(mysqlExePath) Then
            MessageBox.Show("mysql.exe was not found at: " & mysqlExePath, "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            Dim psi As New ProcessStartInfo() With {
                .FileName = mysqlExePath,
                .UseShellExecute = False,
                .RedirectStandardInput = True,
                .RedirectStandardError = True,
                .CreateNoWindow = True
            }

            If String.IsNullOrEmpty(dbPassword) Then
                psi.Arguments = "-h " & dbHost & " -u " & dbUser & " " & dbName
            Else
                psi.Arguments = "-h " & dbHost & " -u " & dbUser & " -p" & dbPassword & " " & dbName
            End If

            Using proc As Process = Process.Start(psi)
                Using sqlReader As New StreamReader(sqlFilePath)
                    proc.StandardInput.Write(sqlReader.ReadToEnd())
                End Using
                proc.StandardInput.Close()

                Dim stderr As String = proc.StandardError.ReadToEnd()
                proc.WaitForExit()

                If proc.ExitCode <> 0 Then
                    MessageBox.Show("Restore failed: " & stderr, "Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
            End Using

            MessageBox.Show("Database restored successfully from backup.", "Restore Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            For Each frm As Form In Application.OpenForms
                Dim admin As AdminForm = TryCast(frm, AdminForm)
                If admin IsNot Nothing AndAlso Not admin.IsDisposed Then
                    admin.LoadAllLists()
                    admin.LoadEnrollmentLists()
                    admin.LoadAssignmentTypes()
                    admin.LoadAllGrades()
                    admin.LoadCoursesAndInstructors()
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("Error during restore: " & ex.Message, "Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Module