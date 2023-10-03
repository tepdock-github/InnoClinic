namespace ProfilesService.Domain.DataTransferObjects
{
    public class ReceptionistProfileDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string MiddleName { get; set; }
        public string OfficeId { get; set; }
        public string AccountId { get; set; }
    }
}
