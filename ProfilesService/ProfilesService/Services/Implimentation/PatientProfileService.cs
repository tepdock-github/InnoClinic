using AutoMapper;
using MassTransit;
using ProfilesService.Domain.DataTransferObjects;
using ProfilesService.Domain.Entities;
using ProfilesService.Domain.Interfaces;
using ProfilesService.Domain.RequestFeatures;
using ProfilesService.Extensions.Exceptions;
using ProfilesService.Services.Interfaces;
using SharedModelsInnoClinic;

namespace ProfilesService.Services.Implimentation
{
    public class PatientProfileService : IPatientProfileService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public PatientProfileService(IRepositoryManager repositoryManager, IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
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
                throw new NotFoundException("patient with id: " + id + " wasnt found");

            _repositoryManager.PatientProfile.DeletePatientProfile(profile);
            await _repositoryManager.SaveAsync();
        }

        public async Task<PatientProfileDto> GetPatientProfile(int id)
        {
            var profile = await _repositoryManager.PatientProfile.GetPatientProfile(id, trackChanges: false);
            if (profile == null)
                throw new NotFoundException("patient with id: " + id + " wasnt found");

            return _mapper.Map<PatientProfileDto>(profile);
        }

        public async Task<PatientProfileDto> GetPatientProfileByAccount(string id)
        {
            var profile = await _repositoryManager.PatientProfile.GetPatientProfileByAccountId(id, trackChanges: false);
            if (profile == null)
                throw new NotFoundException("patient with account id: " + id + " wasnt found");

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
                throw new NotFoundException("patient with id: " + id + " wasnt found");

            _mapper.Map(patientProfileDto, profile);
            await _repositoryManager.SaveAsync();

            await _publishEndpoint.Publish<IPatientProfileManipulation>(new
            {
                Id = id,
                patientProfileDto.FirstName,
                patientProfileDto.LastName,
                patientProfileDto.IsLinkedToAccount,
                patientProfileDto.AccountId
            });
        }
    }
}
