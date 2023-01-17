using AutoMapper;
using ServicesService.Domain.Interfaces;
using ServicesService.ServicesInterfaces;

namespace ServicesService.Services
{
    public class ServicesManager : IServicesManager
    {
        private IRepositoryManager _repositoryManager;
        private IMapper _mapper;

        private ICategoryServices _categoryServices;
        private IServiceServices _serviceServices;
        private ISpecializationServices _specializationServices;

        public ServicesManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public ICategoryServices CategoryServices 
        {
            get 
            {
                if (_categoryServices == null)
                    _categoryServices = new CategoryServices(_repositoryManager, _mapper);
                return _categoryServices;
            }
        }

        public IServiceServices ServiceServices
        {
            get
            {
                if (_serviceServices == null)
                    _serviceServices = new ServiceServices(_repositoryManager, _mapper);
                return _serviceServices;
            }
        }

        public ISpecializationServices SpecializationServices 
        {
            get
            {
                if(_specializationServices == null)
                    _specializationServices = new SpecializationServices(_repositoryManager, _mapper);
                return _specializationServices;
            }
        }
    }
}
