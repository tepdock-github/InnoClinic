namespace Appoitments.Domain.DataTransferObjects
{
    public class ResultDto
    {
        public string Id { get; set; }
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recomendations { get; set; }

        public string AppoitmentId { get; set; }
    }
}
