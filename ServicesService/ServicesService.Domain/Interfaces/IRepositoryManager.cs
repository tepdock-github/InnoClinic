namespace ServicesService.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        IServiceRepository ServiceRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ISpecializationRepository SpecializationRepository { get; }
        Task SaveAsync();
    }
}
