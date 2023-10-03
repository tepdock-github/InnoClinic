using ServicesService.Domain.Entities;

namespace ServicesService.Domain.Interfaces
{
    public interface ISpecializationRepository
    {
        void CreateSpecialization(Specialization specialization);
        Task<IEnumerable<Specialization>> GetAllSpecializationsAsync(bool trackChanges);
        Task<IEnumerable<Specialization>> GetActiveSpecializationsAsync(bool trackChanges);
        Task<Specialization> GetSpecializationAsync(int id, bool trackChanges);
    }
}
