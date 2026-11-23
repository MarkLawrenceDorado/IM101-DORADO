Imports System.Data
Imports MySql.Data.MySqlClient

Public Module FormHelpers

    Public Sub FilterGrid(grid As DataGridView, searchText As String)
        Dim dt As DataTable = TryCast(grid.DataSource, DataTable)
        If dt Is Nothing Then Return

        Dim q As String = searchText.Trim().Replace("'", "''")
        If q = "" Then
            dt.DefaultView.RowFilter = ""
            Return
        End If

        Dim parts As New List(Of String)
        For Each col As DataColumn In dt.Columns
            If col.DataType Is GetType(String) Then
                parts.Add("[" & col.ColumnName & "] LIKE '%" & q & "%'")
            Else
                parts.Add("Convert([" & col.ColumnName & "], 'System.String') LIKE '%" & q & "%'")
            End If
        Next

        dt.DefaultView.RowFilter = String.Join(" OR ", parts)
    End Sub

    Public Sub AttachAutoComplete(box As TextBox, sql As String)
        If box Is Nothing Then Return

        Dim suggestions As New AutoCompleteStringCollection()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        If Not reader.IsDBNull(0) Then
                            suggestions.Add(reader.GetValue(0).ToString())
                        End If
                    End While
                End Using
            End Using
        Catch
        Finally
            conn.Close()
        End Try

        box.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        box.AutoCompleteSource = AutoCompleteSource.CustomSource
        box.AutoCompleteCustomSource = suggestions
    End Sub

    Public Function ParseLeadingId(text As String) As Integer?
        Dim trimmed As String = text.Trim()
        If trimmed = "" Then Return Nothing

        Dim dashIdx As Integer = trimmed.IndexOf(" - ")
        Dim idPart As String = If(dashIdx >= 0, trimmed.Substring(0, dashIdx).Trim(), trimmed)
        Dim parsed As Integer
        If Integer.TryParse(idPart, parsed) Then
            Return parsed
        End If
        Return Nothing
    End Function

    Public Function ParseCourseCode(text As String) As String
        If String.IsNullOrWhiteSpace(text) Then Return ""
        Dim cleaned As String = text.Trim().ToUpperInvariant()
        Dim dashIdx As Integer = cleaned.IndexOf(" - ")
        If dashIdx >= 0 Then
            cleaned = cleaned.Substring(0, dashIdx).Trim()
        End If
        Return cleaned
    End Function

End Module