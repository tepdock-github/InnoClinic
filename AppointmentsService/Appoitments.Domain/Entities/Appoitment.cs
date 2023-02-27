namespace Appoitments.Domain.Entities
{
    public class Appoitment
    {
        public required int Id { get; set; }
        public required int PatientId { get; set; }
        public required string PatientFirstName { get; set; }
        public required string PatientLastName { get; set; }
        public required int DoctorId { get; set; }
        public required string DoctorFirstName { get; set; }
        public required string DoctorLastName { get; set; }
        public required int ServiceId { get; set; }
        public required string ServiceName { get; set; }
        public required string Date { get; set; }
        public required string Time { get; set; }
        public required bool isApproved { get; set; }
        public required bool isComplete { get; set; }

        public int? ResultId { get; set; }
        public Result? Result { get; set; }
    }
}
