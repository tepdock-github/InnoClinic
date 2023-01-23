using ProfilesService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Data.RepositoryExtensions
{
    public static class DoctorRepositoryExtension
    {
        public static IQueryable<DoctorsProfile> Search(this IQueryable<DoctorsProfile> doctorsProfiles,
            string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return doctorsProfiles;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return doctorsProfiles.Where(p => p.FirstName.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<DoctorsProfile> Filter(this IQueryable<DoctorsProfile> doctorsProfiles,
            int specialization, int office)
        {
            if (specialization == 0 && office == 0)
                return doctorsProfiles;
            else if (specialization != 0 && office == 0)
                return doctorsProfiles.Where(d => d.SpecializationId == specialization);
            else if (specialization == 0 && office != 0)
                return doctorsProfiles.Where(d => d.OfficeId == office);
            else return doctorsProfiles.Where(d => d.OfficeId == office &&
                d.SpecializationId == specialization);
        }
    }
}
