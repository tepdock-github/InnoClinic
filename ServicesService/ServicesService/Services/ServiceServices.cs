using AutoMapper;
using ServicesService.Domain.DataTransferObjects;
using ServicesService.Domain.Entities;
using ServicesService.Domain.Interfaces;
using ServicesService.ServiceExtensions.Exceptions;
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

        public async Task<ServiceDto?> CreateService(ServicesManipulationDto serviceDto)
        {
            var service = _mapper.Map<Service>(serviceDto);

            var category = await _repositoryManager.CategoryRepository.GetCategoryByIdAsync(service.CategoryId, trackChanges: false);
            var specialization = await _repositoryManager.SpecializationRepository.GetSpecializationAsync(service.SpecializationId, trackChanges: false);
            if (category == null)
                throw new NotFoundException("Category wasn't found");
            if (specialization == null)
                throw new NotFoundException("Specialization wasn't found");

            _repositoryManager.ServiceRepository.CreateService(service);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<ServiceDto>(service);
        }

        public async Task EditService(int id, ServicesManipulationDto serviceDto)
        {
            var service = await _repositoryManager.ServiceRepository.GetServiceByIdAsync(id, trackChanges: true);
            if (service == null)
                throw new NotFoundException("Service with id: " + id + "wasn't found");

            _mapper.Map(serviceDto, service);
            await _repositoryManager.SaveAsync();
        }

        public async Task<ServiceDto> GetServiceById(int id)
        {
            var service = await _repositoryManager.ServiceRepository.GetServiceByIdAsync(id, trackChanges: false);
            if (service == null)
                throw new NotFoundException("Service with id: " + id + "wasn't found");

            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<IEnumerable<ServiceDto>> GetServices()
        {
            var services = await _repositoryManager.ServiceRepository.GetAllServicesAsync(trackChanges: false);

            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }
    }
}
