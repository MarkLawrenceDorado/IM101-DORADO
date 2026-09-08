<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StudentDashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
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
        ' dgvMyGrades
        ' 
        dgvMyGrades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvMyGrades.Location = New Point(43, 136)
        dgvMyGrades.Name = "dgvMyGrades"
        dgvMyGrades.RowHeadersWidth = 51
        dgvMyGrades.Size = New Size(542, 542)
        dgvMyGrades.TabIndex = 0
        ' 
        ' dgvCourseAverages
        ' 
        dgvCourseAverages.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCourseAverages.Location = New Point(657, 136)
        dgvCourseAverages.Name = "dgvCourseAverages"
        dgvCourseAverages.RowHeadersWidth = 51
        dgvCourseAverages.Size = New Size(524, 542)
        dgvCourseAverages.TabIndex = 1
        ' 
        ' lblWelcome
        ' 
        lblWelcome.AutoSize = True
        lblWelcome.Location = New Point(43, 19)
        lblWelcome.Name = "lblWelcome"
        lblWelcome.Size = New Size(74, 20)
        lblWelcome.TabIndex = 2
        lblWelcome.Text = "Welcome:"
        ' 
        ' lblMyGrades
        ' 
        lblMyGrades.AutoSize = True
        lblMyGrades.Location = New Point(271, 99)
        lblMyGrades.Name = "lblMyGrades"
        lblMyGrades.Size = New Size(79, 20)
        lblMyGrades.TabIndex = 3
        lblMyGrades.Text = "My Grades"
        ' 
        ' lblCourseAverage
        ' 
        lblCourseAverage.AutoSize = True
        lblCourseAverage.Location = New Point(855, 99)
        lblCourseAverage.Name = "lblCourseAverage"
        lblCourseAverage.Size = New Size(113, 20)
        lblCourseAverage.TabIndex = 4
        lblCourseAverage.Text = "Course Average"
        ' 
        ' StudentDashboard
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1374, 724)
        Controls.Add(lblCourseAverage)
        Controls.Add(lblMyGrades)
        Controls.Add(lblWelcome)
        Controls.Add(dgvCourseAverages)
        Controls.Add(dgvMyGrades)
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
