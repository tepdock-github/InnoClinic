using DocumentsService.Services;
using MassTransit;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using SharedModelsInnoClinic;
using System.Text;

namespace DocumentsService.Consumers
{
    public class ResultManipulationConsumer : IConsumer<IResultManipulation>
    {
        private readonly IBlobService _blobService;

        public ResultManipulationConsumer(IBlobService blobService)
        {
            _blobService = blobService;
        }

        public async Task Consume(ConsumeContext<IResultManipulation> context)
        {
            var message = context.Message;

            var document = new PdfDocument();
            var page = document.AddPage();

            var gfx = XGraphics.FromPdfPage(page);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
            var font = new XFont("Times New Roman", 12, XFontStyle.Regular, options);
            var rect = new XRect(0, 0, page.Width, page.Height);

            var tf = new XTextFormatter(gfx);
            tf.DrawString("Appoitment ID: " + message.AppoitmentId.ToString(), font, XBrushes.Black, rect, XStringFormats.TopLeft);
            var rect1 = new XRect(0, 20, page.Width, page.Height);
            tf.DrawString("Complaints: " + message.Complaints, font, XBrushes.Black, rect1, XStringFormats.TopLeft);
            var rect2 = new XRect(0, 40, page.Width, page.Height);
            tf.DrawString("Diagnosis: " + message.Diagnosis, font, XBrushes.Black, rect2, XStringFormats.TopLeft);
            var rect3 = new XRect(0, 60, page.Width, page.Height);
            tf.DrawString("Conclusion: " + message.Conclusion, font, XBrushes.Black, rect3, XStringFormats.TopLeft);
            var rect4 = new XRect(0, 80, page.Width, page.Height);
            tf.DrawString("Recomendations: " + message.Recomendations, font, XBrushes.Black, rect4, XStringFormats.TopLeft);

            var stream = new MemoryStream();
            document.Save(stream, false);

            document.Close();

            var blobName = "Appoitment" + message.AppoitmentId.ToString() + ".pdf";
            await _blobService.UploadFileAsync(new FormFile(stream, 0, stream.Length, blobName, blobName));
        }
    }
}
