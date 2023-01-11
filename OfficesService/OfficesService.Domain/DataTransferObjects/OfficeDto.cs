using OfficesService.Domain.Models;

namespace OfficesService.Domain.DataTransferObjects
{
    public class OfficeDto
    {
        public string Id { get; set; }
        public required string Address { get; set; }
        public string PhoneNumber { get; set; }
        public required bool IsActive { get; set; }
        public List<Photo> PhotosList { get; set; }
    }
}
