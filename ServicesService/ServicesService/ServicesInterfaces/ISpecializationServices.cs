using ServicesService.Domain.DataTransferObjects;
using ServicesService.Domain.Entities;

namespace ServicesService.ServicesInterfaces
{
    public interface ISpecializationServices
    {
        Task EditSpecialization(int id, SpecializationManipulationDto specializationDto);
        Task<SpecializationDto> CreateSpecialization(Specialization specialization);
        Task<Specialization?> GetSpecializationById(int id);
        Task<IEnumerable<Specialization>> GetAllSpecializations();
    }
}
