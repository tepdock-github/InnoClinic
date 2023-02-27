namespace Appoitments.Domain.DataTransferObjects
{
    public class ResultManipulationDto
    {
        public required string Complaints { get; set; }
        public required string Conclusion { get; set; }
        public required string Recomendations { get; set; }
        public string? Diagnosis { get; set; }

        public required int AppoitmentId { get; set; }
    }
}
