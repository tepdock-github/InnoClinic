using Microsoft.EntityFrameworkCore;
using OfficesService.Domain;
using OfficesService.Domain.Interfaces;
using OfficesService.Domain.Models;

namespace OfficesService.Data.Repositories
{
    public class OfficeRepository : RepositoryBase<Office>, IOfficeRepository
    {
        public OfficeRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        public void CreateOfficeAsync(Office office) => Create(office);

        public async Task<Office?> GetOfficeAsync(string id, bool trackChanges) =>
            await FindByCondition(o => o.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Office>> GetOfficesActiveAsync(bool trackChanges) =>
            await FindByCondition(o => o.IsActive == true, trackChanges)
            .ToListAsync();

        public async Task<IEnumerable<Office>> GetOfficesAsync(bool trackChanges) =>
               await FindAll(trackChanges).ToListAsync();
    }
}
