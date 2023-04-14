using Appoitments.Domain.Entities;

namespace Appoitments.Domain.Interfaces
{
    public interface IAppoitmentRepository
    {
        void CreateAppoitment(Appoitment appoitment);
        void DeleteAppoitment(Appoitment appoitment);
        Task<IEnumerable<Appoitment>> GetAppoitmentsScheduleByDocrot(Guid doctorId, bool trackChanges);
        Task<IEnumerable<Appoitment>> GetAllAppoitments(bool trackChanges);
        Task<IEnumerable<Appoitment>> GetAppoitmentsByPatient(Guid patientId, bool trackChanges);
        Task<IEnumerable<Appoitment>> GetAppoitmentsByDoctor(Guid doctorId, bool trackChanges);
        Task<IEnumerable<Appoitment>> GetAppoitmentsHistoryByDoctor(Guid doctorId, bool trackChanges);
        Task<IEnumerable<Appoitment>> GetAppoitmentsHistoryByPatient(Guid patientId, bool trackChanges);
        Task<Appoitment?> GetAppoitmentId(int id, bool trackChanges);
        Task<IEnumerable<Appoitment>> GetAppoitmentsByServiceId(int serviceId, bool trackChanges);
    }
}
