Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Globalization

Public Class BulkGradeEntryForm
    Inherits Form

    Private Const OwnCourses As String =
        "SELECT c.course_id FROM Courses c INNER JOIN Instructors i ON c.instructor_id = i.instructor_id WHERE i.user_id = @uid"

    Private Class GradeEntry
        Public Property StudentId As Integer
        Public Property NewGrade As Decimal
    End Class

    Private ReadOnly _cmbCourse As New ComboBox()
    Private ReadOnly _cmbType As New ComboBox()
    Private ReadOnly _btnLoad As New Button()
    Private ReadOnly _btnUndo As New Button()
    Private ReadOnly _btnRedo As New Button()
    Private ReadOnly _btnPaste As New Button()
    Private ReadOnly _btnClear As New Button()
    Private ReadOnly _btnSave As New Button()
    Private ReadOnly _grid As New DataGridView()
    Private ReadOnly _lblStatus As New Label()
    Private ReadOnly _undo As New UndoManager()

    Private _loadedCourseId As Integer
    Private _loadedTypeId As Integer
    Private _loadedLabel As String = ""
    Private _oldValue As String = ""

    Public Sub New()
        Me.Text = "Bulk Grade Entry"
        Me.Size = New Size(900, 600)

        BuildUi()
        LoadLookups()
        UpdateState()
    End Sub

    ' ---------- Screen ----------
    Private Sub BuildUi()
        _cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList
        _cmbCourse.Width = 150
        _cmbType.DropDownStyle = ComboBoxStyle.DropDownList
        _cmbType.Width = 170

        _btnLoad.Text = "Load students"
        _btnLoad.AutoSize = True
        AddHandler _btnLoad.Click, Sub(s, e) LoadStudents()

        Dim top As New FlowLayoutPanel()
        top.Dock = DockStyle.Top
        top.AutoSize = True
        top.Padding = New Padding(6)
        top.Controls.Add(MakeLabel("Course"))
        top.Controls.Add(_cmbCourse)
        top.Controls.Add(MakeLabel("Assignment type"))
        top.Controls.Add(_cmbType)
        top.Controls.Add(_btnLoad)

        _btnUndo.Text = "Undo"
        _btnUndo.AutoSize = True
        AddHandler _btnUndo.Click, Sub(s, e) _undo.Undo()

        _btnRedo.Text = "Redo"
        _btnRedo.AutoSize = True
        AddHandler _btnRedo.Click, Sub(s, e) _undo.Redo()

        _btnPaste.Text = "Paste from clipboard"
        _btnPaste.AutoSize = True
        AddHandler _btnPaste.Click, Sub(s, e) PasteFromClipboard()

        _btnClear.Text = "Clear all"
        _btnClear.AutoSize = True
        AddHandler _btnClear.Click, Sub(s, e) ClearAll()

        _btnSave.Text = "Save all grades"
        _btnSave.AutoSize = True
        AddHandler _btnSave.Click, Sub(s, e) SaveAll()

        Dim toolbar As New FlowLayoutPanel()
        toolbar.Dock = DockStyle.Top
        toolbar.AutoSize = True
        toolbar.Padding = New Padding(6, 0, 6, 6)
        toolbar.Controls.Add(_btnUndo)
        toolbar.Controls.Add(_btnRedo)
        toolbar.Controls.Add(_btnPaste)
        toolbar.Controls.Add(_btnClear)
        toolbar.Controls.Add(_btnSave)

        _grid.Dock = DockStyle.Fill
        _grid.AllowUserToAddRows = False
        _grid.AllowUserToDeleteRows = False
        _grid.RowHeadersVisible = False
        _grid.Tag = "editable"
        _grid.Columns.Add(NewColumn("student_id", "ID", True, True))
        _grid.Columns.Add(NewColumn("student", "Student", True, False))
        _grid.Columns.Add(NewColumn("current", "Current grade", True, False))
        _grid.Columns.Add(NewColumn("newgrade", "New grade", False, False))
        AddHandler _grid.CellBeginEdit, AddressOf GridCellBeginEdit
        AddHandler _grid.CellEndEdit, AddressOf GridCellEndEdit

        _lblStatus.Dock = DockStyle.Bottom
        _lblStatus.Height = 26
        _lblStatus.Padding = New Padding(6, 4, 0, 4)

        Me.Controls.Add(_grid)
        Me.Controls.Add(_lblStatus)
        Me.Controls.Add(toolbar)
        Me.Controls.Add(top)

        AddHandler _undo.Changed, AddressOf UpdateState

        Dim tip As New ToolTip()
        tip.SetToolTip(_cmbCourse, "The course to grade")
        tip.SetToolTip(_cmbType, "The kind of assessment these grades are for")
        tip.SetToolTip(_btnLoad, "List the students enrolled in the course")
        tip.SetToolTip(_btnUndo, "Undo the last change (Ctrl+Z)")
        tip.SetToolTip(_btnRedo, "Redo the change you undid (Ctrl+Y)")
        tip.SetToolTip(_btnPaste, "Paste a column of grades, for example copied from Excel, starting at the selected row")
        tip.SetToolTip(_btnClear, "Empty all the new grades (can be undone)")
        tip.SetToolTip(_btnSave, "Save every entered grade in one step")
        tip.SetToolTip(_grid, "Type grades in the New grade column. Leave a cell empty to skip that student.")
    End Sub

    Private Function MakeLabel(text As String) As Label
        Dim label As New Label()
        label.Text = text
        label.AutoSize = True
        label.Margin = New Padding(8, 8, 2, 0)
        Return label
    End Function

    Private Function NewColumn(name As String, header As String, isReadOnly As Boolean, isHidden As Boolean) As DataGridViewTextBoxColumn
        Dim column As New DataGridViewTextBoxColumn()
        column.Name = name
        column.HeaderText = header
        column.ReadOnly = isReadOnly
        column.Visible = Not isHidden
        column.SortMode = DataGridViewColumnSortMode.NotSortable
        Return column
    End Function

    Private Sub SetStatus(text As String, isError As Boolean)
        _lblStatus.Text = text
        _lblStatus.ForeColor = If(isError, Color.Firebrick, Color.ForestGreen)
    End Sub

    Private Function SelectedId(combo As ComboBox, columnName As String) As Integer
        Dim view As DataRowView = TryCast(combo.SelectedItem, DataRowView)
        If view Is Nothing Then Return 0
        Return Convert.ToInt32(view(columnName))
    End Function

    ' ---------- Data ----------
    Private Sub LoadLookups()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()

            Dim courses As New DataTable()
            Dim sql As String = "SELECT course_id, course_code FROM Courses"
            If SessionManager.RoleId = 2 Then sql &= " WHERE course_id IN (" & OwnCourses & ")"
            sql &= " ORDER BY course_code"
            Dim courseAdapter As New MySqlDataAdapter(sql, conn)
            courseAdapter.SelectCommand.Parameters.AddWithValue("@uid", SessionManager.UserId)
            courseAdapter.Fill(courses)

            Dim types As New DataTable()
            Dim typeAdapter As New MySqlDataAdapter("SELECT type_id, type_name FROM AssignmentTypes ORDER BY type_name", conn)
            typeAdapter.Fill(types)

            _cmbCourse.DataSource = courses
            _cmbCourse.DisplayMember = "course_code"
            _cmbCourse.ValueMember = "course_id"

            _cmbType.DataSource = types
            _cmbType.DisplayMember = "type_name"
            _cmbType.ValueMember = "type_id"

        Catch ex As Exception
            ApplicationLogger.LogError("Could not load bulk grade entry lists", ex)
            SetStatus("Could not load the course list: " & GlobalExceptionHandler.FriendlyMessage(ex), True)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub LoadStudents()
        Dim courseId As Integer = SelectedId(_cmbCourse, "course_id")
        Dim typeId As Integer = SelectedId(_cmbType, "type_id")

        If courseId = 0 OrElse typeId = 0 Then
            SetStatus("Choose a course and an assignment type first.", True)
            Return
        End If

        If HasEntries() AndAlso Not CriticalDialog.Confirm(Me, "Discard Entries",
            "You have grades that are not saved. Loading a different list will discard them.", "Discard and load") Then
            Return
        End If

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter(
                "SELECT s.student_id, CONCAT(s.first_name, ' ', s.last_name) AS student_name, g.grade AS current_grade " &
                "FROM Enrollments e " &
                "INNER JOIN Students s ON e.student_id = s.student_id " &
                "LEFT JOIN Grades g ON g.student_id = e.student_id AND g.course_id = e.course_id AND g.type_id = @typeId " &
                "WHERE e.course_id = @courseId ORDER BY s.last_name, s.first_name", conn)
            adapter.SelectCommand.Parameters.AddWithValue("@typeId", typeId)
            adapter.SelectCommand.Parameters.AddWithValue("@courseId", courseId)

            Dim table As New DataTable()
            adapter.Fill(table)

            _grid.Rows.Clear()
            For Each row As DataRow In table.Rows
                Dim current As String = If(IsDBNull(row("current_grade")), "", Convert.ToDecimal(row("current_grade")).ToString("0.00"))
                _grid.Rows.Add(row("student_id"), row("student_name"), current, "")
            Next

            _loadedCourseId = courseId
            _loadedTypeId = typeId
            _loadedLabel = _cmbCourse.Text & " / " & _cmbType.Text
            _undo.Clear()

            SetStatus(If(table.Rows.Count = 0,
                "No students are enrolled in this course.",
                table.Rows.Count & " student(s) loaded for " & _loadedLabel & ". Type the grades in the New grade column."), table.Rows.Count = 0)

        Catch ex As Exception
            ApplicationLogger.LogError("Could not load students for bulk grade entry", ex)
            SetStatus("Could not load students: " & GlobalExceptionHandler.FriendlyMessage(ex), True)
        Finally
            conn.Close()
        End Try
    End Sub

    ' ---------- Editing ----------
    Private Function TryParseGrade(text As String, ByRef grade As Decimal, ByRef problem As String) As Boolean
        If Not Decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, grade) Then
            problem = "Enter a number."
            Return False
        End If

        If grade < 0 OrElse grade > 100 Then
            problem = "A grade must be between 0 and 100."
            Return False
        End If

        If Math.Round(grade, 2) <> grade Then
            problem = "Use at most 2 decimal places."
            Return False
        End If

        problem = ""
        Return True
    End Function

    Private Sub GridCellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs)
        _oldValue = Convert.ToString(_grid.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
    End Sub

    Private Sub GridCellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        If _grid.Columns(e.ColumnIndex).Name <> "newgrade" Then Return

        Dim cell As DataGridViewCell = _grid.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Dim text As String = Convert.ToString(cell.Value).Trim()

        If text <> "" Then
            Dim grade As Decimal
            Dim problem As String = ""

            If Not TryParseGrade(text, grade, problem) Then
                cell.Value = _oldValue
                cell.ErrorText = ""
                SetStatus(problem, True)
                Return
            End If

            text = grade.ToString("0.00")
        End If

        cell.Value = text
        cell.ErrorText = ""

        If text <> _oldValue Then
            Dim rowIndex As Integer = e.RowIndex
            Dim before As String = _oldValue
            Dim after As String = text

            _undo.Record("Edit grade",
                Sub() SetNewGrade(rowIndex, before),
                Sub() SetNewGrade(rowIndex, after))
        End If

        UpdateState()
    End Sub

    Private Sub SetNewGrade(rowIndex As Integer, value As String)
        If rowIndex >= 0 AndAlso rowIndex < _grid.Rows.Count Then
            _grid.Rows(rowIndex).Cells("newgrade").Value = value
        End If
    End Sub

    Private Function Snapshot() As String()
        Dim values(_grid.Rows.Count - 1) As String
        For i As Integer = 0 To _grid.Rows.Count - 1
            values(i) = Convert.ToString(_grid.Rows(i).Cells("newgrade").Value)
        Next
        Return values
    End Function

    Private Sub Restore(values() As String)
        For i As Integer = 0 To Math.Min(values.Length, _grid.Rows.Count) - 1
            _grid.Rows(i).Cells("newgrade").Value = values(i)
        Next
        UpdateState()
    End Sub

    Private Function HasEntries() As Boolean
        For Each row As DataGridViewRow In _grid.Rows
            If Convert.ToString(row.Cells("newgrade").Value).Trim() <> "" Then Return True
        Next
        Return False
    End Function

    Private Sub ClearAll()
        If Not HasEntries() Then Return

        Dim before() As String = Snapshot()
        Dim after(before.Length - 1) As String
        For i As Integer = 0 To after.Length - 1
            after(i) = ""
        Next

        Restore(after)
        _undo.Record("Clear all", Sub() Restore(before), Sub() Restore(after))
    End Sub

    Private Sub PasteFromClipboard()
        If _grid.Rows.Count = 0 Then
            SetStatus("Load the students first.", True)
            Return
        End If

        If Not Clipboard.ContainsText() Then
            SetStatus("The clipboard does not contain any text.", True)
            Return
        End If

        Dim lines() As String = Clipboard.GetText().Split(New String() {vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
        Dim start As Integer = If(_grid.CurrentRow Is Nothing, 0, _grid.CurrentRow.Index)
        Dim before() As String = Snapshot()
        Dim pasted As Integer = 0
        Dim skipped As Integer = 0

        For i As Integer = 0 To lines.Length - 1
            Dim rowIndex As Integer = start + i
            If rowIndex >= _grid.Rows.Count Then Exit For

            Dim text As String = lines(i).Split(New String() {vbTab}, StringSplitOptions.None)(0).Trim()
            If text = "" Then Continue For

            Dim grade As Decimal
            Dim problem As String = ""
            If TryParseGrade(text, grade, problem) Then
                _grid.Rows(rowIndex).Cells("newgrade").Value = grade.ToString("0.00")
                pasted += 1
            Else
                skipped += 1
            End If
        Next

        Dim after() As String = Snapshot()
        If pasted > 0 Then _undo.Record("Paste grades", Sub() Restore(before), Sub() Restore(after))

        UpdateState()
        SetStatus(pasted & " grade(s) pasted" & If(skipped > 0, ", " & skipped & " skipped because they were not valid grades", "") & ".", skipped > 0 AndAlso pasted = 0)
    End Sub

    Private Sub UpdateState()
        _btnUndo.Enabled = _undo.CanUndo
        _btnRedo.Enabled = _undo.CanRedo
        _btnClear.Enabled = HasEntries()
        _btnSave.Enabled = HasEntries()
        _btnPaste.Enabled = _grid.Rows.Count > 0
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If Not _grid.IsCurrentCellInEditMode Then
            If keyData = (Keys.Control Or Keys.Z) Then
                _undo.Undo()
                Return True
            End If

            If keyData = (Keys.Control Or Keys.Y) Then
                _undo.Redo()
                Return True
            End If
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    ' ---------- Saving ----------
    Private Function CollectEntries() As List(Of GradeEntry)
        Dim entries As New List(Of GradeEntry)

        For Each row As DataGridViewRow In _grid.Rows
            Dim text As String = Convert.ToString(row.Cells("newgrade").Value).Trim()
            If text = "" Then Continue For

            Dim grade As Decimal
            Dim problem As String = ""
            If Not TryParseGrade(text, grade, problem) Then
                row.Cells("newgrade").ErrorText = problem
                SetStatus("Row " & (row.Index + 1) & ": " & problem, True)
                Return Nothing
            End If

            Dim entry As New GradeEntry()
            entry.StudentId = Convert.ToInt32(row.Cells("student_id").Value)
            entry.NewGrade = grade
            entries.Add(entry)
        Next

        Return entries
    End Function

    Private Sub SaveAll()
        Dim entries As List(Of GradeEntry) = CollectEntries()
        If entries Is Nothing Then Return

        If entries.Count = 0 Then
            SetStatus("There are no grades to save.", True)
            Return
        End If

        Dim overwrites As Integer = 0
        For Each row As DataGridViewRow In _grid.Rows
            Dim current As String = Convert.ToString(row.Cells("current").Value)
            Dim entered As String = Convert.ToString(row.Cells("newgrade").Value).Trim()
            If current <> "" AndAlso entered <> "" AndAlso current <> entered Then overwrites += 1
        Next

        Dim details As String = ""
        If overwrites > 0 Then details = overwrites & " existing grade(s) will be replaced by the new values."

        If Not CriticalDialog.Confirm(Me, "Save Grades",
            "Save " & entries.Count & " grade(s) for " & _loadedLabel & "?", "Save grades", details) Then
            Return
        End If

        Dim courseId As Integer = _loadedCourseId
        Dim typeId As Integer = _loadedTypeId
        Dim inserted As Integer = 0
        Dim updated As Integer = 0
        Dim unchanged As Integer = 0

        Dim result As ProgressResult = ProgressRunner.Run(Me, "Saving grades",
            Sub(progress As ProgressDialog)
                Dim conn As MySqlConnection = DBConnection.GetConnection()
                Try
                    conn.Open()
                    Using transaction As MySqlTransaction = conn.BeginTransaction()
                        Try
                            For i As Integer = 0 To entries.Count - 1
                                If progress.IsCancelled Then Throw New OperationCanceledException()

                                Dim entry As GradeEntry = entries(i)
                                Dim existing As Object

                                Using findCmd As New MySqlCommand(
                                    "SELECT grade FROM Grades WHERE student_id = @s AND course_id = @c AND type_id = @t", conn, transaction)
                                    findCmd.Parameters.AddWithValue("@s", entry.StudentId)
                                    findCmd.Parameters.AddWithValue("@c", courseId)
                                    findCmd.Parameters.AddWithValue("@t", typeId)
                                    existing = findCmd.ExecuteScalar()
                                End Using

                                If existing Is Nothing Then
                                    Using insertCmd As New MySqlCommand(
                                        "INSERT INTO Grades (student_id, course_id, type_id, grade) VALUES (@s, @c, @t, @g)", conn, transaction)
                                        insertCmd.Parameters.AddWithValue("@s", entry.StudentId)
                                        insertCmd.Parameters.AddWithValue("@c", courseId)
                                        insertCmd.Parameters.AddWithValue("@t", typeId)
                                        insertCmd.Parameters.AddWithValue("@g", entry.NewGrade)
                                        insertCmd.ExecuteNonQuery()
                                    End Using
                                    inserted += 1

                                ElseIf Convert.ToDecimal(existing) <> entry.NewGrade Then
                                    Using updateCmd As New MySqlCommand(
                                        "UPDATE Grades SET grade = @g, date_recorded = CURRENT_TIMESTAMP " &
                                        "WHERE student_id = @s AND course_id = @c AND type_id = @t", conn, transaction)
                                        updateCmd.Parameters.AddWithValue("@s", entry.StudentId)
                                        updateCmd.Parameters.AddWithValue("@c", courseId)
                                        updateCmd.Parameters.AddWithValue("@t", typeId)
                                        updateCmd.Parameters.AddWithValue("@g", entry.NewGrade)
                                        updateCmd.ExecuteNonQuery()
                                    End Using
                                    updated += 1

                                Else
                                    unchanged += 1
                                End If

                                progress.ReportProgress(CInt((i + 1) * 100 / entries.Count),
                                                        "Saving grade " & (i + 1) & " of " & entries.Count)
                            Next

                            transaction.Commit()
                        Catch
                            transaction.Rollback()
                            Throw
                        End Try
                    End Using
                Finally
                    conn.Close()
                End Try
            End Sub)

        If result.Completed Then
            ApplicationLogger.LogInfo("Bulk grade entry saved for " & _loadedLabel & ": " & inserted & " new, " & updated & " updated")
            LoadStudentsAfterSave()
            SetStatus("Saved: " & inserted & " new, " & updated & " updated, " & unchanged & " unchanged.", False)
        ElseIf result.Cancelled Then
            SetStatus("Saving was cancelled. Nothing was saved.", True)
        Else
            ApplicationLogger.LogError("Bulk grade save failed", result.Failure)
            SetStatus("Nothing was saved. " & GlobalExceptionHandler.FriendlyMessage(result.Failure), True)
        End If
    End Sub

    ' Reloads the list without asking about unsaved entries (they were just saved)
    Private Sub LoadStudentsAfterSave()
        _grid.Rows.Clear()
        _undo.Clear()

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter(
                "SELECT s.student_id, CONCAT(s.first_name, ' ', s.last_name) AS student_name, g.grade AS current_grade " &
                "FROM Enrollments e " &
                "INNER JOIN Students s ON e.student_id = s.student_id " &
                "LEFT JOIN Grades g ON g.student_id = e.student_id AND g.course_id = e.course_id AND g.type_id = @typeId " &
                "WHERE e.course_id = @courseId ORDER BY s.last_name, s.first_name", conn)
            adapter.SelectCommand.Parameters.AddWithValue("@typeId", _loadedTypeId)
            adapter.SelectCommand.Parameters.AddWithValue("@courseId", _loadedCourseId)

            Dim table As New DataTable()
            adapter.Fill(table)

            For Each row As DataRow In table.Rows
                Dim current As String = If(IsDBNull(row("current_grade")), "", Convert.ToDecimal(row("current_grade")).ToString("0.00"))
                _grid.Rows.Add(row("student_id"), row("student_name"), current, "")
            Next
        Catch ex As Exception
            ApplicationLogger.LogError("Could not reload students after saving", ex)
        Finally
            conn.Close()
        End Try
    End Sub

End Class