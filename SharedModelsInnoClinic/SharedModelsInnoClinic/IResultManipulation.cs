namespace SharedModelsInnoClinic
{
    public interface IResultManipulation
    {
        int Id { get; set; }
        string Complaints { get; set; }
        string Conclusion { get; set; }
        string Recomendations { get; set; }
        string? Diagnosis { get; set; }

        int AppoitmentId { get; set; }
    }
}
