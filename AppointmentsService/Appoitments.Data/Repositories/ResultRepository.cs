using Appoitments.Domain;
using Appoitments.Domain.Entities;
using Appoitments.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Appoitments.Data.Repositories
{
    public class ResultRepository : RepositoryBase<Result>, IResultRepository
    {
        public ResultRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        public void CreateResult(Result result) => Create(result);

        public void DeleteResult(Result result) => Delete(result);

        public async Task<IEnumerable<Result>> GetAllResultByDoctor(string doctorId, bool trackChanges) =>
            await FindByCondition(r => r.Appoitment.DoctorId.Equals(doctorId), trackChanges)
            .ToListAsync();

        public async Task<IEnumerable<Result>> GetAllResultByPatient(string patientId, bool trackChanges) =>
            await FindByCondition(r => r.Appoitment.PatientId.Equals(patientId), trackChanges)
            .ToListAsync();

        public async Task<Result?> GetResultById(int id, bool trackChanges) =>
            await FindByCondition(r => r.Id.Equals(id), trackChanges)
            .FirstOrDefaultAsync();
    }
}
