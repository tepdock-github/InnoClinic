using ServicesService.Domain.Entities;

namespace ServicesService.Domain.Interfaces
{
    public interface IServiceRepository
    {
        void CreateService(Service service);
        Task<Service> GetServiceByIdAsync(int id, bool trackChanges);
        Task<IEnumerable<Service>> GetAllServicesAsync(bool trackChanges);
        Task<IEnumerable<Service>> GetActiveServicesAsync(bool trackChanges);
    }
}
