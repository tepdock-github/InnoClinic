namespace SharedModelsInnoClinic
{
    public interface ICategoryManipulation
    {
        int Id { get; set; }
        string CategoryName { get; set; }
        string TimeSlotSize { get; set; }
    }
}
