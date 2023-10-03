using Authorization.Domain.Models;

namespace AuthorizationService.Services
{
    public interface IAccountService
    {
        Task<Account> GetDto(string id);
    }
}
