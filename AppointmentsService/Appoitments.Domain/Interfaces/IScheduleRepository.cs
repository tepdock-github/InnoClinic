using Appoitments.Domain.Entities;

namespace Appoitments.Domain.Interfaces
{
    public interface IScheduleRepository
    {
        void CreateSchedule(Schedule schedule);
        void DeleteSchedule(Schedule schedule);
        Task<IEnumerable<Schedule>> GetAllSchedules(bool trackChanges);
        Task<Schedule?> GetScheduleById(int id, bool trackChanges);
        Task<IEnumerable<Schedule>> GetAllSchedulesByDoctorAndDate(string doctorId, string date, bool trackChanges);
        Task<IEnumerable<Schedule>> GetScheduleByDoctor(string doctorId, bool  trackChanges);
        Task<IEnumerable<Schedule>> GetFreeSchedulesByDoctorAndDate(string doctorId, string date, bool trackChanges);
        Task<Schedule?> GetScheduleByAppoitmentId(int appoitmentId, bool trackChanges);
    }
}
