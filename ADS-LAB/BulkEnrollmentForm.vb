Imports MySql.Data.MySqlClient
Imports System.Data

Public Class BulkEnrollmentForm
    Inherits Form

    Private ReadOnly _cmbCourse As New ComboBox()
    Private ReadOnly _btnLoad As New Button()
    Private ReadOnly _btnUndo As New Button()
    Private ReadOnly _btnRedo As New Button()
    Private ReadOnly _btnSelectAll As New Button()
    Private ReadOnly _btnClearSelection As New Button()
    Private ReadOnly _btnEnroll As New Button()
    Private ReadOnly _clbStudents As New CheckedListBox()
    Private ReadOnly _lblStatus As New Label()
    Private ReadOnly _undo As New UndoManager()

    Private _suppressItemCheck As Boolean = False

    Public Sub New()
        Me.Text = "Bulk Enrollment"
        Me.Size = New Size(760, 520)

        BuildUi()
        LoadCourses()
        LoadStudents()
        UpdateState()
    End Sub

    Private Sub BuildUi()
        _cmbCourse.Name = "cmbBulkCourse"
        _cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList
        _cmbCourse.Width = 180
        AddHandler _cmbCourse.SelectedIndexChanged, Sub(s, e) HighlightAlreadyEnrolled()

        _btnLoad.Text = "Refresh students"
        _btnLoad.AutoSize = True
        AddHandler _btnLoad.Click, Sub(s, e) LoadStudents()

        Dim top As New FlowLayoutPanel()
        top.Dock = DockStyle.Top
        top.AutoSize = True
        top.Padding = New Padding(8)
        top.Controls.Add(MakeLabel("Target Course:"))
        top.Controls.Add(_cmbCourse)
        top.Controls.Add(_btnLoad)

        _btnUndo.Text = "Undo"
        _btnUndo.AutoSize = True
        AddHandler _btnUndo.Click, Sub(s, e) _undo.Undo()

        _btnRedo.Text = "Redo"
        _btnRedo.AutoSize = True
        AddHandler _btnRedo.Click, Sub(s, e) _undo.Redo()

        _btnSelectAll.Text = "Select all"
        _btnSelectAll.AutoSize = True
        AddHandler _btnSelectAll.Click, Sub(s, e) SetAllChecked(True)

        _btnClearSelection.Text = "Uncheck all"
        _btnClearSelection.AutoSize = True
        AddHandler _btnClearSelection.Click, Sub(s, e) SetAllChecked(False)

        _btnEnroll.Name = "btnBulkEnroll"
        _btnEnroll.Text = "Enroll selected"
        _btnEnroll.AutoSize = True
        AddHandler _btnEnroll.Click, Sub(s, e) EnrollSelected()

        Dim toolbar As New FlowLayoutPanel()
        toolbar.Dock = DockStyle.Top
        toolbar.AutoSize = True
        toolbar.Padding = New Padding(8, 0, 8, 8)
        toolbar.Controls.Add(_btnUndo)
        toolbar.Controls.Add(_btnRedo)
        toolbar.Controls.Add(_btnSelectAll)
        toolbar.Controls.Add(_btnClearSelection)
        toolbar.Controls.Add(_btnEnroll)

        _clbStudents.Name = "clbBulkStudents"
        _clbStudents.Dock = DockStyle.Fill
        _clbStudents.CheckOnClick = True
        AddHandler _clbStudents.ItemCheck, AddressOf OnStudentItemCheck

        _lblStatus.Dock = DockStyle.Bottom
        _lblStatus.Height = 28
        _lblStatus.Padding = New Padding(8, 5, 0, 4)

        Me.Controls.Add(_clbStudents)
        Me.Controls.Add(_lblStatus)
        Me.Controls.Add(toolbar)
        Me.Controls.Add(top)

        AddHandler _undo.Changed, AddressOf UpdateState

        Dim tip As New ToolTip()
        tip.SetToolTip(_cmbCourse, "Select the course to enroll students into")
        tip.SetToolTip(_clbStudents, "Check the students to enroll in the selected course")
        tip.SetToolTip(_btnEnroll, "Enroll all checked students in one transaction")
        tip.SetToolTip(_btnUndo, "Undo the last selection change (Ctrl+Z)")
        tip.SetToolTip(_btnRedo, "Redo the last undone selection change (Ctrl+Y)")
    End Sub

    Private Function MakeLabel(text As String) As Label
        Dim lbl As New Label()
        lbl.Text = text
        lbl.AutoSize = True
        lbl.Margin = New Padding(4, 7, 4, 0)
        Return lbl
    End Function

    Private Sub SetStatus(text As String, isError As Boolean)
        _lblStatus.Text = text
        _lblStatus.ForeColor = If(isError, Color.Firebrick, Color.ForestGreen)
    End Sub

    Private Function SelectedCourseId() As Integer
        Dim view As DataRowView = TryCast(_cmbCourse.SelectedItem, DataRowView)
        If view Is Nothing Then Return 0
        Return Convert.ToInt32(view("course_id"))
    End Function

    Private Sub LoadCourses()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter(
                "SELECT course_id, CONCAT(course_code, ' - ', course_name) AS display_name FROM Courses ORDER BY course_code", conn)
            Dim table As New DataTable()
            adapter.Fill(table)

            _cmbCourse.DataSource = table
            _cmbCourse.DisplayMember = "display_name"
            _cmbCourse.ValueMember = "course_id"
        Catch ex As Exception
            ApplicationLogger.LogError("Could not load courses for bulk enrollment", ex)
            SetStatus("Could not load courses: " & GlobalExceptionHandler.FriendlyMessage(ex), True)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub LoadStudents()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter(
                "SELECT student_id, CONCAT(last_name, ', ', first_name, ' (ID: ', student_id, ')') AS label_text " &
                "FROM Students ORDER BY last_name, first_name", conn)
            Dim table As New DataTable()
            adapter.Fill(table)

            _suppressItemCheck = True
            _clbStudents.Items.Clear()
            For Each row As DataRow In table.Rows
                _clbStudents.Items.Add(New StudentItem With {
                    .StudentId = Convert.ToInt32(row("student_id")),
                    .DisplayText = Convert.ToString(row("label_text"))
                }, False)
            Next
            _undo.Clear()
            SetStatus(_clbStudents.Items.Count & " student(s) available.", False)
        Catch ex As Exception
            ApplicationLogger.LogError("Could not load students for bulk enrollment", ex)
            SetStatus("Could not load students: " & GlobalExceptionHandler.FriendlyMessage(ex), True)
        Finally
            _suppressItemCheck = False
            conn.Close()
        End Try
    End Sub

    Private Sub HighlightAlreadyEnrolled()
        ' Optional visual cue in status bar when switching courses
        Dim courseId As Integer = SelectedCourseId()
        If courseId <= 0 Then Return

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM Enrollments WHERE course_id = @cid", conn)
                cmd.Parameters.AddWithValue("@cid", courseId)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                SetStatus("Selected course currently has " & count & " enrolled student(s).", False)
            End Using
        Catch ex As Exception
            ApplicationLogger.LogError("Could not count course enrollments", ex)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Function CaptureCheckedIds() As HashSet(Of Integer)
        Dim setIds As New HashSet(Of Integer)()
        For i As Integer = 0 To _clbStudents.Items.Count - 1
            If _clbStudents.GetItemChecked(i) Then
                Dim item As StudentItem = DirectCast(_clbStudents.Items(i), StudentItem)
                setIds.Add(item.StudentId)
            End If
        Next
        Return setIds
    End Function

    Private Sub ApplyCheckedIds(ids As HashSet(Of Integer))
        _suppressItemCheck = True
        Try
            For i As Integer = 0 To _clbStudents.Items.Count - 1
                Dim item As StudentItem = DirectCast(_clbStudents.Items(i), StudentItem)
                _clbStudents.SetItemChecked(i, ids.Contains(item.StudentId))
            Next
        Finally
            _suppressItemCheck = False
        End Try
        UpdateState()
    End Sub

    Private Sub OnStudentItemCheck(sender As Object, e As ItemCheckEventArgs)
        If _suppressItemCheck Then Return

        Dim beforeIds As HashSet(Of Integer) = CaptureCheckedIds()
        Me.BeginInvoke(
            Sub()
                Dim afterIds As HashSet(Of Integer) = CaptureCheckedIds()
                If Not beforeIds.SetEquals(afterIds) Then
                    _undo.Record("Change student selection",
                        Sub() ApplyCheckedIds(beforeIds),
                        Sub() ApplyCheckedIds(afterIds))
                End If
                UpdateState()
            End Sub)
    End Sub

    Private Sub SetAllChecked(checked As Boolean)
        Dim beforeIds As HashSet(Of Integer) = CaptureCheckedIds()
        _suppressItemCheck = True
        Try
            For i As Integer = 0 To _clbStudents.Items.Count - 1
                _clbStudents.SetItemChecked(i, checked)
            Next
        Finally
            _suppressItemCheck = False
        End Try
        Dim afterIds As HashSet(Of Integer) = CaptureCheckedIds()

        If Not beforeIds.SetEquals(afterIds) Then
            _undo.Record(If(checked, "Select all students", "Uncheck all students"),
                Sub() ApplyCheckedIds(beforeIds),
                Sub() ApplyCheckedIds(afterIds))
        End If
        UpdateState()
    End Sub

    Private Sub UpdateState()
        _btnUndo.Enabled = _undo.CanUndo
        _btnRedo.Enabled = _undo.CanRedo
        _btnEnroll.Enabled = (CaptureCheckedIds().Count > 0 AndAlso SelectedCourseId() > 0)
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.Z) Then
            _undo.Undo()
            Return True
        End If
        If keyData = (Keys.Control Or Keys.Y) Then
            _undo.Redo()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub EnrollSelected()
        Dim courseId As Integer = SelectedCourseId()
        Dim selectedIds As List(Of Integer) = CaptureCheckedIds().ToList()

        If courseId <= 0 OrElse selectedIds.Count = 0 Then
            SetStatus("Select a course and at least one student first.", True)
            Return
        End If

        If Not CriticalDialog.Confirm(Me, "Bulk Enrollment",
            "Enroll " & selectedIds.Count & " selected student(s) into " & _cmbCourse.Text & "?",
            "Enroll students") Then
            Return
        End If

        Dim enrolledCount As Integer = 0
        Dim skippedCount As Integer = 0

        Dim result As ProgressResult = ProgressRunner.Run(Me, "Enrolling students",
            Sub(progress As ProgressDialog)
                Dim conn As MySqlConnection = DBConnection.GetConnection()
                Try
                    conn.Open()
                    Using tx As MySqlTransaction = conn.BeginTransaction()
                        Try
                            For i As Integer = 0 To selectedIds.Count - 1
                                If progress.IsCancelled Then Throw New OperationCanceledException()

                                Dim sid As Integer = selectedIds(i)
                                Dim exists As Boolean = False

                                Using checkCmd As New MySqlCommand(
                                    "SELECT COUNT(*) FROM Enrollments WHERE student_id = @s AND course_id = @c", conn, tx)
                                    checkCmd.Parameters.AddWithValue("@s", sid)
                                    checkCmd.Parameters.AddWithValue("@c", courseId)
                                    exists = (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                                End Using

                                If exists Then
                                    skippedCount += 1
                                Else
                                    Using insCmd As New MySqlCommand(
                                        "INSERT INTO Enrollments (student_id, course_id, enrollment_date) VALUES (@s, @c, CURDATE())", conn, tx)
                                        insCmd.Parameters.AddWithValue("@s", sid)
                                        insCmd.Parameters.AddWithValue("@c", courseId)
                                        insCmd.ExecuteNonQuery()
                                    End Using
                                    enrolledCount += 1
                                End If

                                progress.ReportProgress(CInt((i + 1) * 100 / selectedIds.Count),
                                                        "Processing student " & (i + 1) & " of " & selectedIds.Count)
                            Next

                            tx.Commit()
                        Catch
                            tx.Rollback()
                            Throw
                        End Try
                    End Using
                Finally
                    conn.Close()
                End Try
            End Sub)

        If result.Completed Then
            ApplicationLogger.LogInfo("Bulk enrollment for course_id=" & courseId & ": " & enrolledCount & " enrolled, " & skippedCount & " already enrolled")
            SetAllChecked(False)
            _undo.Clear()
            SetStatus("Enrollment complete: " & enrolledCount & " added, " & skippedCount & " already enrolled.", False)
        ElseIf result.Cancelled Then
            SetStatus("Bulk enrollment cancelled. No changes were saved.", True)
        Else
            ApplicationLogger.LogError("Bulk enrollment failed", result.Failure)
            SetStatus("Enrollment failed: " & GlobalExceptionHandler.FriendlyMessage(result.Failure), True)
        End If
    End Sub

    Private Class StudentItem
        Public Property StudentId As Integer
        Public Property DisplayText As String
        Public Overrides Function ToString() As String
            Return DisplayText
        End Function
    End Class

End Class