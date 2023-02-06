namespace SharedModels
{
    public interface IDoctorProfileManipulation
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string? MiddleName { get; set; }
    }
}
