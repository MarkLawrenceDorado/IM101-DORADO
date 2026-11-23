<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DashboardAnalyticsForm
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Friend WithEvents tlpAnalytics As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents flpAnalyticsTop As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnRefreshAnalytics As System.Windows.Forms.Button
    Friend WithEvents lblAnalyticsUpdated As System.Windows.Forms.Label
    Friend WithEvents chartCoursePerformance As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents chartGradeDistribution As System.Windows.Forms.DataVisualization.Charting.Chart

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

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.tlpAnalytics = New System.Windows.Forms.TableLayoutPanel()
        Me.flpAnalyticsTop = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnRefreshAnalytics = New System.Windows.Forms.Button()
        Me.lblAnalyticsUpdated = New System.Windows.Forms.Label()
        Me.chartCoursePerformance = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.chartGradeDistribution = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.SuspendLayout()
        '
        'DashboardAnalyticsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1000, 700)
        Me.Name = "DashboardAnalyticsForm"
        Me.Text = "Analytics"
        Me.ResumeLayout(False)
    End Sub
End Class