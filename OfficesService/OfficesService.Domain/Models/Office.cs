namespace OfficesService.Domain.Models
{
    public class Office
    {
        public required string Id { get; set; }
        public required string Address { get; set; }
        public string PhoneNumber { get; set; }
        public required bool IsActive { get; set; }

    }
}
