using Authorization.Domain;
using Authorization.Domain.Interfaces;

namespace Authorization.Data
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
