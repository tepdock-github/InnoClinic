using AutoMapper;
using CustomExceptionMiddleware.Exceptions;
using MassTransit;
using ServicesService.Domain.DataTransferObjects;
using ServicesService.Domain.Entities;
using ServicesService.Domain.Interfaces;
using ServicesService.ServicesInterfaces;
using SharedModelsInnoClinic;

namespace ServicesService.Services
{
    public class ServiceServices : IServiceServices
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public ServiceServices(IRepositoryManager repositoryManager, IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
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

            var category = await _repositoryManager.CategoryRepository.GetCategoryByIdAsync(service.CategoryId, trackChanges: false);
            var specialization = await _repositoryManager.SpecializationRepository.GetSpecializationAsync(service.SpecializationId, trackChanges: false);
            if (category == null)
                throw new NotFoundException("Category wasn't found");
            if (specialization == null)
                throw new NotFoundException("Specialization wasn't found");

            _mapper.Map(serviceDto, service);
            await _repositoryManager.SaveAsync();

            await _publishEndpoint.Publish<IServiceManipulation>(new
            {
                Id = id,
                serviceDto.ServiceName,
                serviceDto.Price,
                serviceDto.IsActive,
                serviceDto.CategoryId,
                serviceDto.SpecializationId

            });
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
