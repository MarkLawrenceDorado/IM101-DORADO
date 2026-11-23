<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StudentDashboard
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        dgvMyGrades = New DataGridView()
        dgvCourseAverages = New DataGridView()
        lblWelcome = New Label()
        lblMyGrades = New Label()
        lblCourseAverage = New Label()
        CType(dgvMyGrades, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvCourseAverages, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblWelcome
        ' 
        lblWelcome.AutoSize = True
        lblWelcome.Font = New Font("Segoe UI", 13.0F, FontStyle.Bold)
        lblWelcome.Location = New Point(28, 20)
        lblWelcome.Name = "lblWelcome"
        lblWelcome.Size = New Size(190, 23)
        lblWelcome.TabIndex = 2
        lblWelcome.Text = "Student Dashboard"
        ' 
        ' lblMyGrades
        ' 
        lblMyGrades.AutoSize = True
        lblMyGrades.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)
        lblMyGrades.Location = New Point(28, 68)
        lblMyGrades.Name = "lblMyGrades"
        lblMyGrades.Size = New Size(94, 19)
        lblMyGrades.TabIndex = 3
        lblMyGrades.Text = "My Grades"
        ' 
        ' dgvMyGrades
        ' 
        dgvMyGrades.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        dgvMyGrades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvMyGrades.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvMyGrades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvMyGrades.Location = New Point(28, 96)
        dgvMyGrades.Name = "dgvMyGrades"
        dgvMyGrades.ReadOnly = True
        dgvMyGrades.RowHeadersWidth = 45
        dgvMyGrades.Size = New Size(660, 596)
        dgvMyGrades.TabIndex = 0
        ' 
        ' lblCourseAverage
        ' 
        lblCourseAverage.AutoSize = True
        lblCourseAverage.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)
        lblCourseAverage.Location = New Point(716, 68)
        lblCourseAverage.Name = "lblCourseAverage"
        lblCourseAverage.Size = New Size(135, 19)
        lblCourseAverage.TabIndex = 4
        lblCourseAverage.Text = "Course Average"
        ' 
        ' dgvCourseAverages
        ' 
        dgvCourseAverages.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvCourseAverages.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvCourseAverages.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvCourseAverages.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCourseAverages.Location = New Point(716, 96)
        dgvCourseAverages.Name = "dgvCourseAverages"
        dgvCourseAverages.ReadOnly = True
        dgvCourseAverages.RowHeadersWidth = 45
        dgvCourseAverages.Size = New Size(630, 596)
        dgvCourseAverages.TabIndex = 1
        ' 
        ' StudentDashboard
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1374, 720)
        Controls.Add(lblCourseAverage)
        Controls.Add(lblMyGrades)
        Controls.Add(lblWelcome)
        Controls.Add(dgvCourseAverages)
        Controls.Add(dgvMyGrades)
        FormBorderStyle = FormBorderStyle.Sizable
        MinimumSize = New Size(900, 500)
        Name = "StudentDashboard"
        Text = "Student Dashboard"
        WindowState = FormWindowState.Maximized
        CType(dgvMyGrades, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvCourseAverages, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents dgvMyGrades As DataGridView
    Friend WithEvents dgvCourseAverages As DataGridView
    Friend WithEvents lblWelcome As Label
    Friend WithEvents lblMyGrades As Label
    Friend WithEvents lblCourseAverage As Label
End Class