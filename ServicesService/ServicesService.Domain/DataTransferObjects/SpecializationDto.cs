namespace ServicesService.Domain.DataTransferObjects
{
    public class SpecializationDto
    {
        public int Id { get; set; }
        public required string SpecializationName { get; set; }
        public required bool IsActive { get; set; }
    }
}
