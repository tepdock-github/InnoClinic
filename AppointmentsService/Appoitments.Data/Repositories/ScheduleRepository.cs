using Appoitments.Domain;
using Appoitments.Domain.Entities;
using Appoitments.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Appoitments.Data.Repositories
{
    public class ScheduleRepository : RepositoryBase<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateSchedule(Schedule schedule) => Create(schedule);

        public void DeleteSchedule(Schedule schedule) => Delete(schedule);

        public async Task<IEnumerable<Schedule>> GetAllSchedules(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<IEnumerable<Schedule>> GetAllSchedulesByDoctorAndDate(string doctorId, string date, bool trackChanges) =>
            await FindByCondition(s => s.DoctorId.Equals(doctorId) && s.Date.Equals(date), trackChanges)
            .ToListAsync();

        public async Task<IEnumerable<Schedule>> GetFreeSchedulesByDoctorAndDate(string doctorId, string date, bool trackChanges) =>
            await FindByCondition(s => s.DoctorId.Equals(doctorId) && s.Date.Equals(date) && s.isBooked == false, trackChanges)
            .ToListAsync();

        public async Task<Schedule?> GetScheduleByAppoitmentId(int appoitmentId, bool trackChanges) =>
            await FindByCondition(s => s.AppoitmentId == appoitmentId, trackChanges)
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<Schedule>> GetScheduleByDoctor(string doctorId, bool trackChanges) =>
            await FindByCondition(s => s.DoctorId.Equals(doctorId), trackChanges)
            .ToListAsync();

        public async Task<Schedule?> GetScheduleById(int id, bool trackChanges) =>
            await FindByCondition(s => s.Id == id, trackChanges)
            .FirstOrDefaultAsync();
    }
}
