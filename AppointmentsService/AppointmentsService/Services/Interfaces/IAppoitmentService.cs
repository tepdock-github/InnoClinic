using Appoitments.Domain.DataTransferObjects;

namespace AppointmentsService.Services.Interfaces
{
    public interface IAppoitmentService
    {
        Task<IEnumerable<AppoitmentDto>> GetAppoitments();
        Task<AppoitmentDto?> GetAppoitmentById(int id);
        Task<IEnumerable<AppoitmentDto>> GetDoctorHistory(Guid doctorId);
        Task<IEnumerable<AppoitmentDto>> GetPatientHistory(Guid patientId);
        Task<IEnumerable<AppoitmentDto>> GetDoctorSchedule(Guid doctorId);
        Task<IEnumerable<AppoitmentDto>> GetPatientAppoitments(Guid patientId);
        Task<AppoitmentDto> CreateAppoitment(AppoitmentManipulationDto appoitmentDto);
        Task UpdateAppoitment(int id, AppoitmentManipulationDto appoitmentDto);
    }
}
