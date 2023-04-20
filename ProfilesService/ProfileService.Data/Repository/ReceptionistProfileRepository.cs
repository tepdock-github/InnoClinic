using Microsoft.EntityFrameworkCore;
using ProfilesService.Domain;
using ProfilesService.Domain.Entities;
using ProfilesService.Domain.Interfaces;

namespace ProfileService.Data.Repository
{
    public class ReceptionistProfileRepository : RepositoryBase<ReceptionistProfile>,
        IReceptionistProfileRepository
    {
        public ReceptionistProfileRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateReceptionistProfile(ReceptionistProfile receptionistProfile) => Create(receptionistProfile);

        public void DeleteReceptionistProfile(ReceptionistProfile receptionistProfile) => Delete(receptionistProfile);

        public async Task<ReceptionistProfile?> GetReceptionistProfile(int receptionistId, bool trackChanges) =>
            await FindByCondition(r => r.Id.Equals(receptionistId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<ReceptionistProfile?> GetReceptionistProfileByAccountId(string accountId, bool trackChanges) =>
            await FindByCondition(r => r.AccountId.Equals(accountId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<ReceptionistProfile>> GetReceptionistProfiles(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();
    }
}
