using Appoitments.Domain.DataTransferObjects;

namespace AppointmentsService.Services.Interfaces
{
    public interface IAppoitmentService
    {
        Task<IEnumerable<AppoitmentDto>> GetAppoitments();
        Task<AppoitmentDto?> GetAppoitmentById(int id);
        Task<IEnumerable<AppoitmentDto>> GetDoctorHistory(string doctorId);
        Task<IEnumerable<AppoitmentDto>> GetPatientHistory(string patientId);
        Task<IEnumerable<AppoitmentDto>> GetDoctorSchedule(string doctorId);
        Task<IEnumerable<AppoitmentDto>> GetPatientAppoitments(string patientId);
        Task<AppoitmentDto> CreateAppoitment(AppoitmentManipulationDto appoitmentDto);
        Task UpdateAppoitment(int id, AppoitmentManipulationDto appoitmentDto);
        Task DeleteAppoitment(int id);
    }
}
