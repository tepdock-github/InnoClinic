namespace SharedModelsInnoClinic
{
    public interface IServiceManipulation
    {
        int Id { get; set; }
        string ServiceName { get; set; }
        float Price { get; set; }
        bool IsActive { get; set; }

        int CategoryId { get; set; }

        int SpecializationId { get; set; }
    }
}
