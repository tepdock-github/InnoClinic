namespace ServicesService.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string CategoryName { get; set; }
        public string? TimeSlotSize { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
