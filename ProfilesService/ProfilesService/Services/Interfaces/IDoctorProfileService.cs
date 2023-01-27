using ProfilesService.Domain.DataTransferObjects;
using ProfilesService.Domain.RequestFeatures;

namespace ProfilesService.Services.Interfaces
{
    public interface IDoctorProfileService
    {
        Task<IEnumerable<DoctorProfileDto>> GetDoctorProfiles(DoctorParameters doctorParameters);
        Task<DoctorProfileDto> GetDoctorProfile(int id);
        Task<DoctorProfileDto> CreateDoctorProfile(DoctorProfileManipulationDto doctorProfileDto);
        Task DeleteDoctorProfile(int id);
        Task UpdaeDoctorProfile(int id, DoctorProfileManipulationDto doctorProfileDto);
    }
}
