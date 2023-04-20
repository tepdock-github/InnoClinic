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
    public class DoctorProfileRepository : RepositoryBase<DoctorsProfile>,
        IDoctorProfileRepository
    {
        public DoctorProfileRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        public void CreateDoctorProfile(DoctorsProfile doctorsProfile) => Create(doctorsProfile);

        public void DeleteDoctorProfile(DoctorsProfile doctorsProfile) => Delete(doctorsProfile);

        public async Task<DoctorsProfile?> GetDoctorProfile(int doctorId, bool trackChanges) =>
            await FindByCondition(d => d.Id.Equals(doctorId), trackChanges)
                    .SingleOrDefaultAsync();

        public async Task<DoctorsProfile?> GetDoctorProfileByAccountId(string accountId, bool trackChanges) =>
            await FindByCondition(d => d.AccountId.Equals(accountId), trackChanges)
                    .SingleOrDefaultAsync();

        public async Task<IEnumerable<DoctorsProfile>> GetDoctorsProfiles(DoctorParameters doctorParameters,
            bool trackChanges) =>
            await FindAll(trackChanges)
            .Search(doctorParameters.SearchTerm)
            .ToListAsync();
    }
}
