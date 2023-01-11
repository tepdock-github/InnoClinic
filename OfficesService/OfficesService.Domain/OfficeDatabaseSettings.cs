using OfficesService.Domain.Interfaces;

namespace OfficesService.Domain
{
    public class OfficeDatabaseSettings : IOfficeDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string OfficeCollectionName { get; set; }
        public string PhotoCollectionName { get; set; }
    }
}
