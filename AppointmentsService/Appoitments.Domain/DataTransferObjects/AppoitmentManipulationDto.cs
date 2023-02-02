namespace Appoitments.Domain.DataTransferObjects
{
    public class AppoitmentManipulationDto
    {
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
    }
}
