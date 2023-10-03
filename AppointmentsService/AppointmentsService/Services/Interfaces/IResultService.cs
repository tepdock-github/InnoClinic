using Appoitments.Domain.DataTransferObjects;

namespace AppointmentsService.Services.Interfaces
{
    public interface IResultService
    {
        Task<ResultDto?> GetResultById(int id);
        Task<ResultDto?> GetResultByAppoitmentId(int id);
        Task<IEnumerable<ResultDto>> GetResults(); 
        Task<IEnumerable<ResultDto>> GetResultsByPatient(string patientId);
        Task<IEnumerable<ResultDto>> GetResultsByDoctor(string doctorId);
        Task<ResultDto> CreateResult(ResultManipulationDto resultDto);
        Task DeleteResult(int id);
        Task UpdateResult(int id, ResultManipulationDto resultDto);
    }
}
