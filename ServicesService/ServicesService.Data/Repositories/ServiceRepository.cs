using Microsoft.EntityFrameworkCore;
using ServicesService.Domain;
using ServicesService.Domain.Entities;
using ServicesService.Domain.Interfaces;

namespace ServicesService.Data.Repositories
{
    public class ServiceRepository : RepositoryBase<Service>, IServiceRepository
    {
        public ServiceRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateService(Service service) => Create(service);

        public async Task<IEnumerable<Service>> GetActiveServicesAsync(bool trackChanges) =>
            await FindByCondition(s => s.IsActive == true, trackChanges).ToListAsync();

        public async Task<IEnumerable<Service>> GetAllServicesAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<Service> GetServiceByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(s => s.Id.Equals(id), trackChanges)
            .FirstOrDefaultAsync();
    }
}
