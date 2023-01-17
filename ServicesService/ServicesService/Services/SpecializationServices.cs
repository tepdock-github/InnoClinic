using AutoMapper;
using ServicesService.Domain.DataTransferObjects;
using ServicesService.Domain.Entities;
using ServicesService.Domain.Interfaces;
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

        public async Task<SpecializationDto> CreateSpecialization(Specialization specialization)
        {
            _repositoryManager.SpecializationRepository.CreateSpecialization(specialization);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<SpecializationDto>(specialization);
        }

        public async Task<bool> EditSpecialization(int id, SpecializationManipulationDto specializationDto)
        {
            var specialization = await _repositoryManager.SpecializationRepository.GetSpecializationAsync(id, trackChanges: true);
            if (specialization == null)
                return false;

            _mapper.Map(specializationDto, specialization);
            await _repositoryManager.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<Specialization>> GetAllSpecializations() => await _repositoryManager.SpecializationRepository.GetAllSpecializationsAsync(trackChanges: false);

        public async Task<Specialization?> GetSpecializationById(int id) => await _repositoryManager.SpecializationRepository.GetSpecializationAsync(id, trackChanges: false);
    }
}
