using AutoMapper;
using ProfilesService.Domain.DataTransferObjects;
using ProfilesService.Domain.Entities;
using ProfilesService.Domain.Interfaces;
using ProfilesService.Domain.RequestFeatures;
using ProfilesService.Services.Interfaces;

namespace ProfilesService.Services.Implimentation
{
    public class PatientProfileService : IPatientProfileService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public PatientProfileService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<PatientProfileDto> CreatePatientProfile(PatientProfileManipulationDto patientProfileDto)
        {
            var profile = _mapper.Map<PatientProfile>(patientProfileDto);

            _repositoryManager.PatientProfile.CreatePatientProfile(profile);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<PatientProfileDto>(profile);
        }

        public async Task DeletePatientProfile(int id)
        {
            var profile = await _repositoryManager.PatientProfile.GetPatientProfile(id, trackChanges: false);
            if (profile == null)
                throw new BadHttpRequestException("patient with id: " + id + " wasnt found", 404);

            _repositoryManager.PatientProfile.DeletePatientProfile(profile);
            await _repositoryManager.SaveAsync();
        }

        public async Task<PatientProfileDto> GetPatientProfile(int id)
        {
            var profile = await _repositoryManager.PatientProfile.GetPatientProfile(id, trackChanges: false);
            if (profile == null)
                throw new BadHttpRequestException("patient with id: " + id + " wasnt found", 404);

            return _mapper.Map<PatientProfileDto>(profile);
        }

        public async Task<IEnumerable<PatientProfileDto>> GetPatientProfiles(PatientParameters patientParameters)
        {
            var profiles = await _repositoryManager.PatientProfile.GetPatientProfiles(patientParameters, trackChanges: false);

            return _mapper.Map<IEnumerable<PatientProfileDto>>(profiles);
        }

        public async Task UpdatePatientProfile(int id, PatientProfileManipulationDto patientProfileDto)
        {
            var profile = await _repositoryManager.PatientProfile.GetPatientProfile(id, trackChanges: true);
            if (profile == null)
                throw new BadHttpRequestException("patient with id: " + id + " wasnt found", 404);

            _mapper.Map(patientProfileDto, profile);
            await _repositoryManager.SaveAsync();
        }
    }
}
