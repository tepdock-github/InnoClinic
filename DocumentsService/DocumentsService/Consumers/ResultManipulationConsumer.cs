using DocumentsService.PdfGenerator;
using DocumentsService.Services;
using MassTransit;
using SharedModelsInnoClinic;

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

            var colums = new List<string> 
            {
                "Complaints",
                "Conclusion"
            };

            var rows = new List<List<string>>
            {
                new List<string> { message.Complaints, message.Conclusion }
            };

            var blobName = "Appoitment" + message.AppoitmentId.ToString() + ".pdf";
            var stream = Pdf.Create()
                .AddHeaderTextBox("Conclusion: " + message.Id, true)
                .AddSpace()
                .AddTable(colums, rows)
                .AddSpace()
                .AddTextBox("Recomendations: " + message.Recomendations)
                .AddSpace()
                .AddTextBox("Diagnosis: " + message.Diagnosis)
                .AddFooters()
                .ToMemoryStream();
            
            await _blobService.UploadFileAsync(new FormFile(stream, 0, stream.Length, blobName, blobName));
        }
    }
}
