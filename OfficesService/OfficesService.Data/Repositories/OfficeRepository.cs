using MongoDB.Driver;
using OfficesService.Domain.Interfaces;
using OfficesService.Domain.Models;

namespace OfficesService.Data.Repositories
{
    public class OfficeRepository
    {
        private readonly IMongoCollection<Office> _offices;

        public OfficeRepository(IOfficeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _offices = database.GetCollection<Office>(settings.OfficeCollectionName);
        }

        public async Task<List<Office>> GetOfficesAsync() =>
            await _offices.Find(p => true).ToListAsync();

        public async Task<Office> GetOfficeAsync(string id) =>
            await _offices.Find<Office>(p => p.Id.Equals(id)).FirstOrDefaultAsync();

        public async Task<Office> CreateOfficeAsync(Office office) 
        { 
            await _offices.InsertOneAsync(office);
            return office;
        }

        public async Task UpdateOfficeAsync(string id, Office office) => await _offices.ReplaceOneAsync(p => p.Id.Equals(id), office);

        //public async Task DeletePhotoAsync(string id) => await _offices.DeleteOneAsync(p => p.Id.Equals(id));
    }
}
