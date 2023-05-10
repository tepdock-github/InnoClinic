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
    public class DoctorProfileService : IDoctorProfileService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public DoctorProfileService(IRepositoryManager repositoryManager, IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<DoctorProfileDto> CreateDoctorProfile(DoctorProfileManipulationDto doctorProfileDto)
        {
            var doctorProfile = _mapper.Map<DoctorsProfile>(doctorProfileDto);

            _repositoryManager.DoctorProfile.CreateDoctorProfile(doctorProfile);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<DoctorProfileDto>(doctorProfile);
        }

        public async Task DeleteDoctorProfile(int id)
        {
            var profile = await _repositoryManager.DoctorProfile.GetDoctorProfile(id, trackChanges: false);
            if (profile == null)
                throw new NotFoundException("doctor with id: " + id + " wasnt found");

            _repositoryManager.DoctorProfile.DeleteDoctorProfile(profile);
            await _repositoryManager.SaveAsync();
        }

        public async Task<DoctorProfileDto> GetDoctorProfile(int id)
        {
            var profile = await _repositoryManager.DoctorProfile.GetDoctorProfile(id, trackChanges: false);
            if (profile == null)
                throw new NotFoundException("doctor with id: " + id + " wasnt found");

            return _mapper.Map<DoctorProfileDto>(profile);
        }

        public async Task<DoctorProfileDto> GetDoctorProfileByAccount(string id)
        {
            var profile = await _repositoryManager.DoctorProfile.GetDoctorProfileByAccountId(id, trackChanges: false);
            if (profile == null)
                throw new NotFoundException("doctor with account id: " + id + " wasnt found");

            return _mapper.Map<DoctorProfileDto>(profile);
        }

        public async Task<IEnumerable<DoctorProfileDto>> GetDoctorProfiles(DoctorParameters doctorParameters)
        {
            var profiles = await _repositoryManager.DoctorProfile.GetDoctorsProfiles(doctorParameters, trackChanges: false);

            return _mapper.Map<IEnumerable<DoctorProfileDto>>(profiles);
        }

        public async Task UpdaeDoctorProfile(int id, DoctorProfileManipulationDto doctorProfileDto)
        {
            var profile = await _repositoryManager.DoctorProfile.GetDoctorProfile(id, trackChanges: true);
            if(profile == null)
                throw new NotFoundException("doctor with id: " + id + " wasnt found");

            _mapper.Map(doctorProfileDto, profile);
            await _repositoryManager.SaveAsync();

            await _publishEndpoint.Publish<IDoctorProfileManipulation>(new
            {
                Id = id.ToString(),
                doctorProfileDto.FirstName,
                doctorProfileDto.LastName,
                doctorProfileDto.MiddleName
            });
        }
    }
}
