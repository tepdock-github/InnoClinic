using Appoitments.Domain.Entities;

namespace Appoitments.Domain.Interfaces
{
    public interface IAppoitmentRepository
    {
        void CreateAppoitment(Appoitment appoitment);
        void DeleteAppoitment(Appoitment appoitment);
        Task<IEnumerable<Appoitment>> GetAppoitmentsScheduleByDocrot(int doctorId, bool trackChanges);
        Task<IEnumerable<Appoitment>> GetAllAppoitments(bool trackChanges);
        Task<IEnumerable<Appoitment>> GetAppoitmentsByPatient(int patientId, bool trackChanges);
        Task<IEnumerable<Appoitment>> GetAppoitmentsHistoryByDoctor(int doctorId, bool trackChanges);
        Task<IEnumerable<Appoitment>> GetAppoitmentsHistoryByPatient(int patientId, bool trackChanges);
        Task<Appoitment?> GetAppoitmentId(int id, bool trackChanges);
    }
}
