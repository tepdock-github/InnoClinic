namespace DocumentService.Domain.DataTransferObjects
{
    public class BlobManipulationDto
    {
        public string? Status { get; set; }
        public bool Error { get; set; }
        public BlobDto Blob { get; set; }

        public BlobManipulationDto()
        {
            Blob = new BlobDto();
        }
    }
}
