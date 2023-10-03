namespace Appoitments.Domain.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public required string DoctorId { get; set; }
        public required string DoctorFirstName { get; set; }
        public required string DoctorLastName { get; set; }
        public required string Date { get; set; }
        public required string Time { get; set; }
        public required bool isBooked { get; set; }
        public int AppoitmentId { get; set; }
    }
}
