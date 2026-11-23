Imports System.Globalization
Imports System.Text

Public Module GridEnhancer

    Private ReadOnly GradeColumns() As String = {"grade", "Final Grade", "average_grade", "overall_average"}

    ' Applies the grid features to one DataGridView. Set a grid's Tag to "editable" to keep it editable.
    Public Sub Enhance(dgv As DataGridView)
        Dim editable As Boolean = String.Equals(Convert.ToString(dgv.Tag), "editable", StringComparison.OrdinalIgnoreCase)

        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        If Not editable Then dgv.ReadOnly = True

        RemoveHandler dgv.RowPrePaint, AddressOf OnRowPrePaint
        AddHandler dgv.RowPrePaint, AddressOf OnRowPrePaint

        RemoveHandler dgv.CellDoubleClick, AddressOf OnCellDoubleClick
        If Not editable Then AddHandler dgv.CellDoubleClick, AddressOf OnCellDoubleClick
    End Sub

    ' Applies Enhance to every DataGridView on a form, including those inside tabs and panels
    Public Sub EnhanceAll(parent As Control)
        For Each child As Control In parent.Controls
            Dim grid As DataGridView = TryCast(child, DataGridView)
            If grid IsNot Nothing Then Enhance(grid)
            If child.HasChildren Then EnhanceAll(child)
        Next
    End Sub

    ' Repaints every grid, for example after the row-coloring setting changes
    Public Sub Repaint(parent As Control)
        For Each child As Control In parent.Controls
            Dim grid As DataGridView = TryCast(child, DataGridView)
            If grid IsNot Nothing Then grid.Invalidate()
            If child.HasChildren Then Repaint(child)
        Next
    End Sub

    ' Keeps the scroll position when a grid is reloaded by auto-refresh
    Public Function CaptureScroll(dgv As DataGridView) As Integer
        If dgv.RowCount = 0 Then Return 0
        Return Math.Max(0, dgv.FirstDisplayedScrollingRowIndex)
    End Function

    Public Sub RestoreScroll(dgv As DataGridView, index As Integer)
        If index <= 0 OrElse index >= dgv.RowCount Then Return
        Try
            dgv.FirstDisplayedScrollingRowIndex = index
        Catch
        End Try
    End Sub

    ' ---------- Row coloring ----------
    Private Sub OnRowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs)
        Dim dgv As DataGridView = DirectCast(sender, DataGridView)
        If e.RowIndex < 0 OrElse e.RowIndex >= dgv.Rows.Count Then Return

        Dim row As DataGridViewRow = dgv.Rows(e.RowIndex)
        row.DefaultCellStyle.BackColor = RowColorFor(dgv, row)
    End Sub

    Private Function RowColorFor(dgv As DataGridView, row As DataGridViewRow) As Color
        If Not AppSettings.RowColoringEnabled Then Return Color.Empty

        ' Audit log: color by action
        If dgv.Columns.Contains("action_type") Then
            Select Case Convert.ToString(row.Cells("action_type").Value)
                Case "INSERT"
                    Return Color.FromArgb(232, 245, 233)
                Case "UPDATE"
                    Return Color.FromArgb(255, 248, 225)
                Case "DELETE"
                    Return Color.FromArgb(255, 235, 238)
            End Select
        End If

        ' Notification center: color by priority
        If dgv.Columns.Contains("priority") Then
            Select Case Convert.ToString(row.Cells("priority").Value)
                Case "High"
                    Return Color.FromArgb(255, 235, 238)
                Case "Medium"
                    Return Color.FromArgb(255, 248, 225)
            End Select
        End If

        ' Status columns (backups, final grades)
        If dgv.Columns.Contains("status") Then
            Dim status As String = Convert.ToString(row.Cells("status").Value)
            If status.StartsWith("COMPLETED", StringComparison.OrdinalIgnoreCase) Then Return Color.FromArgb(232, 245, 233)
            If status.StartsWith("FAILED", StringComparison.OrdinalIgnoreCase) Then Return Color.FromArgb(255, 235, 238)
            If status.StartsWith("STARTED", StringComparison.OrdinalIgnoreCase) Then Return Color.FromArgb(255, 248, 225)
            If status.StartsWith("Overridden", StringComparison.OrdinalIgnoreCase) Then Return Color.FromArgb(227, 242, 253)
            If status.StartsWith("Partial", StringComparison.OrdinalIgnoreCase) Then Return Color.FromArgb(255, 248, 225)
        End If

        ' Grades: below 60 pink, 90 and above green
        For Each columnName As String In GradeColumns
            If Not dgv.Columns.Contains(columnName) Then Continue For

            Dim value As Object = row.Cells(columnName).Value
            Dim grade As Decimal
            If value IsNot Nothing AndAlso Not IsDBNull(value) AndAlso
               Decimal.TryParse(Convert.ToString(value, CultureInfo.CurrentCulture), grade) Then
                If grade < 60 Then Return Color.LightPink
                If grade >= 90 Then Return Color.LightGreen
            End If
            Exit For
        Next

        Return Color.Empty
    End Function

    ' ---------- Double-click: show the whole record ----------
    Private Sub OnCellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return

        Dim dgv As DataGridView = DirectCast(sender, DataGridView)
        Dim sb As New StringBuilder()

        For Each column As DataGridViewColumn In dgv.Columns
            If Not column.Visible Then Continue For
            sb.AppendLine(column.HeaderText & ": " & Convert.ToString(dgv.Rows(e.RowIndex).Cells(column.Index).Value))
        Next

        Using dialog As New Form()
            dialog.Text = "Record Details"
            dialog.StartPosition = FormStartPosition.CenterParent
            dialog.Size = New Size(480, 360)
            dialog.MinimizeBox = False
            dialog.MaximizeBox = False

            Dim box As New TextBox()
            box.Multiline = True
            box.ReadOnly = True
            box.Dock = DockStyle.Fill
            box.ScrollBars = ScrollBars.Vertical
            box.Font = New Font("Segoe UI", 10)
            box.Text = sb.ToString()
            dialog.Controls.Add(box)
            box.Select(0, 0)

            dialog.ShowDialog(dgv.FindForm())
        End Using
    End Sub

End Module