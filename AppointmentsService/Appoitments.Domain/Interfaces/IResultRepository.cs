using Appoitments.Domain.Entities;

namespace Appoitments.Domain.Interfaces
{
    public interface IResultRepository
    {
        void CreateResult(Result result);
        void DeleteResult(Result result);
        Task<IEnumerable<Result>> GetAllResultByPatient(string patientId, bool trackChanges);
        Task<IEnumerable<Result>> GetAllResultByDoctor(string doctorId, bool trackChanges);
        Task<IEnumerable<Result>> GetResults(bool trackChanges);
        Task<Result?> GetResultById(int id, bool trackChanges);
        Task<Result?> GetResultByAppoitmentId(int id, bool trackChanges);
    }
}
