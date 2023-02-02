namespace Appoitments.Domain.Entities
{
    public class Result
    {
        public required int Id { get; set; }
        public required string Complaints { get; set; }
        public required string Conclusion { get; set; }
        public required string Recomendations { get; set; }
        public string? Diagnosis { get; set; }

        public required int AppoitmentId { get; set; }
        public Appoitment? Appoitment { get; set; }
    }
}
