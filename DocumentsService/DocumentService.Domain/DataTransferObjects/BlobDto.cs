namespace DocumentService.Domain.DataTransferObjects
{
    public class BlobDto
    {
        public int Id { get; set; }
        public string? Uri { get; set; }
        public string? Name { get; set; }
        public string? ContentType { get; set; }
        public Stream? Content { get; set; }
    }
}
