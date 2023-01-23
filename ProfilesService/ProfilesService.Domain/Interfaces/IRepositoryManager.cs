namespace ProfilesService.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        IReceptionistProfileRepository ReceptionistProfile { get; }
        IPatientProfileRepository PatientProfile { get; }
        IDoctorProfileRepository DoctorProfile { get; }
        Task SaveAsync();
    }
}
