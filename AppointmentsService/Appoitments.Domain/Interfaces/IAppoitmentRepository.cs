using Appoitments.Domain.Entities;

namespace Appoitments.Domain.Interfaces
{
    public interface IAppoitmentRepository
    {
        void CreateAppoitment(Appoitment appoitment);
        void DeleteAppoitment(Appoitment appoitment);
        Task<IEnumerable<Appoitment>> GetAppoitmentsScheduleByDocrot(string doctorId, bool trackChanges);
        Task<IEnumerable<Appoitment>> GetAllAppoitments(bool trackChanges);
        Task<IEnumerable<Appoitment>> GetAppoitmentsByPatient(string patientId, bool trackChanges);
        Task<IEnumerable<Appoitment>> GetAppoitHistoryByDoctor(string doctorId, bool trackChanges);
        Task<IEnumerable<Appoitment>> GetAppoitHistoryByPatient(string patientId, bool trackChanges);
        Task<Appoitment> GetAppoitmentId(string id, bool trackChanges);
    }
}
