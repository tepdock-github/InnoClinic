namespace ServicesService.Domain.Entities
{
    public class Specialization
    {
        public int Id { get; set; }
        public required string SpecializationName { get; set; }
        public required bool IsActive { get; set; }

        public ICollection<Service> Services { get;set; }
    }
}
