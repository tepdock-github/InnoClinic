namespace ProfilesService.Domain.DataTransferObjects
{
    public class ReceptionistProfileManipulationDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string MiddleName { get; set; }
        public string OfficeId { get; set; }
        public string AccountId { get; set; }
    }
}
