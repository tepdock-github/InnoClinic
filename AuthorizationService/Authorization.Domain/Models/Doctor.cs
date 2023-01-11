using System.ComponentModel.DataAnnotations.Schema;

namespace Authorization.Domain.Models
{
    public class Doctor
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CareerStartYear { get; set; }
        public string Status { get; set; }

        public string AccountId { get; set; }
        public Account Account { get; set; }

       // public ICollection<Appoitment> Appoitments { get; set; }
       // public ICollection<Specialization> Specializations { get; set; }
       // public ICollection<Office> Offices { get; set; }
    }
}
