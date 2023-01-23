using OfficesService.Domain.Models;

namespace OfficesService.Domain.Interfaces
{
    public interface IOfficeRepository
    {
        public Task<List<Office>> GetOfficesAsync();
        public Task<Office> GetOfficeAsync(string id);
        public Task<Office> CreateOfficeAsync(Office office);
        public Task UpdateOfficeAsync(string id, Office office);
    }
}
