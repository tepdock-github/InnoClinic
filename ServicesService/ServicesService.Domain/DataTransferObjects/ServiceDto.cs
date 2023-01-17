using ServicesService.Domain.Entities;

namespace ServicesService.Domain.DataTransferObjects
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public required string ServiceName { get; set; }
        public float Price { get; set; }
        public required bool IsActive { get; set; }

        public int CategoryId { get; set; }
        public Category ServiceCategory { get; set; }

        public int SpecializationId { get; set; }
    }
}
