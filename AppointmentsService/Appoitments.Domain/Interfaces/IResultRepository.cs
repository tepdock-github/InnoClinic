using Appoitments.Domain.Entities;

namespace Appoitments.Domain.Interfaces
{
    public interface IResultRepository
    {
        void CreateResult(Result result);
        void DeleteResult(Result result);
        Task<IEnumerable<Result>> GetAllResultByPatient(int patientId, bool trackChanges);
        Task<IEnumerable<Result>> GetAllResultByDoctor(int doctorId, bool trackChanges);
        Task<Result?> GetResultById(int id, bool trackChanges);
    }
}
