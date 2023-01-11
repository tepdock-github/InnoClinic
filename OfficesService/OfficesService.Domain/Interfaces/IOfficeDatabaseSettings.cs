namespace OfficesService.Domain.Interfaces
{
    public interface IOfficeDatabaseSettings
    {
        public string DatabaseName { get; }
        public string ConnectionString { get; }
        public string OfficeCollectionName { get; }
        public string PhotoCollectionName { get; }
    }
}
