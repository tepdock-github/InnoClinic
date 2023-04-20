using ProfilesService.Domain.Entities;
using ProfilesService.Domain.RequestFeatures;

namespace ProfilesService.Domain.Interfaces
{
    public interface IPatientProfileRepository
    {
        void CreatePatientProfile(PatientProfile patientProfile);
        void DeletePatientProfile(PatientProfile patientProfile);
        Task<IEnumerable<PatientProfile>> GetPatientProfiles(PatientParameters patientParameters, bool trackChanges);
        Task<PatientProfile?> GetPatientProfile(int patientId, bool trackChanges);
        Task<PatientProfile?> GetPatientProfileByAccountId(string accountId, bool trackChanges);
    }
}
