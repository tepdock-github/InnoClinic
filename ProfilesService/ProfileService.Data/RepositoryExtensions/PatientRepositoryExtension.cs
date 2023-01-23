using ProfilesService.Domain.Entities;

namespace ProfileService.Data.RepositoryExtensions
{
    public static class PatientRepositoryExtension
    {
        public static IQueryable<PatientProfile> Search(this IQueryable<PatientProfile> patientProfiles,
            string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return patientProfiles;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return patientProfiles.Where(p => p.FirstName.ToLower().Contains(lowerCaseTerm));
        }
    }
}
