using Authorization.Domain;
using Authorization.Domain.Interfaces;

namespace Authorization.Data
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IAccountRepository _accountRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IAccountRepository AccountRepository { 
            get 
            {
                if (_accountRepository == null) _accountRepository = new AccountRepository(_repositoryContext);
                return _accountRepository; 
            } 
        }
        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
