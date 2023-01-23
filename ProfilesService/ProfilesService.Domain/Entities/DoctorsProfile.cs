namespace ProfilesService.Domain.Entities
{
    public class DoctorsProfile
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string MiddleName { get; set; }
        public required string DateOfBirth { get; set; }
        public int AccountId { get; set; }
        public int SpecializationId { get; set; }
        public int OfficeId { get; set; }
        public required string CareerStartYear { get; set; }
        public required string Status { get; set; }
    }
}
