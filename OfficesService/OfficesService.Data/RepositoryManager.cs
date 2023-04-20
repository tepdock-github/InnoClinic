using OfficesService.Data.Repositories;
using OfficesService.Domain;
using OfficesService.Domain.Interfaces;

namespace OfficesService.Data
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IOfficeRepository _officeRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IOfficeRepository OfficeRepository 
        {
            get 
            {
                if(_officeRepository == null )
                {
                    _officeRepository = new OfficeRepository( _repositoryContext );
                }
                return _officeRepository; 
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
