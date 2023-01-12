namespace Appoitments.Domain.Entities
{
    public class Appoitment
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string ServiceId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public bool isApproved { get; set; }
        public bool isComplete { get; set; }

        public string? ResultId { get; set; }
        public Result Result { get; set; }
    }
}
