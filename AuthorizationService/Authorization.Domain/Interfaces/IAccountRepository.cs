using Authorization.Domain.Models;

namespace Authorization.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetAsync(string id, bool trackChanges);
    }
}
