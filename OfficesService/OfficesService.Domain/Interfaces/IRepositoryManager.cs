namespace OfficesService.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        IOfficeRepository OfficeRepository { get; }

        Task SaveAsync();
    }
}
