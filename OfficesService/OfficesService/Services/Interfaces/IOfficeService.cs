using OfficesService.Domain.DataTransferObjects;

namespace OfficesService.Services.Interfaces
{
    public interface IOfficeService
    {
        Task<IEnumerable<OfficeDto>> GetOffices();
        Task<OfficeDto> GetOfficeById(string id);
        Task<OfficeDto> AddOffice(OfficeForManipulationDto officeDto);
        Task UpdateOffice(string id, OfficeForManipulationDto officeDto);
    }
}