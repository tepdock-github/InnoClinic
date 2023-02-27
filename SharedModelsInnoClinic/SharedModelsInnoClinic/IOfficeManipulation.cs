namespace SharedModelsInnoClinic
{
    public interface IOfficeManipulation
    {
        string Id { get; set; }
        string Address { get; set; }
        string PhoneNumber { get; set; }
        bool IsActive { get; set; }
    }
}
