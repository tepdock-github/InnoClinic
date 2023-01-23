using ProfilesService.Domain.DataTransferObjects;
using ProfilesService.Domain.RequestFeatures;

namespace ProfilesService.Services.Interfaces
{
    public interface IPatientProfileService
    {
        Task<IEnumerable<PatientProfileDto>> GetPatientProfiles(PatientParameters patientParameters);
        Task<PatientProfileDto> GetPatientProfile(int id);
        Task<PatientProfileDto> CreatePatientProfile(PatientProfileManipulationDto patientProfileDto);
        Task UpdatePatientProfile(int id, PatientProfileManipulationDto patientProfileDto);
        Task DeletePatientProfile(int id);
    }
}
