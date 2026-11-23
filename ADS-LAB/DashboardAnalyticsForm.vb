Imports System.Data
Imports System.Windows.Forms.DataVisualization.Charting
Imports Microsoft.SqlServer
Imports MySql.Data.MySqlClient

Public Class DashboardAnalyticsForm
    Implements IRefreshable

    Private Const OwnCourses As String =
        "SELECT c.course_id FROM Courses c INNER JOIN Instructors i ON c.instructor_id = i.instructor_id WHERE i.user_id = @uid"

    Private ReadOnly _courseChart As Chart = NewChart("Course Performance", True)
    Private ReadOnly _distributionChart As Chart = NewChart("Grade Distribution", False)
    Private ReadOnly _lblUpdated As New Label()
    Private ReadOnly _btnRefresh As New Button()

    Public Sub New()
        InitializeComponent()
        BuildUi()
    End Sub

    Private Shared Function NewChart(title As String, showLegend As Boolean) As Chart
        Dim chart As New Chart()
        chart.Dock = DockStyle.Fill
        chart.ChartAreas.Add(New ChartArea("Main"))
        If showLegend Then chart.Legends.Add(New Legend("Legend"))
        chart.Titles.Add(title)
        Return chart
    End Function

    Private Sub BuildUi()
        Me.Text = "Analytics"
        Me.Size = New Size(1000, 700)

        Dim layout As New TableLayoutPanel()
        layout.Dock = DockStyle.Fill
        layout.ColumnCount = 1
        layout.RowCount = 3
        layout.RowStyles.Add(New RowStyle(SizeType.Absolute, 36))
        layout.RowStyles.Add(New RowStyle(SizeType.Percent, 50))
        layout.RowStyles.Add(New RowStyle(SizeType.Percent, 50))

        Dim topBar As New FlowLayoutPanel()
        topBar.Dock = DockStyle.Fill

        _btnRefresh.Text = "Refresh"
        _btnRefresh.AutoSize = True
        AddHandler _btnRefresh.Click, Sub(s, e) LoadCharts(True)

        _lblUpdated.AutoSize = True
        _lblUpdated.Padding = New Padding(8, 7, 0, 0)

        topBar.Controls.Add(_btnRefresh)
        topBar.Controls.Add(_lblUpdated)

        layout.Controls.Add(topBar, 0, 0)
        layout.Controls.Add(_courseChart, 0, 1)
        layout.Controls.Add(_distributionChart, 0, 2)
        Me.Controls.Add(layout)

        Dim tip As New ToolTip()
        tip.SetToolTip(_btnRefresh, "Reload both charts now")
        tip.SetToolTip(_courseChart, "Average grade per course" & If(SessionManager.RoleId = 3, "", " and number of enrolled students"))
        tip.SetToolTip(_distributionChart, "How many grades fall in each score range")
    End Sub

    Private Sub DashboardAnalyticsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCharts(True)
    End Sub

    Public Sub RefreshData() Implements IRefreshable.RefreshData
        LoadCharts(False)
    End Sub

    Private Sub LoadCharts(showErrors As Boolean)
        Try
            LoadCourseChart()
            LoadDistributionChart()
            _lblUpdated.Text = "Updated " & Date.Now.ToString("HH:mm:ss")
        Catch ex As Exception
            ApplicationLogger.LogError("Could not load charts", ex)
            _lblUpdated.Text = "Could not load chart data (see the application log)"
            If showErrors Then
                MessageBox.Show(GlobalExceptionHandler.FriendlyMessage(ex), "Analytics", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End Try
    End Sub

    Private Function FillTable(sql As String) As DataTable
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

    ' ---------- Chart 1: average grade (columns) and enrollment (line) per course ----------
    Private Sub LoadCourseChart()
        Dim role As Integer = SessionManager.RoleId
        Dim sql As String

        If role = 3 Then
            sql = "SELECT course_code, ROUND(AVG(grade), 2) AS avg_grade, 0 AS enrolled_students " &
                  "FROM StudentGradeSummary WHERE user_id = @uid GROUP BY course_code ORDER BY course_code"
        ElseIf role = 2 Then
            sql = "SELECT course_code, ROUND(COALESCE(average_grade, 0), 2) AS avg_grade, enrolled_students " &
                  "FROM CoursePerformanceAnalytics WHERE course_id IN (" & OwnCourses & ") ORDER BY course_code"
        Else
            sql = "SELECT course_code, ROUND(COALESCE(average_grade, 0), 2) AS avg_grade, enrolled_students " &
                  "FROM CoursePerformanceAnalytics ORDER BY course_code"
        End If

        Dim table As DataTable = FillTable(sql)

        _courseChart.Series.Clear()
        Dim area As ChartArea = _courseChart.ChartAreas(0)
        area.AxisX.LabelStyle.Angle = -45
        area.AxisX.Interval = 1
        area.AxisY.Minimum = 0
        area.AxisY.Maximum = 100
        area.AxisY.Title = "Average grade"

        Dim gradeSeries As New Series("Average Grade")
        gradeSeries.ChartType = SeriesChartType.Column
        gradeSeries.Color = Color.SteelBlue
        gradeSeries.IsValueShownAsLabel = True

        Dim enrollmentSeries As Series = Nothing
        If role <> 3 Then
            enrollmentSeries = New Series("Enrolled Students")
            enrollmentSeries.ChartType = SeriesChartType.Line
            enrollmentSeries.Color = Color.Red
            enrollmentSeries.BorderWidth = 3
            enrollmentSeries.MarkerStyle = MarkerStyle.Circle
            enrollmentSeries.YAxisType = AxisType.Secondary
            area.AxisY2.Title = "Enrolled students"
            area.AxisY2.MajorGrid.Enabled = False
        End If

        For Each row As DataRow In table.Rows
            Dim code As String = Convert.ToString(row("course_code"))
            gradeSeries.Points.AddXY(code, Convert.ToDouble(row("avg_grade")))
            If enrollmentSeries IsNot Nothing Then
                enrollmentSeries.Points.AddXY(code, Convert.ToDouble(row("enrolled_students")))
            End If
        Next

        _courseChart.Series.Add(gradeSeries)
        If enrollmentSeries IsNot Nothing Then _courseChart.Series.Add(enrollmentSeries)

        _courseChart.Titles(0).Text = If(table.Rows.Count = 0, "Course Performance (no data yet)", "Course Performance")
    End Sub

    ' ---------- Chart 2: how many grades fall in each score range ----------
    Private Sub LoadDistributionChart()
        Dim role As Integer = SessionManager.RoleId

        Dim columns As String =
            "SELECT COALESCE(SUM(grade >= 90), 0) AS r1, " &
            "COALESCE(SUM(grade >= 80 AND grade < 90), 0) AS r2, " &
            "COALESCE(SUM(grade >= 70 AND grade < 80), 0) AS r3, " &
            "COALESCE(SUM(grade >= 60 AND grade < 70), 0) AS r4, " &
            "COALESCE(SUM(grade < 60), 0) AS r5 "

        Dim sql As String
        If role = 3 Then
            sql = columns & "FROM StudentGradeSummary WHERE user_id = @uid"
        ElseIf role = 2 Then
            sql = columns & "FROM Grades WHERE course_id IN (" & OwnCourses & ")"
        Else
            sql = columns & "FROM Grades"
        End If

        Dim table As DataTable = FillTable(sql)

        Dim labels() As String = {"90 - 100", "80 - 89", "70 - 79", "60 - 69", "Below 60"}
        Dim colors() As Color = {Color.SeaGreen, Color.YellowGreen, Color.Gold, Color.DarkOrange, Color.IndianRed}

        _distributionChart.Series.Clear()
        Dim area As ChartArea = _distributionChart.ChartAreas(0)
        area.AxisY.Minimum = 0
        area.AxisY.Title = "Number of grades"
        area.AxisX.Title = "Score range"

        Dim series As New Series("Grades")
        series.ChartType = SeriesChartType.Column
        series.IsValueShownAsLabel = True

        Dim total As Integer = 0
        For i As Integer = 0 To 4
            Dim count As Integer = Convert.ToInt32(table.Rows(0)(i))
            total += count
            Dim index As Integer = series.Points.AddXY(labels(i), count)
            series.Points(index).Color = colors(i)
        Next

        _distributionChart.Series.Add(series)
        _distributionChart.Titles(0).Text = If(total = 0, "Grade Distribution (no grades yet)", "Grade Distribution")
    End Sub

End Class