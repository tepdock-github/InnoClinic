namespace SharedModelsInnoClinic
{
    public interface IDoctorProfileManipulation
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string? MiddleName { get; set; }
    }
}
