using Microsoft.EntityFrameworkCore;
using ProfileService.Data.RepositoryExtensions;
using ProfilesService.Domain;
using ProfilesService.Domain.Entities;
using ProfilesService.Domain.Interfaces;
using ProfilesService.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Data.Repository
{
    public class PatientProfileRepository : RepositoryBase<PatientProfile>,
        IPatientProfileRepository
    {
        public PatientProfileRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        public void CreatePatientProfile(PatientProfile patientProfile) => Create(patientProfile);

        public void DeletePatientProfile(PatientProfile patientProfile) => Delete(patientProfile);

        public async Task<PatientProfile?> GetPatientProfile(int patientId, bool trackChanges) =>
            await FindByCondition(p => p.Id.Equals(patientId), trackChanges)
                    .SingleOrDefaultAsync();

        public async Task<PatientProfile?> GetPatientProfileByAccountId(string accountId, bool trackChanges) =>
            await FindByCondition(p => p.AccountId.Equals(accountId), trackChanges)
                    .SingleOrDefaultAsync();

        public async Task<IEnumerable<PatientProfile>> GetPatientProfiles(PatientParameters patientParameters, 
            bool trackChanges) =>
            await FindAll(trackChanges)
                .Search(patientParameters.SearchTerm)
                .ToListAsync();

    }
}
