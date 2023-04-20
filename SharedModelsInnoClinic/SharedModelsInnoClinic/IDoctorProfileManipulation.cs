namespace SharedModelsInnoClinic
{
    public interface IDoctorProfileManipulation
    {
        string Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string? MiddleName { get; set; }
    }
}
