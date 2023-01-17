using ServicesService.Domain.DataTransferObjects;
using ServicesService.Domain.Entities;

namespace ServicesService.ServicesInterfaces
{
    public interface IServiceServices
    {
        Task<bool> EditService(int id, ServicesManipulationDto serviceDto);
        Task<ServiceDto?> CreateService(Service service);
        Task<Service> GetServiceById(int id);
        Task<IEnumerable<Service>> GetServices();
    }
}
