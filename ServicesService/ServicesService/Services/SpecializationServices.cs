using AutoMapper;
using ServicesService.Domain.DataTransferObjects;
using ServicesService.Domain.Entities;
using ServicesService.Domain.Interfaces;
using ServicesService.ServiceExtensions.Exceptions;
using ServicesService.ServicesInterfaces;

namespace ServicesService.Services
{
    public class SpecializationServices : ISpecializationServices
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public SpecializationServices(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<SpecializationDto> CreateSpecialization(SpecializationManipulationDto specializationDto)
        {
            var specialization = _mapper.Map<Specialization>(specializationDto);

            _repositoryManager.SpecializationRepository.CreateSpecialization(specialization);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<SpecializationDto>(specialization);
        }

        public async Task EditSpecialization(int id, SpecializationManipulationDto specializationDto)
        {
            var specialization = await _repositoryManager.SpecializationRepository.GetSpecializationAsync(id, trackChanges: true);
            if (specialization == null)
                throw new NotFoundException("specialization with id: " + id + " wasn't found");

            _mapper.Map(specializationDto, specialization);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<SpecializationDto>> GetAllSpecializations()
        {
            var specializations = await _repositoryManager.SpecializationRepository.GetAllSpecializationsAsync(trackChanges: false);

            return _mapper.Map<IEnumerable<SpecializationDto>>(specializations);
        }

        public async Task<SpecializationDto?> GetSpecializationById(int id)
        {
            var specialization = await _repositoryManager.SpecializationRepository.GetSpecializationAsync(id, trackChanges: false);
            if (specialization == null)
                throw new NotFoundException("specialization with id: " + id + " wasn't found");

            return _mapper.Map<SpecializationDto?>(specialization);
        }
    }
}
