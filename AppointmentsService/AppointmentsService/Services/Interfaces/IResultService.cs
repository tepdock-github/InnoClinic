using Appoitments.Domain.DataTransferObjects;

namespace AppointmentsService.Services.Interfaces
{
    public interface IResultService
    {
        Task<ResultDto?> GetResultById(int id);
        Task<IEnumerable<ResultDto>> GetResultsByPatient(int patientId);
        Task<IEnumerable<ResultDto>> GetResultsByDoctor(int doctorId);
        Task<ResultDto> CreateResult(ResultManipulationDto resultDto);
        Task DeleteResult(int id);
        Task UpdateResult(int id, ResultManipulationDto resultDto);
    }
}
