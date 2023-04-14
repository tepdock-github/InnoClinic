namespace SharedModelsInnoClinic
{
    public interface IPatientProfileManipulation
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string? MiddleName { get; set; }
        bool IsLinkedToAccount { get; set; }
        int AccountId { get; set; }
    }
}
