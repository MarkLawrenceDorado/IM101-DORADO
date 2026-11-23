Public Module UiHelper

    Private ReadOnly Tips As New Dictionary(Of Form, ToolTip)()

    ' Control name -> hint text. Controls not listed here are left alone.
    Private ReadOnly Hints As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase) From {
        {"cmbUserType", "Choose whether to create a teacher or a student account"},
        {"txtNewUsername", "Login name: 3 to 30 letters, numbers, dots or underscores"},
        {"txtNewPassword", "At least 8 characters with an uppercase letter, a lowercase letter and a number"},
        {"txtFirstName", "First name (letters only)"},
        {"txtLastName", "Last name (letters only)"},
        {"txtDepartment", "The teacher's department"},
        {"txtEmail", "The student's email address (must be unique)"},
        {"dtpDateOfBirth", "The student's date of birth"},
        {"btnAddTeacher", "Create the account and its teacher or student profile"},
        {"btnRefresh", "Reload the lists from the database"},
        {"txtTargetUsername", "Username of the account whose password will be reset"},
        {"txtNewResetPassword", "The new password to give the user"},
        {"btnResetPassword", "Replace the user's password with the new one"},
        {"txtTypeName", "Name of the assignment type, for example Quiz"},
        {"txtDefaultWeight", "Weight of this type in the final grade, in percent"},
        {"btnAddType", "Add a new assignment type"},
        {"btnUpdateType", "Save changes to the selected assignment type"},
        {"btnDeleteType", "Delete the selected assignment type"},
        {"dgvUsers", "All user accounts. Double-click a row for details, right-click to export."},
        {"dgvInstructors", "All instructors. Double-click a row for details, right-click to export."},
        {"dgvStudents", "All students. Double-click a row for details, right-click to export."},
        {"dgvAssignmentTypes", "Assignment types and their default weights"},
        {"dgvAllGrades", "Every grade in the system (read-only)"},
        {"dgvCourses", "All courses and their instructors"},
        {"cmbEnrollStudent", "The student to enroll"},
        {"cmbEnrollCourse", "The course to enroll the student in"},
        {"btnEnrollStudent", "Enroll the selected student in the selected course"},
        {"clbBulkStudents", "Tick every student to enroll"},
        {"cmbBulkCourse", "The course for the bulk enrollment"},
        {"btnBulkEnroll", "Enroll all ticked students in the course"},
        {"txtCourseCode", "Course code: 2 to 5 letters followed by 3 digits, for example CS101"},
        {"txtCourseName", "Full name of the course"},
        {"cmbCourseInstructor", "The instructor teaching the course (optional)"},
        {"btnAddCourse", "Create the course"},
        {"txtGradeStudent", "Start typing a student name or ID"},
        {"txtGradeCourse", "Start typing a course code"},
        {"btnSearchGrades", "Search grades with the criteria above"},
        {"btnClearGradeSearch", "Clear the search and show all grades"},
        {"cmbCourse", "The course you are working with"},
        {"cmbAssignmentType", "The kind of assessment, for example Quiz or Midterm Exam"},
        {"cmbStudent", "A student enrolled in the selected course"},
        {"txtGrade", "A grade from 0 to 100"},
        {"btnSaveGrade", "Save the grade (updates it if one already exists)"},
        {"dgvCourseOverview", "Submissions and average grade for each of your courses"},
        {"btnCalculateFinal", "Calculate each student's weighted final grade for the selected course"},
        {"dgvFinalGrades", "Calculated final grades. Edit the Final Grade column to override one."},
        {"txtCourseSearch", "Start typing a course code"},
        {"btnCourseSearch", "Show only matching courses"},
        {"btnCourseSearchClear", "Show all your courses again"},
        {"dgvMyGrades", "All of your grades"},
        {"dgvCourseAverages", "Your average grade in each course"},
        {"txtStudentSearch", "Type a student ID or name"},
        {"btnSearchTranscript", "Show the transcript for this student"},
        {"dgvTranscript", "The student's grades with weights and instructors"},
        {"cmbCourseFilter", "Limit the report to one course"},
        {"cmbInstructorFilter", "Limit the report to one instructor's courses"},
        {"chkUseDates", "Only include records within the date range"},
        {"dtpFrom", "Start of the date range"},
        {"dtpTo", "End of the date range"},
        {"btnApplyFilters", "Apply the filters"},
        {"btnResetFilters", "Clear the filters"},
        {"dgvAnalytics", "Course performance statistics"},
        {"btnExportCsv", "Save the table shown as a CSV file"},
        {"btnExportPdf", "Save the table shown as a PDF file"},
        {"cmbUser", "Show changes made by one user"},
        {"cmbTable", "Show changes to one table"},
        {"cmbAction", "Show only inserts, updates or deletes"},
        {"dgvAudit", "Audit trail. Select a row to see the before and after values."},
        {"txtDetails", "Before and after values of the selected audit entry"},
        {"txtBackupDescription", "A short note describing this backup"},
        {"btnCreateBackup", "Record a new database backup"},
        {"btnRefreshHistory", "Reload the backup history"},
        {"dgvBackupHistory", "Previous backups and their status"}
    }

    ' Adds a tooltip and a status bar hint to every known control on the form
    Public Sub ApplyHints(form As Form)
        Dim tip As New ToolTip()
        tip.InitialDelay = 500
        tip.AutoPopDelay = 8000

        Dim applied As Integer = 0
        AttachHints(form, tip, applied)

        If applied = 0 Then
            tip.Dispose()
            Return
        End If

        Tips(form) = tip
        AddHandler form.Disposed,
            Sub(s, e)
                Dim existing As ToolTip = Nothing
                If Tips.TryGetValue(form, existing) Then
                    existing.Dispose()
                    Tips.Remove(form)
                End If
            End Sub
    End Sub

    Private Sub AttachHints(parent As Control, tip As ToolTip, ByRef applied As Integer)
        For Each child As Control In parent.Controls
            Dim text As String = Nothing
            If child.Name <> "" AndAlso Hints.TryGetValue(child.Name, text) Then
                tip.SetToolTip(child, text)

                Dim hint As String = text
                AddHandler child.MouseEnter, Sub(s, e) MainMDIParent.ShowHint(hint)
                AddHandler child.MouseLeave, Sub(s, e) MainMDIParent.ShowHint("")

                applied += 1
            End If

            If child.HasChildren Then AttachHints(child, tip, applied)
        Next
    End Sub

End Module