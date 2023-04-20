using AutoMapper;
using MassTransit;
using ProfilesService.Domain.DataTransferObjects;
using ProfilesService.Domain.Entities;
using ProfilesService.Domain.Interfaces;
using ProfilesService.Services.Interfaces;
using SharedModelsInnoClinic;

namespace ProfilesService.Services.Implimentation
{
    public class ReceptionistProfileeService : IReceptionistProfileService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public ReceptionistProfileeService(IRepositoryManager repositoryManager, IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<ReceptionistProfileDto> CreateReceptionistProfile(ReceptionistProfileManipulationDto receptionistProfile)
        {
            var profile = _mapper.Map<ReceptionistProfile>(receptionistProfile);

            _repositoryManager.ReceptionistProfile.CreateReceptionistProfile(profile);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<ReceptionistProfileDto>(profile);
        }

        public async Task DeleteReceptionistProfile(int id)
        {
            var profile = await _repositoryManager.ReceptionistProfile.GetReceptionistProfile(id, trackChanges: false);
            if (profile == null)
                throw new BadHttpRequestException("receptionist with id: " + id + " wasnt found", 404);

            _repositoryManager.ReceptionistProfile.DeleteReceptionistProfile(profile);
            await _repositoryManager.SaveAsync();
        }

        public async Task<ReceptionistProfileDto> GetReceptionistProfile(int id)
        {
            var profile = await _repositoryManager.ReceptionistProfile.GetReceptionistProfile(id, trackChanges: false);
            if (profile == null)
                throw new BadHttpRequestException("receptionist with id: " + id + " wasnt found", 404);

            return _mapper.Map<ReceptionistProfileDto>(profile);
        }

        public async Task<ReceptionistProfileDto> GetReceptionistProfileByAccount(string id)
        {
            var profile = await _repositoryManager.ReceptionistProfile.GetReceptionistProfileByAccountId(id, trackChanges: false);
            if (profile == null)
                throw new BadHttpRequestException("receptionist with account id: " + id + " wasnt found", 404);

            return _mapper.Map<ReceptionistProfileDto>(profile);
        }

        public async Task<IEnumerable<ReceptionistProfileDto>> GetReceptionistProfiles()
        {
            var profiles = await _repositoryManager.ReceptionistProfile.GetReceptionistProfiles(trackChanges: false);

            return _mapper.Map<IEnumerable<ReceptionistProfileDto>>(profiles);
        }

        public async Task UpdateReceptionistProfile(int id, ReceptionistProfileManipulationDto receptionistProfileDto)
        {
            var profile = await _repositoryManager.ReceptionistProfile.GetReceptionistProfile(id, trackChanges: true);
            if (profile == null)
                throw new BadHttpRequestException("receptionist with id: " + id + " wasnt found", 404);

            _mapper.Map(receptionistProfileDto, profile);
            await _repositoryManager.SaveAsync();

            await _publishEndpoint.Publish<IReceptionistProfileManipulation>(new
            {
                Id = id,
                receptionistProfileDto.FirstName, 
                receptionistProfileDto.LastName,
                receptionistProfileDto.MiddleName,
                receptionistProfileDto.OfficeId,
                receptionistProfileDto.AccountId
            });
        }
    }
}
