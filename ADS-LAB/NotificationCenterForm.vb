Imports MySql.Data.MySqlClient
Imports System.Data

Public Class NotificationCenterForm
    Implements IRefreshable

    Private Const OwnCourses As String =
        "SELECT c.course_id FROM Courses c INNER JOIN Instructors i ON c.instructor_id = i.instructor_id WHERE i.user_id = @uid"

    Private ReadOnly _grid As New DataGridView()
    Private ReadOnly _summary As New Label()
    Private ReadOnly _btnRefresh As New Button()
    Private _notes As DataTable

    Public Sub New()
        InitializeComponent()
        BuildUi()
    End Sub

    Private Sub BuildUi()
        Me.Text = "Notification Center"
        Me.Size = New Size(900, 560)

        Dim topBar As New FlowLayoutPanel()
        topBar.Dock = DockStyle.Top
        topBar.AutoSize = True

        _btnRefresh.Text = "Refresh"
        _btnRefresh.AutoSize = True
        AddHandler _btnRefresh.Click, Sub(s, e) LoadNotifications()

        _summary.AutoSize = True
        _summary.Padding = New Padding(8, 7, 0, 0)

        topBar.Controls.Add(_btnRefresh)
        topBar.Controls.Add(_summary)

        _grid.Dock = DockStyle.Fill
        _grid.ReadOnly = True
        _grid.AllowUserToAddRows = False

        Me.Controls.Add(_grid)
        Me.Controls.Add(topBar)

        Dim tip As New ToolTip()
        tip.SetToolTip(_btnRefresh, "Check for new notifications and pending tasks now")
        tip.SetToolTip(_grid, "Double-click a row to see the full message")
    End Sub

    Private Sub NotificationCenterForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadNotifications()
    End Sub

    Public Sub RefreshData() Implements IRefreshable.RefreshData
        Dim scroll As Integer = GridEnhancer.CaptureScroll(_grid)
        LoadNotifications()
        GridEnhancer.RestoreScroll(_grid, scroll)
    End Sub

    ' ---------- Helpers ----------
    Private Sub AddNote(priority As String, category As String, message As String, raisedAt As Date)
        Dim sortValue As Integer = If(priority = "High", 0, If(priority = "Medium", 1, 2))
        _notes.Rows.Add(sortValue, priority, category, message, raisedAt)
    End Sub

    Private Function RunQuery(sql As String) As DataTable
        Dim table As New DataTable()
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter(sql, conn)
            adapter.SelectCommand.Parameters.AddWithValue("@uid", SessionManager.UserId)
            adapter.Fill(table)
        Finally
            conn.Close()
        End Try
        Return table
    End Function

    Private Function ScalarInt(sql As String) As Integer
        Dim table As DataTable = RunQuery(sql)
        If table.Rows.Count = 0 OrElse IsDBNull(table.Rows(0)(0)) Then Return 0
        Return Convert.ToInt32(table.Rows(0)(0))
    End Function

    ' ---------- Build the list ----------
    Private Sub LoadNotifications()
        _notes = New DataTable()
        _notes.Columns.Add("sort", GetType(Integer))
        _notes.Columns.Add("priority", GetType(String))
        _notes.Columns.Add("category", GetType(String))
        _notes.Columns.Add("message", GetType(String))
        _notes.Columns.Add("raised_at", GetType(Date))

        Try
            Select Case SessionManager.RoleId
                Case 1
                    CollectAdminNotes()
                Case 2
                    CollectTeacherNotes()
                Case 3
                    CollectStudentNotes()
            End Select
        Catch ex As Exception
            ApplicationLogger.LogError("Could not collect notifications", ex)
            AddNote("High", "System", "Some notifications could not be loaded: " & GlobalExceptionHandler.FriendlyMessage(ex), Date.Now)
        End Try

        Dim high As Integer = _notes.Select("priority = 'High'").Length
        Dim medium As Integer = _notes.Select("priority = 'Medium'").Length
        Dim info As Integer = _notes.Select("priority = 'Info'").Length

        _summary.Text = If(_notes.Rows.Count = 0,
            "You are all caught up. There are no notifications.",
            high & " high priority, " & medium & " pending task(s), " & info & " other notice(s)")

        _notes.DefaultView.Sort = "sort ASC, raised_at DESC"
        _grid.DataSource = _notes

        _grid.Columns("sort").Visible = False
        _grid.Columns("priority").HeaderText = "Priority"
        _grid.Columns("category").HeaderText = "Category"
        _grid.Columns("message").HeaderText = "Message"
        _grid.Columns("raised_at").HeaderText = "Date / Time"
        _grid.Columns("priority").FillWeight = 12
        _grid.Columns("category").FillWeight = 14
        _grid.Columns("message").FillWeight = 56
        _grid.Columns("raised_at").FillWeight = 18
    End Sub

    Private Sub CollectAdminNotes()
        Dim unenrolled As Integer = ScalarInt(
            "SELECT COUNT(*) FROM Students s WHERE NOT EXISTS (SELECT 1 FROM Enrollments e WHERE e.student_id = s.student_id)")
        If unenrolled > 0 Then AddNote("Medium", "Enrollment", unenrolled & " student(s) are not enrolled in any course.", Date.Now)

        Dim noInstructor As Integer = ScalarInt("SELECT COUNT(*) FROM Courses WHERE instructor_id IS NULL")
        If noInstructor > 0 Then AddNote("Medium", "Courses", noInstructor & " course(s) have no instructor assigned.", Date.Now)

        Dim emptyCourses As Integer = ScalarInt(
            "SELECT COUNT(*) FROM Courses c WHERE NOT EXISTS (SELECT 1 FROM Enrollments e WHERE e.course_id = c.course_id)")
        If emptyCourses > 0 Then AddNote("Info", "Courses", emptyCourses & " course(s) have no enrolled students.", Date.Now)

        Dim backup As DataTable = RunQuery("SELECT status, created_at FROM BackupLog ORDER BY backup_id DESC LIMIT 1")
        If backup.Rows.Count = 0 Then
            AddNote("Medium", "Backup", "No backup has been recorded yet.", Date.Now)
        Else
            Dim createdAt As Date = Convert.ToDateTime(backup.Rows(0)("created_at"))
            Dim status As String = Convert.ToString(backup.Rows(0)("status"))
            Dim daysAgo As Integer = CInt((Date.Now - createdAt).TotalDays)

            If status.Equals("FAILED", StringComparison.OrdinalIgnoreCase) Then
                AddNote("High", "Backup", "The last backup attempt failed.", createdAt)
            ElseIf daysAgo >= 7 Then
                AddNote("Medium", "Backup", "The last backup was " & daysAgo & " day(s) ago.", createdAt)
            End If
        End If

        Dim changes As Integer = ScalarInt("SELECT COUNT(*) FROM AuditLog WHERE action_timestamp >= DATE_SUB(NOW(), INTERVAL 1 DAY)")
        If changes > 0 Then AddNote("Info", "Audit", changes & " change(s) were recorded in the audit log in the last 24 hours.", Date.Now)

        Dim sessions As Integer = ScalarInt("SELECT COUNT(*) FROM UserSessions WHERE is_active = 1")
        AddNote("Info", "Sessions", sessions & " active session(s).", Date.Now)
    End Sub

    Private Sub CollectTeacherNotes()
        Dim courseCount As Integer = ScalarInt("SELECT COUNT(*) FROM (" & OwnCourses & ") own")
        If courseCount = 0 Then
            AddNote("Medium", "Courses", "No courses are assigned to you yet. Please contact an administrator.", Date.Now)
            Return
        End If

        Dim pending As DataTable = RunQuery(
            "SELECT c.course_code, COUNT(*) AS missing " &
            "FROM Courses c " &
            "INNER JOIN Instructors i ON c.instructor_id = i.instructor_id " &
            "INNER JOIN Enrollments e ON e.course_id = c.course_id " &
            "CROSS JOIN AssignmentTypes at " &
            "LEFT JOIN Grades g ON g.student_id = e.student_id AND g.course_id = c.course_id AND g.type_id = at.type_id " &
            "WHERE i.user_id = @uid AND g.student_id IS NULL " &
            "GROUP BY c.course_code ORDER BY c.course_code")
        For Each row As DataRow In pending.Rows
            AddNote("Medium", "Grading", Convert.ToString(row("course_code")) & ": " &
                    Convert.ToString(row("missing")) & " grade(s) still to enter.", Date.Now)
        Next

        Dim atRisk As DataTable = RunQuery(
            "SELECT c.course_code, CONCAT(s.first_name, ' ', s.last_name) AS student_name, ROUND(AVG(g.grade), 2) AS average " &
            "FROM Grades g " &
            "INNER JOIN Courses c ON g.course_id = c.course_id " &
            "INNER JOIN Instructors i ON c.instructor_id = i.instructor_id " &
            "INNER JOIN Students s ON g.student_id = s.student_id " &
            "WHERE i.user_id = @uid " &
            "GROUP BY g.student_id, g.course_id, c.course_code, s.first_name, s.last_name " &
            "HAVING AVG(g.grade) < 60 ORDER BY AVG(g.grade) LIMIT 20")
        For Each row As DataRow In atRisk.Rows
            AddNote("High", "At risk", Convert.ToString(row("student_name")) & " has an average of " &
                    Convert.ToString(row("average")) & " in " & Convert.ToString(row("course_code")) & ".", Date.Now)
        Next

        Dim empty As DataTable = RunQuery(
            "SELECT c.course_code FROM Courses c " &
            "INNER JOIN Instructors i ON c.instructor_id = i.instructor_id " &
            "WHERE i.user_id = @uid AND NOT EXISTS (SELECT 1 FROM Enrollments e WHERE e.course_id = c.course_id)")
        For Each row As DataRow In empty.Rows
            AddNote("Info", "Courses", Convert.ToString(row("course_code")) & " has no enrolled students yet.", Date.Now)
        Next
    End Sub

    Private Sub CollectStudentNotes()
        Dim recent As DataTable = RunQuery(
            "SELECT course_code, type_name, grade, date_recorded FROM StudentGradeSummary " &
            "WHERE user_id = @uid AND date_recorded >= DATE_SUB(NOW(), INTERVAL 7 DAY) ORDER BY date_recorded DESC")
        For Each row As DataRow In recent.Rows
            Dim grade As Decimal = Convert.ToDecimal(row("grade"))
            Dim text As String = "New grade in " & Convert.ToString(row("course_code")) & " (" &
                                 Convert.ToString(row("type_name")) & "): " & grade.ToString("0.00")
            AddNote(If(grade < 60, "High", "Info"), "Grades", text, Convert.ToDateTime(row("date_recorded")))
        Next

        Dim low As DataTable = RunQuery(
            "SELECT course_code, ROUND(AVG(grade), 2) AS average FROM StudentGradeSummary " &
            "WHERE user_id = @uid GROUP BY course_code HAVING AVG(grade) < 60")
        For Each row As DataRow In low.Rows
            AddNote("High", "Courses", "Your average in " & Convert.ToString(row("course_code")) &
                    " is " & Convert.ToString(row("average")) & ", which is below 60.", Date.Now)
        Next

        If ScalarInt("SELECT COUNT(*) FROM StudentGradeSummary WHERE user_id = @uid") = 0 Then
            AddNote("Info", "Grades", "No grades have been posted for you yet.", Date.Now)
        End If
    End Sub

End Class