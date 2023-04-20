using ProfilesService.Domain.Entities;

namespace ProfilesService.Domain.Interfaces
{
    public interface IReceptionistProfileRepository
    {
        Task<IEnumerable<ReceptionistProfile>> GetReceptionistProfiles(bool trackChanges);
        Task<ReceptionistProfile?> GetReceptionistProfile(int receptionistId, bool trackChanges);
        Task<ReceptionistProfile?> GetReceptionistProfileByAccountId(string accountId, bool trackChanges);
        void CreateReceptionistProfile(ReceptionistProfile receptionistProfile);
        void DeleteReceptionistProfile(ReceptionistProfile receptionistProfile);
    }
}
