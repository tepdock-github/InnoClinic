using ServicesService.Domain.DataTransferObjects;

namespace ServicesService.ServicesInterfaces
{
    public interface IServiceServices
    {
        Task EditService(int id, ServicesManipulationDto serviceDto);
        Task<ServiceDto?> CreateService(ServicesManipulationDto serviceDto);
        Task<ServiceDto> GetServiceById(int id);
        Task<IEnumerable<ServiceDto>> GetServices();
        Task<IEnumerable<ServiceDto>> GetActiveServices();
    }
}
