using Appoitments.Domain.Entities;

namespace Appoitments.Domain.DataTransferObjects
{
    public class AppoitmentDto
    {
        public required int Id { get; set; }
        public required string PatientFirstName { get; set; }
        public required string PatientLastName { get; set; }
        public required string DoctorFirstName { get; set; }
        public required string DoctorLastName { get; set; }
        public required string ServiceName { get; set; }
        public required string Date { get; set; }
        public required string Time { get; set; }
        public required bool isApproved { get; set; }
        public required bool isComplete { get; set; }
        public Result? Result { get; set; }
    }
}
