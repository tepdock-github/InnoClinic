using AutoMapper;
using ServicesService.Domain.DataTransferObjects;
using ServicesService.Domain.Entities;
using ServicesService.Domain.Interfaces;
using ServicesService.ServicesInterfaces;

namespace ServicesService.Services
{
    public class ServiceServices : IServiceServices
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ServiceServices(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ServiceDto?> CreateService(Service service)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryByIdAsync(service.CategoryId, trackChanges: false);
            var specialization = await _repositoryManager.SpecializationRepository.GetSpecializationAsync(service.SpecializationId, trackChanges: false);
            if (category == null || specialization == null)
                return null;

            _repositoryManager.ServiceRepository.CreateService(service);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<bool> EditService(int id, ServicesManipulationDto serviceDto)
        {
            var service = await _repositoryManager.ServiceRepository.GetServiceByIdAsync(id, trackChanges: true);
            if (service == null)
                return false;

            _mapper.Map(serviceDto, service);
            await _repositoryManager.SaveAsync();
            return true;
        }

        public async Task<Service> GetServiceById(int id) => await _repositoryManager.ServiceRepository.GetServiceByIdAsync(id, trackChanges: false);

        public async Task<IEnumerable<Service>> GetServices() => await _repositoryManager.ServiceRepository.GetAllServicesAsync(trackChanges: false);
    }
}
