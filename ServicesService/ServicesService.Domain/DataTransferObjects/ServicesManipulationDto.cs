namespace ServicesService.Domain.DataTransferObjects
{
    public class ServicesManipulationDto
    {
        public required string ServiceName { get; set; }
        public float Price { get; set; }
        public required bool IsActive { get; set; }

        public int CategoryId { get; set; }
        public int SpecializationId { get; set; }
    }
}
