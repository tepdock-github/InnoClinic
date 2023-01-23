namespace ProfilesService.Domain.DataTransferObjects
{
    public class PatientProfileManipulationDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string MiddleName { get; set; }
        public required bool IsLinkedToAccount { get; set; }
        public required string DateOfBirth { get; set; }
        public int AccountId { get; set; }
    }
}
