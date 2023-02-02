using Appoitments.Domain.DataTransferObjects;

namespace AppointmentsService.Services.Interfaces
{
    public interface IAppoitmentService
    {
        Task<IEnumerable<AppoitmentDto>> GetAppoitments();
        Task<AppoitmentDto?> GetAppoitmentById(int id);
        Task<IEnumerable<AppoitmentDto>> GetDoctorHistory(int doctorId);
        Task<IEnumerable<AppoitmentDto>> GetPatientHistory(int patientId);
        Task<IEnumerable<AppoitmentDto>> GetDoctorSchedule(int doctorId);
        Task<IEnumerable<AppoitmentDto>> GetPatientAppoitments(int patientId);
        Task<AppoitmentDto> CreateAppoitment(AppoitmentManipulationDto appoitmentDto);
        Task UpdateAppoitment(int id, AppoitmentManipulationDto appoitmentDto);
    }
}
