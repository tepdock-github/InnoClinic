namespace ProfilesService.Domain.RequestFeatures
{
    public class DoctorParameters 
    {
        public string SearchTerm { get; set; }

        public int Specialization { get; set; }
        public int Office { get; set; }

    }
}
