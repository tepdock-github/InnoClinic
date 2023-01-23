namespace ServicesService.Domain.DataTransferObjects
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public required string CategoryName { get; set; }
        public string TimeSlotSize { get; set; }
    }
}
