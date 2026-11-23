Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Text

Public Module SearchHelper

    ' Fills a TextBox's autocomplete list from a query that returns one text column.
    Public Sub AttachAutoComplete(box As TextBox, sql As String, ParamArray params() As MySqlParameter)
        Dim source As New AutoCompleteStringCollection()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddRange(params)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        If Not reader.IsDBNull(0) Then source.Add(reader.GetString(0))
                    End While
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading suggestions: " & ex.Message, "Error")
        Finally
            conn.Close()
        End Try

        box.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        box.AutoCompleteSource = AutoCompleteSource.CustomSource
        box.AutoCompleteCustomSource = source
    End Sub

    ' Client-side filter for grids already loaded from a DataTable (small lists).
    Public Sub FilterGrid(grid As DataGridView, term As String)
        Dim table As DataTable = TryCast(grid.DataSource, DataTable)
        If table Is Nothing Then Return

        term = term.Trim()
        If term = "" Then
            table.DefaultView.RowFilter = ""
            Return
        End If

        Dim safe As New StringBuilder()
        For Each c As Char In term
            Select Case c
                Case "'"c
                    safe.Append("''")
                Case "*"c, "%"c, "["c, "]"c
                    safe.Append("[").Append(c).Append("]")
                Case Else
                    safe.Append(c)
            End Select
        Next

        Dim parts As New List(Of String)
        For Each column As DataColumn In table.Columns
            parts.Add("Convert([" & column.ColumnName & "], 'System.String') LIKE '%" & safe.ToString() & "%'")
        Next

        table.DefaultView.RowFilter = String.Join(" OR ", parts)
    End Sub

    ' Escapes LIKE wildcards so user text is matched literally.
    Public Function EscapeLike(text As String) As String
        Return text.Replace("\", "\\").Replace("%", "\%").Replace("_", "\_")
    End Function

    ' "12 - Jane Doe" -> 12, plain "12" -> 12, anything else -> Nothing
    Public Function ParseLeadingId(text As String) As Integer?
        Dim head As String = text.Trim().Split({" "c}, 2)(0)
        Dim id As Integer
        If Integer.TryParse(head, id) Then Return id
        Return Nothing
    End Function

    ' "CS101 - Intro to Programming" -> "CS101"
    Public Function ParseCourseCode(text As String) As String
        Dim index As Integer = text.IndexOf(" - ", StringComparison.Ordinal)
        Return If(index > 0, text.Substring(0, index), text).Trim()
    End Function

End Module