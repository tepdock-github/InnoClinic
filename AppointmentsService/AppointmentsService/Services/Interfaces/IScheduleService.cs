using Appoitments.Domain.DataTransferObjects;

namespace AppointmentsService.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<ScheduleDto?> CreateScheduleAsync(ScheduleManipulationDto scheduleDto);
        Task UpdateScheduleAsync(int id, ScheduleManipulationDto scheduleDto);
        Task DeleteScheduleAsync(int id);
        Task<IEnumerable<ScheduleDto>> GetAllSchedulesAsync();
        Task<ScheduleDto?> GetScheduleAsync(int id);
        Task<ScheduleDto?> GetScheduleByAppoitmentIdAsync(int appoitmentId);
        Task<IEnumerable<ScheduleDto>> GetAllSchedulesByDoctorAsync(string doctorId);
        Task<IEnumerable<ScheduleDto>> GetAllSchedulesByDoctorAndDateAsync(string doctorId, string date);
        Task<IEnumerable<ScheduleDto>> GetFreeSchedulesByDoctorAndDate(string doctorId, string date);

    }
}
