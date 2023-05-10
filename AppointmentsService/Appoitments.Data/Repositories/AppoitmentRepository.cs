using Appoitments.Domain;
using Appoitments.Domain.Entities;
using Appoitments.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Appoitments.Data.Repositories
{
    public class AppoitmentRepository : RepositoryBase<Appoitment>, IAppoitmentRepository
    {
        public AppoitmentRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        public void CreateAppoitment(Appoitment appoitment) => Create(appoitment);

        public void DeleteAppoitment(Appoitment appoitment) => Delete(appoitment);

        public async Task<IEnumerable<Appoitment>> GetAllAppoitments(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<IEnumerable<Appoitment>> GetAppoitmentsHistoryByDoctor(string doctorId, bool trackChanges) =>
            await FindByCondition(a => a.DoctorId.Equals(doctorId) && a.isComplete == true, trackChanges)
            .ToListAsync();

        public async Task<IEnumerable<Appoitment>> GetAppoitmentsHistoryByPatient(string patientId, bool trackChanges) =>
            await FindByCondition(a => a.PatientId.Equals(patientId) && a.isComplete == true, trackChanges)
            .ToListAsync();

        public async Task<IEnumerable<Appoitment>> GetAppoitmentsByPatient(string patientId, bool trackChanges) =>
            await FindByCondition(a => a.PatientId.Equals(patientId) && a.isComplete == false, trackChanges).ToListAsync();

        public async Task<IEnumerable<Appoitment>> GetAppoitmentsByDoctor(string doctorId, bool trackChanges) =>
            await FindByCondition(a => a.DoctorId.Equals(doctorId), trackChanges).ToListAsync();

        public async Task<IEnumerable<Appoitment>> GetAppoitmentsScheduleByDocrot(string doctorId, bool trackChanges) =>
            await FindByCondition(a => a.DoctorId.Equals(doctorId) && a.isApproved == true && a.isComplete == false, trackChanges)
            .ToListAsync();

        public async Task<Appoitment?> GetAppoitmentId(int id, bool trackChanges) =>
            await FindByCondition(a => a.Id.Equals(id), trackChanges)
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<Appoitment>> GetAppoitmentsByServiceId(int serviceId, bool trackChanges) =>
            await FindByCondition(a => a.ServiceId.Equals(serviceId), trackChanges).ToListAsync();
    }
}
