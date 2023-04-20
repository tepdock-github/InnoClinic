using ProfilesService.Domain.Entities;
using ProfilesService.Domain.RequestFeatures;

namespace ProfilesService.Domain.Interfaces
{
    public interface IDoctorProfileRepository
    {
        Task<IEnumerable<DoctorsProfile>> GetDoctorsProfiles(DoctorParameters doctorParameters, bool trackChanges);
        Task<DoctorsProfile?> GetDoctorProfile(int doctorId, bool trackChanges);
        Task<DoctorsProfile?> GetDoctorProfileByAccountId(string accountId, bool trackChanges);
        void CreateDoctorProfile(DoctorsProfile doctorsProfile);
        void DeleteDoctorProfile(DoctorsProfile doctorsProfile);
    }
}
