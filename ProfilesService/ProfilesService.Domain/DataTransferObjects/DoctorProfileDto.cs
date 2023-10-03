namespace ProfilesService.Domain.DataTransferObjects
{
    public class DoctorProfileDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? MiddleName { get; set; }
        public required string DateOfBirth { get; set; }
        public string AccountId { get; set; }
        public required string SpecializationName { get; set; }
        public string OfficeId { get; set; }
        public required string CareerStartYear { get; set; }
        public required string Status { get; set; }
    }
}
