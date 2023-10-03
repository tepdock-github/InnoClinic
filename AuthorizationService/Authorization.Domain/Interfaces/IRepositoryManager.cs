namespace Authorization.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        IAccountRepository AccountRepository { get; }
        Task SaveAsync();
    }
}
