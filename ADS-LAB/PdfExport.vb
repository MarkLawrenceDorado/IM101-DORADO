Imports QuestPDF.Fluent
Imports QuestPDF.Helpers
Imports QuestPDF.Infrastructure

Public Module PdfExport

    Public Sub ExportGrid(grid As DataGridView, title As String, filePath As String, subtitle As String)
        If grid Is Nothing OrElse String.IsNullOrWhiteSpace(filePath) Then Return

        Dim columns As List(Of DataGridViewColumn) =
            grid.Columns.Cast(Of DataGridViewColumn)().
            Where(Function(c) c.Visible).
            OrderBy(Function(c) c.DisplayIndex).
            ToList()

        Dim headers As New List(Of String)()
        For Each col As DataGridViewColumn In columns
            headers.Add(If(col.HeaderText, ""))
        Next

        Dim rows As New List(Of Object())()
        For Each row As DataGridViewRow In grid.Rows
            If row.IsNewRow Then Continue For

            Dim values(columns.Count - 1) As Object
            For i As Integer = 0 To columns.Count - 1
                values(i) = row.Cells(columns(i).Index).FormattedValue
            Next
            rows.Add(values)
        Next

        If headers.Count = 0 OrElse rows.Count = 0 Then Return

        ExportRows(headers, rows, title, filePath, If(subtitle, ""), True)
    End Sub

    Public Sub ExportRows(headers As IList(Of String),
                          rows As IList(Of Object()),
                          title As String,
                          filePath As String,
                          Optional footerText As String = "",
                          Optional landscape As Boolean = True)
        If headers Is Nothing OrElse headers.Count = 0 Then Return
        If String.IsNullOrWhiteSpace(filePath) Then Return

        ' Required once before generating documents
        QuestPDF.Settings.License = LicenseType.Community

        Dim safeTitle As String = If(String.IsNullOrWhiteSpace(title), "Report", title)
        Dim safeFooter As String = If(footerText, "")

        Dim weights(headers.Count - 1) As Single
        For i As Integer = 0 To headers.Count - 1
            Dim longest As Integer = If(headers(i), "").Length
            Dim sampled As Integer = 0

            If rows IsNot Nothing Then
                For Each r In rows
                    sampled += 1
                    If sampled > 200 Then Exit For
                    If r IsNot Nothing AndAlso i < r.Length AndAlso r(i) IsNot Nothing AndAlso Not IsDBNull(r(i)) Then
                        Dim len As Integer = Convert.ToString(r(i)).Length
                        If len > longest Then longest = len
                    End If
                Next
            End If

            weights(i) = CSng(Math.Min(Math.Max(longest, 6), 70))
        Next

        Dim pageSize As PageSize = If(landscape, PageSizes.A4.Landscape(), PageSizes.A4.Portrait())

        Dim doc As IDocument = Document.Create(
            Sub(container As IDocumentContainer)
                container.Page(
                    Sub(page As PageDescriptor)
                        page.Size(pageSize)
                        page.Margin(28)
                        page.DefaultTextStyle(TextStyle.Default.FontSize(8).FontFamily(Fonts.Lato))

                        page.Header().PaddingBottom(8).Text(safeTitle).FontSize(14).Bold()

                        page.Content().Table(
                            Sub(table As TableDescriptor)
                                table.ColumnsDefinition(
                                    Sub(cols As TableColumnsDefinitionDescriptor)
                                        For i As Integer = 0 To headers.Count - 1
                                            cols.RelativeColumn(weights(i))
                                        Next
                                    End Sub)

                                table.Header(
                                    Sub(header As TableCellDescriptor)
                                        For Each h In headers
                                            header.Cell().
                                                Background(Colors.Grey.Lighten3).
                                                BorderBottom(1).BorderColor(Colors.Grey.Medium).
                                                Padding(4).
                                                Text(If(h, "")).Bold()
                                        Next
                                    End Sub)

                                If rows IsNot Nothing Then
                                    For rowIndex As Integer = 0 To rows.Count - 1
                                        Dim rowVals() As Object = rows(rowIndex)
                                        Dim bgColor As String = If(rowIndex Mod 2 = 0, Colors.White, Colors.Grey.Lighten5)

                                        For c As Integer = 0 To headers.Count - 1
                                            Dim cellVal As Object = If(rowVals IsNot Nothing AndAlso c < rowVals.Length, rowVals(c), Nothing)
                                            Dim text As String = If(cellVal Is Nothing OrElse IsDBNull(cellVal), "", Convert.ToString(cellVal))

                                            table.Cell().
                                                Background(bgColor).
                                                BorderBottom(0.5F).BorderColor(Colors.Grey.Lighten2).
                                                Padding(4).
                                                Text(text)
                                        Next
                                    Next
                                End If
                            End Sub)

                        page.Footer().Row(
                            Sub(row As RowDescriptor)
                                row.RelativeItem().Text(safeFooter).FontSize(7).FontColor(Colors.Grey.Darken1)
                                row.AutoItem().Text(
                                    Sub(txt As TextDescriptor)
                                        txt.Span("Page ")
                                        txt.CurrentPageNumber()
                                        txt.Span(" of ")
                                        txt.TotalPages()
                                    End Sub)
                            End Sub)
                    End Sub)
            End Sub)

        doc.GeneratePdf(filePath)
    End Sub

End Module