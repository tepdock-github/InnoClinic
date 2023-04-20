using OfficesService.Domain.Models;

namespace OfficesService.Domain.Interfaces
{
    public interface IOfficeRepository
    {
        public Task<IEnumerable<Office>> GetOfficesAsync(bool trackChanges);
        public Task<IEnumerable<Office>> GetOfficesActiveAsync(bool trackChanges);
        public Task<Office?> GetOfficeAsync(string id, bool trackChanges);
        public void CreateOfficeAsync(Office office);
    }
}
