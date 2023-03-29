namespace SharedModelsInnoClinic
{
    public interface ISpecializationManipulation
    {
        int Id { get; set; }
        string SpecializationName { get; set; }
        bool IsActive { get; set; }
    }
}
