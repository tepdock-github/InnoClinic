using ProfilesService.Domain.DataTransferObjects;

namespace ProfilesService.Services.Interfaces
{
    public interface IReceptionistProfileService
    {
        Task<IEnumerable<ReceptionistProfileDto>> GetReceptionistProfiles();
        Task<ReceptionistProfileDto> GetReceptionistProfile(int id);
        Task<ReceptionistProfileDto> GetReceptionistProfileByAccount(string id);
        Task<ReceptionistProfileDto> CreateReceptionistProfile(ReceptionistProfileManipulationDto receptionistProfile);
        Task UpdateReceptionistProfile(int id, ReceptionistProfileManipulationDto receptionistProfileDto);
        Task DeleteReceptionistProfile(int id);
    }
}