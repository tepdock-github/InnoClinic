using Authorization.Domain;
using Authorization.Domain.Interfaces;
using Authorization.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Data
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<Account?> GetAsync(string id, bool trackChanges) =>
            await FindByCondition(a => a.Id.Equals(id), trackChanges).FirstOrDefaultAsync();
    }
}
