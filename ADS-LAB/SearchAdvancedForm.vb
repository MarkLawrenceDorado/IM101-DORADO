Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Text

Public Class SearchAdvancedForm

    Private Const OwnCourses As String =
        "SELECT c2.course_id FROM Courses c2 INNER JOIN Instructors i2 ON c2.instructor_id = i2.instructor_id WHERE i2.user_id = @uid"

    Private Const ResultLimit As Integer = 500

    Private ReadOnly _cmbEntity As New ComboBox()
    Private ReadOnly _txtKeyword As New TextBox()
    Private ReadOnly _cmbCourse As New ComboBox()
    Private ReadOnly _chkGrade As New CheckBox()
    Private ReadOnly _numMin As New NumericUpDown()
    Private ReadOnly _numMax As New NumericUpDown()
    Private ReadOnly _chkDates As New CheckBox()
    Private ReadOnly _dtpFrom As New DateTimePicker()
    Private ReadOnly _dtpTo As New DateTimePicker()
    Private ReadOnly _btnSearch As New Button()
    Private ReadOnly _btnClear As New Button()
    Private ReadOnly _lblCount As New Label()
    Private ReadOnly _grid As New DataGridView()

    Public Sub New()
        InitializeComponent()
        BuildUi()
        LoadCourses()
        UpdateControlStates()
    End Sub

    ' ---------- Screen ----------
    Private Sub AddField(panel As FlowLayoutPanel, caption As String, control As Control, width As Integer)
        If caption <> "" Then
            Dim label As New Label()
            label.Text = caption
            label.AutoSize = True
            label.Margin = New Padding(10, 8, 2, 0)
            panel.Controls.Add(label)
        End If

        If width > 0 Then control.Width = width
        control.Margin = New Padding(0, 4, 4, 0)
        panel.Controls.Add(control)
    End Sub

    Private Sub BuildUi()
        Me.Text = "Advanced Search"
        Me.Size = New Size(1000, 620)

        _cmbEntity.DropDownStyle = ComboBoxStyle.DropDownList
        _cmbEntity.Items.AddRange({"Students", "Instructors", "Courses", "Enrollments", "Grades"})
        If SessionManager.RoleId = 1 Then _cmbEntity.Items.Add("Users")
        _cmbEntity.SelectedIndex = 0
        AddHandler _cmbEntity.SelectedIndexChanged, Sub(s, e) UpdateControlStates()

        _cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList

        _chkGrade.Text = "Grade between"
        _chkGrade.AutoSize = True
        _numMin.Minimum = 0
        _numMin.Maximum = 100
        _numMin.Value = 0
        _numMax.Minimum = 0
        _numMax.Maximum = 100
        _numMax.Value = 100
        AddHandler _chkGrade.CheckedChanged, Sub(s, e) UpdateControlStates()

        _chkDates.Text = "Date between"
        _chkDates.AutoSize = True
        _dtpFrom.Format = DateTimePickerFormat.Short
        _dtpTo.Format = DateTimePickerFormat.Short
        _dtpFrom.Value = Date.Today.AddMonths(-6)
        _dtpTo.Value = Date.Today
        AddHandler _chkDates.CheckedChanged, Sub(s, e) UpdateControlStates()

        _btnSearch.Text = "Search"
        _btnSearch.AutoSize = True
        AddHandler _btnSearch.Click, Sub(s, e) RunSearch()

        _btnClear.Text = "Clear"
        _btnClear.AutoSize = True
        AddHandler _btnClear.Click, Sub(s, e) ClearSearch()

        Dim panel As New FlowLayoutPanel()
        panel.Dock = DockStyle.Top
        panel.AutoSize = True
        panel.Padding = New Padding(6)

        AddField(panel, "Search in", _cmbEntity, 120)
        AddField(panel, "Keyword", _txtKeyword, 180)
        AddField(panel, "Course", _cmbCourse, 160)
        AddField(panel, "", _chkGrade, 0)
        AddField(panel, "", _numMin, 55)
        AddField(panel, "and", _numMax, 55)
        AddField(panel, "", _chkDates, 0)
        AddField(panel, "", _dtpFrom, 100)
        AddField(panel, "and", _dtpTo, 100)
        AddField(panel, "", _btnSearch, 0)
        AddField(panel, "", _btnClear, 0)

        _lblCount.Dock = DockStyle.Bottom
        _lblCount.Padding = New Padding(6, 4, 0, 4)
        _lblCount.Height = 26

        _grid.Dock = DockStyle.Fill
        _grid.ReadOnly = True
        _grid.AllowUserToAddRows = False

        Me.Controls.Add(_grid)
        Me.Controls.Add(_lblCount)
        Me.Controls.Add(panel)
        Me.AcceptButton = _btnSearch

        Dim tip As New ToolTip()
        tip.SetToolTip(_cmbEntity, "Choose which kind of records to search")
        tip.SetToolTip(_txtKeyword, "Part of a name, email, course code or similar. Leave empty to list everything.")
        tip.SetToolTip(_cmbCourse, "Limit the results to one course")
        tip.SetToolTip(_chkGrade, "Only for Grades: filter by score range")
        tip.SetToolTip(_chkDates, "Only for Enrollments and Grades: filter by date")
        tip.SetToolTip(_btnSearch, "Run the search (Enter)")
        tip.SetToolTip(_btnClear, "Reset all criteria")
    End Sub

    Private Sub LoadCourses()
        Dim table As New DataTable()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim sql As String = "SELECT course_id, course_code FROM Courses"
            If SessionManager.RoleId = 2 Then sql &= " WHERE course_id IN (" & OwnCourses & ")"
            sql &= " ORDER BY course_code"

            Dim adapter As New MySqlDataAdapter(sql, conn)
            adapter.SelectCommand.Parameters.AddWithValue("@uid", SessionManager.UserId)
            adapter.Fill(table)
        Catch ex As Exception
            ApplicationLogger.LogError("Could not load courses for search", ex)
        Finally
            conn.Close()
        End Try

        Dim anyRow As DataRow = table.NewRow()
        anyRow("course_id") = 0
        anyRow("course_code") = "(Any course)"
        table.Rows.InsertAt(anyRow, 0)

        _cmbCourse.DataSource = table
        _cmbCourse.DisplayMember = "course_code"
        _cmbCourse.ValueMember = "course_id"
    End Sub

    Private Sub UpdateControlStates()
        Dim entity As String = _cmbEntity.Text
        Dim isGrades As Boolean = (entity = "Grades")
        Dim hasDates As Boolean = (entity = "Enrollments" OrElse entity = "Grades")

        _cmbCourse.Enabled = (entity <> "Users")

        _chkGrade.Enabled = isGrades
        _numMin.Enabled = isGrades AndAlso _chkGrade.Checked
        _numMax.Enabled = isGrades AndAlso _chkGrade.Checked

        _chkDates.Enabled = hasDates
        _dtpFrom.Enabled = hasDates AndAlso _chkDates.Checked
        _dtpTo.Enabled = hasDates AndAlso _chkDates.Checked
    End Sub

    Private Function SelectedCourseId() As Integer
        Dim view As DataRowView = TryCast(_cmbCourse.SelectedItem, DataRowView)
        If view Is Nothing Then Return 0
        Return Convert.ToInt32(view("course_id"))
    End Function

    Private Sub ClearSearch()
        _txtKeyword.Clear()
        _cmbCourse.SelectedIndex = 0
        _chkGrade.Checked = False
        _numMin.Value = 0
        _numMax.Value = 100
        _chkDates.Checked = False
        _grid.DataSource = Nothing
        _lblCount.Text = ""
    End Sub

    ' ---------- Search ----------
    Private Sub RunSearch()
        Dim entity As String = _cmbEntity.Text
        Dim keyword As String = _txtKeyword.Text.Trim()
        Dim courseId As Integer = SelectedCourseId()
        Dim isTeacher As Boolean = (SessionManager.RoleId = 2)

        If entity = "Grades" AndAlso _chkGrade.Checked AndAlso _numMin.Value > _numMax.Value Then
            MessageBox.Show("The lowest grade must not be higher than the highest grade.", "Advanced Search")
            Return
        End If

        Dim sql As New StringBuilder()
        Dim params As New List(Of MySqlParameter)
        params.Add(New MySqlParameter("@uid", SessionManager.UserId))

        If keyword <> "" Then
            params.Add(New MySqlParameter("@k", "%" & SearchHelper.EscapeLike(keyword) & "%"))
            params.Add(New MySqlParameter("@exact", keyword))
        End If

        If courseId > 0 Then params.Add(New MySqlParameter("@courseId", courseId))

        Dim dateFilter As Boolean = _chkDates.Checked AndAlso (entity = "Enrollments" OrElse entity = "Grades")
        If dateFilter Then
            params.Add(New MySqlParameter("@fromDate", _dtpFrom.Value.Date))
            params.Add(New MySqlParameter("@toDate", _dtpTo.Value.Date.AddDays(1)))
        End If

        Select Case entity
            Case "Students"
                sql.Append("SELECT s.student_id, s.first_name, s.last_name, s.email, s.date_of_birth FROM Students s WHERE 1=1")
                If keyword <> "" Then
                    sql.Append(" AND (s.first_name LIKE @k OR s.last_name LIKE @k OR s.email LIKE @k" &
                               " OR CONCAT(s.first_name, ' ', s.last_name) LIKE @k OR CAST(s.student_id AS CHAR) = @exact)")
                End If
                If courseId > 0 Then
                    sql.Append(" AND s.student_id IN (SELECT e.student_id FROM Enrollments e WHERE e.course_id = @courseId)")
                End If
                If isTeacher Then
                    sql.Append(" AND s.student_id IN (SELECT e2.student_id FROM Enrollments e2 WHERE e2.course_id IN (" & OwnCourses & "))")
                End If
                sql.Append(" ORDER BY s.last_name, s.first_name")

            Case "Instructors"
                sql.Append("SELECT i.instructor_id, i.first_name, i.last_name, i.department FROM Instructors i WHERE 1=1")
                If keyword <> "" Then
                    sql.Append(" AND (i.first_name LIKE @k OR i.last_name LIKE @k OR i.department LIKE @k" &
                               " OR CONCAT(i.first_name, ' ', i.last_name) LIKE @k)")
                End If
                If courseId > 0 Then
                    sql.Append(" AND i.instructor_id IN (SELECT c.instructor_id FROM Courses c WHERE c.course_id = @courseId)")
                End If
                sql.Append(" ORDER BY i.last_name, i.first_name")

            Case "Courses"
                sql.Append("SELECT c.course_id, c.course_code, c.course_name, CONCAT(i.first_name, ' ', i.last_name) AS instructor " &
                           "FROM Courses c LEFT JOIN Instructors i ON c.instructor_id = i.instructor_id WHERE 1=1")
                If keyword <> "" Then
                    sql.Append(" AND (c.course_code LIKE @k OR c.course_name LIKE @k" &
                               " OR CONCAT(i.first_name, ' ', i.last_name) LIKE @k)")
                End If
                If courseId > 0 Then sql.Append(" AND c.course_id = @courseId")
                If isTeacher Then sql.Append(" AND c.course_id IN (" & OwnCourses & ")")
                sql.Append(" ORDER BY c.course_code")

            Case "Enrollments"
                sql.Append("SELECT e.enrollment_id, CONCAT(s.first_name, ' ', s.last_name) AS student, c.course_code, " &
                           "c.course_name, e.enrollment_date FROM Enrollments e " &
                           "INNER JOIN Students s ON e.student_id = s.student_id " &
                           "INNER JOIN Courses c ON e.course_id = c.course_id WHERE 1=1")
                If keyword <> "" Then
                    sql.Append(" AND (CONCAT(s.first_name, ' ', s.last_name) LIKE @k OR c.course_code LIKE @k OR c.course_name LIKE @k)")
                End If
                If courseId > 0 Then sql.Append(" AND e.course_id = @courseId")
                If dateFilter Then sql.Append(" AND e.enrollment_date >= @fromDate AND e.enrollment_date < @toDate")
                If isTeacher Then sql.Append(" AND e.course_id IN (" & OwnCourses & ")")
                sql.Append(" ORDER BY e.enrollment_date DESC")

            Case "Grades"
                sql.Append("SELECT CONCAT(s.first_name, ' ', s.last_name) AS student, c.course_code, at.type_name, " &
                           "g.grade, g.date_recorded FROM Grades g " &
                           "INNER JOIN Students s ON g.student_id = s.student_id " &
                           "INNER JOIN Courses c ON g.course_id = c.course_id " &
                           "INNER JOIN AssignmentTypes at ON g.type_id = at.type_id WHERE 1=1")
                If keyword <> "" Then
                    sql.Append(" AND (CONCAT(s.first_name, ' ', s.last_name) LIKE @k OR c.course_code LIKE @k OR at.type_name LIKE @k)")
                End If
                If courseId > 0 Then sql.Append(" AND g.course_id = @courseId")
                If _chkGrade.Checked Then
                    sql.Append(" AND g.grade BETWEEN @minGrade AND @maxGrade")
                    params.Add(New MySqlParameter("@minGrade", _numMin.Value))
                    params.Add(New MySqlParameter("@maxGrade", _numMax.Value))
                End If
                If dateFilter Then sql.Append(" AND g.date_recorded >= @fromDate AND g.date_recorded < @toDate")
                If isTeacher Then sql.Append(" AND g.course_id IN (" & OwnCourses & ")")
                sql.Append(" ORDER BY g.date_recorded DESC")

            Case "Users"
                sql.Append("SELECT u.user_id, u.username, r.role_name FROM Users u " &
                           "INNER JOIN Roles r ON u.role_id = r.role_id WHERE 1=1")
                If keyword <> "" Then sql.Append(" AND (u.username LIKE @k OR r.role_name LIKE @k)")
                sql.Append(" ORDER BY u.username")

            Case Else
                Return
        End Select

        sql.Append(" LIMIT " & ResultLimit)

        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter(sql.ToString(), conn)
            adapter.SelectCommand.Parameters.AddRange(params.ToArray())

            Dim table As New DataTable()
            adapter.Fill(table)
            _grid.DataSource = table

            _lblCount.Text = If(table.Rows.Count >= ResultLimit,
                ResultLimit & " result(s) shown. The limit was reached, so narrow your search.",
                table.Rows.Count & " result(s)")

        Catch ex As Exception
            ApplicationLogger.LogError("Advanced search failed", ex)
            MessageBox.Show(GlobalExceptionHandler.FriendlyMessage(ex), "Advanced Search", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            conn.Close()
        End Try
    End Sub

End Class