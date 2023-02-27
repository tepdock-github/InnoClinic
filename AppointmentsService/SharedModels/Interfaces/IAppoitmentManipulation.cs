namespace SharedModels.Interfaces
{
    public interface IAppoitmentManipulation
    {
        int PatientId { get; set; }
        string PatientFirstName { get; set; }
        string PatientLastName { get; set; }
        int DoctorId { get; set; }
        string DoctorFirstName { get; set; }
        string DoctorLastName { get; set; }
        int ServiceId { get; set; }
        string ServiceName { get; set; }
    }
}
