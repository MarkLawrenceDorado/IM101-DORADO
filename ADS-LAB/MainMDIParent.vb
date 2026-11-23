Imports System.Diagnostics

Public Class MainMDIParent

    Private ReadOnly _userId As Integer
    Private ReadOnly _menu As New MenuStrip()
    Private ReadOnly _status As New StatusStrip()
    Private ReadOnly _tsSession As New ToolStripStatusLabel()
    Private ReadOnly _tsRefresh As New ToolStripStatusLabel()
    Private ReadOnly _tsHint As New ToolStripStatusLabel()
    Private _realTime As RealTimeDataManager
    Private Shared _currentInstance As MainMDIParent


    Public Sub New(userId As Integer)
        InitializeComponent()
        _userId = userId

        Me.IsMdiContainer = True
        Me.Text = "Academic Management System"
        Me.WindowState = FormWindowState.Maximized

        BuildStatusBar()
        BuildMenu()
    End Sub

    ' ---------- Status bar ----------
    Private Sub BuildStatusBar()
        _tsHint.Spring = True
        _tsHint.TextAlign = ContentAlignment.MiddleRight

        _status.Items.AddRange(New ToolStripItem() {
            _tsSession, New ToolStripSeparator(), _tsRefresh, New ToolStripSeparator(), _tsHint})

        SessionManager.AttachStatusLabel(_tsSession, Me)
        Me.Controls.Add(_status)
    End Sub

    Private Sub UpdateRefreshLabel()
        _tsRefresh.Text = If(AppSettings.AutoRefreshEnabled,
            "Auto-refresh every " & AppSettings.RefreshIntervalSeconds & " s",
            "Auto-refresh off")
    End Sub

    Private Sub OnRefreshed(refreshedAt As Date)
        _tsRefresh.Text = "Last refresh: " & refreshedAt.ToString("HH:mm:ss")
    End Sub

    ' ---------- Menus (shown according to the user's role) ----------
    Private Function NewItem(text As String, onClick As Action, hint As String) As ToolStripMenuItem
        Dim item As New ToolStripMenuItem(text)
        item.ToolTipText = hint

        AddHandler item.Click, Sub(s, e) onClick()
        AddHandler item.MouseEnter, Sub(s, e) _tsHint.Text = hint
        AddHandler item.MouseLeave, Sub(s, e) _tsHint.Text = ""

        Return item
    End Function

    Private Sub BuildMenu()
        Dim role As Integer = SessionManager.RoleId

        Dim mnuFile As New ToolStripMenuItem("&File")
        mnuFile.DropDownItems.Add(NewItem("&Logout", AddressOf OnLogout, "Sign out and return to the login screen"))
        mnuFile.DropDownItems.Add(New ToolStripSeparator())
        mnuFile.DropDownItems.Add(NewItem("E&xit", AddressOf OnExit, "Sign out and close the application"))
        _menu.Items.Add(mnuFile)

        If role = 1 Then
            Dim mnuAdmin As New ToolStripMenuItem("&Administration")
            mnuAdmin.DropDownItems.Add(NewItem("Admin &Console",
                Sub() OpenChild(Of AdminForm)(Function() New AdminForm(_userId)),
                "Manage users, courses, enrollment, assignment types and grades"))
            mnuAdmin.DropDownItems.Add(NewItem("&Audit Log",
                Sub() OpenChild(Of AuditLogForm)(Function() New AuditLogForm()),
                "Review who changed which records"))
            mnuAdmin.DropDownItems.Add(NewItem("&Backups",
                Sub() OpenChild(Of BackupForm)(Function() New BackupForm()),
                "Create a backup and view backup history"))
            _menu.Items.Add(mnuAdmin)

            mnuAdmin.DropDownItems.Add(NewItem("Bulk &Enrollment",
            Sub() OpenChild(Of BulkEnrollmentForm)(Function() New BulkEnrollmentForm()),
            "Enroll multiple students at once"))

        End If

        If role = 2 Then
            Dim mnuTeaching As New ToolStripMenuItem("&Teaching")
            mnuTeaching.DropDownItems.Add(NewItem("Teacher &Dashboard",
                Sub() OpenChild(Of TeacherDashboard)(Function() New TeacherDashboard(_userId)),
                "Course overview, grade entry and final grades"))
            _menu.Items.Add(mnuTeaching)

            mnuTeaching.DropDownItems.Add(NewItem("Bulk &Grade Entry",
            Sub() OpenChild(Of BulkGradeEntryForm)(Function() New BulkGradeEntryForm()),
            "Enter or paste grades for multiple students"))

        End If

        If role = 3 Then
            Dim mnuStudies As New ToolStripMenuItem("&My Studies")
            mnuStudies.DropDownItems.Add(NewItem("My &Grades",
                Sub() OpenChild(Of StudentDashboard)(Function() New StudentDashboard(_userId)),
                "Your grades and course averages"))
            _menu.Items.Add(mnuStudies)
        End If

        If role = 1 OrElse role = 2 Then
            Dim mnuReports As New ToolStripMenuItem("&Reports")
            mnuReports.DropDownItems.Add(NewItem("&Reports and Exports",
                Sub() OpenChild(Of ReportsForm)(Function() New ReportsForm(_userId, SessionManager.RoleId)),
                "Student transcripts and course analytics with CSV and PDF export"))
            _menu.Items.Add(mnuReports)
        End If

        Dim mnuTools As New ToolStripMenuItem("&Tools")
        mnuTools.DropDownItems.Add(NewItem("&Analytics",
            Sub() OpenChild(Of DashboardAnalyticsForm)(Function() New DashboardAnalyticsForm()),
            "Charts of grade distribution and course statistics"))
        mnuTools.DropDownItems.Add(NewItem("&Notification Center",
            Sub() OpenChild(Of NotificationCenterForm)(Function() New NotificationCenterForm()),
            "System notifications and pending tasks"))
        If role = 1 OrElse role = 2 Then
            mnuTools.DropDownItems.Add(NewItem("Advanced &Search",
                Sub() OpenChild(Of SearchAdvancedForm)(Function() New SearchAdvancedForm()),
                "Search students, courses, enrollments and grades with several criteria"))
        End If
        mnuTools.DropDownItems.Add(New ToolStripSeparator())
        mnuTools.DropDownItems.Add(NewItem("S&ettings",
            Sub() OpenChild(Of SettingsForm)(Function() New SettingsForm()),
            "Refresh interval and display options"))
        _menu.Items.Add(mnuTools)

        Dim mnuWindow As New ToolStripMenuItem("&Window")
        mnuWindow.DropDownItems.Add(NewItem("&Cascade", Sub() Me.LayoutMdi(MdiLayout.Cascade), "Arrange windows in a cascade"))
        mnuWindow.DropDownItems.Add(NewItem("Tile &Horizontally", Sub() Me.LayoutMdi(MdiLayout.TileHorizontal), "Tile windows one above the other"))
        mnuWindow.DropDownItems.Add(NewItem("Tile &Vertically", Sub() Me.LayoutMdi(MdiLayout.TileVertical), "Tile windows side by side"))
        mnuWindow.DropDownItems.Add(New ToolStripSeparator())
        mnuWindow.DropDownItems.Add(NewItem("Close &All", AddressOf CloseAllChildren, "Close every open window"))
        _menu.Items.Add(mnuWindow)
        _menu.MdiWindowListItem = mnuWindow

        If role = 1 Then
            Dim mnuHelp As New ToolStripMenuItem("&Help")
            mnuHelp.DropDownItems.Add(NewItem("Open Application &Log", AddressOf OpenLogFile, "Open the application log file"))
            mnuHelp.DropDownItems.Add(NewItem("&Simulate Error (demo)",
                Sub() Throw New InvalidOperationException("Demo error: this is a test of the global exception handler."),
                "Raise a test error to show the error handling"))
            _menu.Items.Add(mnuHelp)
        End If

        Me.Controls.Add(_menu)
        Me.MainMenuStrip = _menu
    End Sub

    ' ---------- Opening child windows ----------
    Private Sub OpenChild(Of T As Form)(create As Func(Of T))
        For Each child As Form In Me.MdiChildren
            If TypeOf child Is T Then
                If child.WindowState = FormWindowState.Minimized Then child.WindowState = FormWindowState.Normal
                child.Activate()
                Return
            End If
        Next

        Try
            Dim newForm As T = create()
            newForm.MdiParent = Me
            newForm.WindowState = FormWindowState.Maximized
            GridEnhancer.EnhanceAll(newForm)
            newForm.Show()
        Catch ex As Exception
            ApplicationLogger.LogError("Could not open " & GetType(T).Name, ex)
            MessageBox.Show(GlobalExceptionHandler.FriendlyMessage(ex), "Cannot Open Window",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub CloseAllChildren()
        For Each child As Form In Me.MdiChildren
            child.Close()
        Next
    End Sub

    Private Sub OnLogout()
        Me.Close()
    End Sub

    Private Sub OnExit()
        SessionManager.EndSession()
        Application.Exit()
    End Sub

    Private Sub OpenLogFile()
        If Not IO.File.Exists(ApplicationLogger.LogPath) Then
            MessageBox.Show("No log entries have been written yet.", "Application Log")
            Return
        End If

        Process.Start(New ProcessStartInfo(ApplicationLogger.LogPath) With {.UseShellExecute = True})
    End Sub

    ' ---------- Load, settings and closing ----------
    Private Sub MainMDIParent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _realTime = New RealTimeDataManager(Me)
        AddHandler _realTime.Refreshed, AddressOf OnRefreshed
        AddHandler AppSettings.SettingsChanged, AddressOf OnSettingsChanged
        UpdateRefreshLabel()

        Select Case SessionManager.RoleId
            Case 1
                OpenChild(Of AdminForm)(Function() New AdminForm(_userId))
            Case 2
                OpenChild(Of TeacherDashboard)(Function() New TeacherDashboard(_userId))
            Case 3
                OpenChild(Of StudentDashboard)(Function() New StudentDashboard(_userId))
        End Select

        ApplicationLogger.LogInfo("Main window opened for " & SessionManager.Username & " (" & SessionManager.RoleName() & ")")
    End Sub

    Private Sub OnSettingsChanged()
        _realTime.ApplySettings()
        UpdateRefreshLabel()

        For Each child As Form In Me.MdiChildren
            GridEnhancer.Repaint(child)
        Next
    End Sub

    Private Sub MainMDIParent_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        RemoveHandler AppSettings.SettingsChanged, AddressOf OnSettingsChanged

        If _realTime IsNot Nothing Then
            _realTime.Dispose()
            _realTime = Nothing
        End If
        If _currentInstance Is Me Then _currentInstance = Nothing
    End Sub

    Public Shared Sub ShowHint(text As String)
        If _currentInstance IsNot Nothing AndAlso Not _currentInstance.IsDisposed Then
            _currentInstance._tsHint.Text = If(text, "")
        End If
    End Sub

End Class