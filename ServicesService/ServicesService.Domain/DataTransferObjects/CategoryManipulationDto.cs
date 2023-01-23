namespace ServicesService.Domain.DataTransferObjects
{
    public class CategoryManipulationDto
    {
        public required string CategoryName { get; set; }
        public string TimeSlotSize { get; set; }
    }
}
