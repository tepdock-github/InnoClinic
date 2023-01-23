using ServicesService.Data.Repositories;
using ServicesService.Domain;
using ServicesService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesService.Data
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICategoryRepository _categoryRepository;
        private IServiceRepository _serviceRepository;
        private ISpecializationRepository _specializationRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IServiceRepository ServiceRepository
        {
            get 
            {
                if (_serviceRepository == null)
                    _serviceRepository = new ServiceRepository(_repositoryContext);
                return _serviceRepository; 
            }
        }

        public ICategoryRepository CategoryRepository 
        {
            get 
            {
                if(_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_repositoryContext);
                return _categoryRepository;
            }
        }

        public ISpecializationRepository SpecializationRepository
        {
            get 
            {
                if(_specializationRepository == null)
                    _specializationRepository= new SpecializationRepository(_repositoryContext);
                return _specializationRepository;
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
