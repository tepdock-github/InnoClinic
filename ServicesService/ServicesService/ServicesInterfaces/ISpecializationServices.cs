using ServicesService.Domain.DataTransferObjects;

namespace ServicesService.ServicesInterfaces
{
    public interface ISpecializationServices
    {
        Task EditSpecialization(int id, SpecializationManipulationDto specializationDto);
        Task<SpecializationDto> CreateSpecialization(SpecializationManipulationDto specializationDto);
        Task<SpecializationDto?> GetSpecializationById(int id);
        Task<IEnumerable<SpecializationDto>> GetAllSpecializations();
    }
}
