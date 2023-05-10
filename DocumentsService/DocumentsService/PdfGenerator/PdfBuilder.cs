using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.Tables;
using MigraDocCore.Rendering;

namespace DocumentsService.PdfGenerator
{
    public class PdfBuilder
    {
        private readonly Document _source;
        private readonly Section _section;
        private IPdfConfiguration _configuration;

        public PdfBuilder(IPdfConfiguration configuration)
        {
            _source = new Document();
            _section = _source.AddSection();

            _section.PageSetup = _source.DefaultPageSetup.Clone();
            _section.PageSetup.PageFormat = PageFormat.A4;

            _configuration = configuration;
        }

        public PdfBuilder AddHeaderTextBox(string text, bool withBorders = false)
        {
            var table = _section.AddTable();
            var column = table.AddColumn();
            column.Width = 495;
            column.Format.Alignment = ParagraphAlignment.Center;

            if (withBorders)
            {
                column.Borders.Width = 0.5;
            }

            var row = table.AddRow();
            row.Cells[0].AddParagraph(text);
            row.TopPadding = _configuration.HeaderSpacing;
            row.BottomPadding = _configuration.HeaderSpacing;
            row.Format.Font.Bold = true;
            row.Format.Font.Size = _configuration.HeaderFontSize;

            return this;
        }

        public PdfBuilder AddTextBox(string text)
        {
            var table = _section.AddTable();
            var column = table.AddColumn();
            column.Width = 495;
            column.Format.Alignment = ParagraphAlignment.Center;

            var row = table.AddRow();
            row.Cells[0].AddParagraph(text);
            row.TopPadding = _configuration.TextBoxSpacing;
            row.BottomPadding = _configuration.TextBoxSpacing;

            return this;
        }

        public PdfBuilder AddSpace()
        {
            AddHeaderTextBox(string.Empty);
            return this;
        }

        public PdfBuilder AddTable(List<string> columns, List<List<string>> rows)
        {
            var table = _section.AddTable();
            table.Style = "Table";
            table.Borders.Width = _configuration.TableBorderWidth / 2;
            table.Borders.Left.Width = _configuration.TableBorderWidth;
            table.Borders.Right.Width = _configuration.TableBorderWidth;

            for (var i = 0; i < columns.Count; i++)
            {
                var column = table.AddColumn();
                column.Format.Alignment = ParagraphAlignment.Center;
            }

            var headerRow = table.AddRow();
            headerRow.HeadingFormat = true;
            headerRow.Format.Font.Bold = true;

            for (var i = 0; i < columns.Count; i++)
            {
                headerRow.Cells[i].AddParagraph(columns[i]);
                headerRow.VerticalAlignment = VerticalAlignment.Center;
            }

            for (var i = 0; i < rows.Count; i++)
            {
                var row = table.AddRow();
                row.VerticalAlignment = VerticalAlignment.Center;

                for (var j = 0; j < rows[i].Count; j++)
                {
                    row.Cells[j].AddParagraph(rows[i][j]);
                }
            }

            return this;
        }

        public PdfBuilder AddFooters()
        {
            var footerTable = _section.Footers.Primary.AddTable();
            footerTable.Style = "Table";
            footerTable.Borders.Top.Width = _configuration.FooterBorderWidth;

            var _sectionWidth = _section.PageSetup.PageWidth - _section.PageSetup.LeftMargin - (_section.PageSetup.RightMargin / 2);
            var columnWidth = _sectionWidth / 3;

            var companyNameColumn = footerTable.AddColumn();
            var createDateColumn = footerTable.AddColumn();
            var pageNumberColumn = footerTable.AddColumn();

            companyNameColumn.Format.Alignment = ParagraphAlignment.Left;
            createDateColumn.Format.Alignment = ParagraphAlignment.Center;
            pageNumberColumn.Format.Alignment = ParagraphAlignment.Right;

            companyNameColumn.Width = columnWidth;
            createDateColumn.Width = columnWidth;
            pageNumberColumn.Width = columnWidth;

            companyNameColumn.LeftPadding = 5;
            pageNumberColumn.RightPadding = 5;

            var row = footerTable.AddRow();
            row.TopPadding = 10;

            var additionalInfo = "Info";
            row.Cells[0].AddParagraph(additionalInfo);
            row.Cells[1].AddParagraph(DateTime.Now.ToString());

            var pageNumberParagraph = row.Cells[2].AddParagraph();
            pageNumberParagraph.AddText("Page ");
            pageNumberParagraph.AddPageField();
            pageNumberParagraph.AddText(" from ");
            pageNumberParagraph.AddNumPagesField();

            var spacing = (int)Math.Round(additionalInfo.Length / 30.0);
            _section.PageSetup.BottomMargin += 8 * spacing;
            _section.PageSetup.FooterDistance -= 8 * spacing;

            return this;
        }

        public void SaveTo(string path)
        {
            _source.Info.Title = "document";
            var pdfDocumentRenderer = new PdfDocumentRenderer
            {
                Document = _source
            };

            pdfDocumentRenderer.RenderDocument();
            pdfDocumentRenderer.Save(path);
        }

        public MemoryStream ToMemoryStream()
        {
            var memoryStream = new MemoryStream();

            _source.Info.Title = "document";
            var pdfDocumentRenderer = new PdfDocumentRenderer
            {
                Document = _source
            };

            pdfDocumentRenderer.RenderDocument();
            pdfDocumentRenderer.Save(memoryStream, false);

            return memoryStream;
        }

        public byte[] ToByteArray()
        {
            var memoryStream = ToMemoryStream();

            var bytes = memoryStream.ToArray();
            memoryStream.Close();

            return bytes;
        }
    }
}
