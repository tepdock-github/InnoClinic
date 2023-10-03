namespace ProfilesService.Domain.DataTransferObjects
{
    public class PatientProfileDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? MiddleName { get; set; }
        public required bool IsLinkedToAccount { get; set; }
        public required string DateOfBirth { get; set; }
        public string AccountId { get; set; }
    }
}
