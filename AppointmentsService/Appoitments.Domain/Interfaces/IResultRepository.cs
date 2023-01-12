using Appoitments.Domain.Entities;

namespace Appoitments.Domain.Interfaces
{
    public interface IResultRepository
    {
        void CreateResult(Result result);
        void DeleteResult(Result result);
        Task<IEnumerable<Result>> GetAllResultByPatient(string patientId, bool trackChanges);
        Task<IEnumerable<Result>> GetAllResultByDoctor(string doctorId, bool trackChanges);
        Task<Result> GetResultById(string id, bool trackChanges);
    }
}
