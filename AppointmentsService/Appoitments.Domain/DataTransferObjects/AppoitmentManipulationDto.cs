namespace Appoitments.Domain.DataTransferObjects
{
    public class AppoitmentManipulationDto
    {
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string ServiceId { get; set; }
        public DateOnly Date { get; set; }
        public string Time { get; set; }
        public bool isApproved { get; set; }
        public bool isComplete { get; set; }
    }
}
