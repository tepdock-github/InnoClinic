namespace Appoitments.Domain.Entities
{
    public class Result
    {
        public string Id { get; set; }
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recomendations { get; set; }

        public string AppoitmentId { get; set; }
        public Appoitment Appoitment { get; set; }
    }
}
