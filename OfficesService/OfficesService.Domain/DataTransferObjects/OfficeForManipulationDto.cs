using OfficesService.Domain.Models;

namespace OfficesService.Domain.DataTransferObjects
{
    public class OfficeForManipulationDto
    {
        public required string Address { get; set; }
        public string PhoneNumber { get; set; }
        public required bool IsActive { get; set; }
        public List<PhotoForManipulationDto> PhotosList { get; set; }
    }
}
