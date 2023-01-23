namespace ProfilesService.Domain.Entities
{
    public class ReceptionistProfile
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string MiddleName { get; set; }
        public int OfficeId { get; set; }
        public int AccountId { get; set; }
    }
}
