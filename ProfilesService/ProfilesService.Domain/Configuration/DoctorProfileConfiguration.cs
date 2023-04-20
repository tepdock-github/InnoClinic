using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfilesService.Domain.Entities;

namespace ProfilesService.Domain.Configuration
{
    public class DoctorProfileConfiguration : IEntityTypeConfiguration<DoctorsProfile>
    {
        public void Configure(EntityTypeBuilder<DoctorsProfile> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(
                new DoctorsProfile
                {
                    Id = 997,
                    FirstName = "Hello",
                    MiddleName = "Hello",
                    LastName = "Hello",
                    DateOfBirth = new DateOnly().Year.ToString(),
                    CareerStartYear = new DateOnly().Year.ToString(),
                    Status = "Remote",
                    SpecializationId = 3,
                    SpecializationName = "Hello",
                    AccountId = "",
                    OfficeId= Guid.NewGuid().ToString(),
                },
                new DoctorsProfile
                {
                    Id = 998,
                    FirstName = "Bye",
                    MiddleName = "Hello",
                    LastName = "Bye",
                    DateOfBirth = new DateOnly().Year.ToString(),
                    CareerStartYear = new DateOnly().Year.ToString(),
                    Status = "At office",
                    SpecializationId = 4,
                    SpecializationName = "spec4",
                    AccountId = "",
                    OfficeId = Guid.NewGuid().ToString()
                },
                new DoctorsProfile
                {
                    Id = 1000,
                    FirstName = "HelloBye",
                    MiddleName = "Hello",
                    LastName = "Bye",
                    DateOfBirth = new DateOnly().Year.ToString(),
                    CareerStartYear = new DateOnly().Year.ToString(),
                    Status = "At office",
                    SpecializationId = 4,
                    SpecializationName = "spec4",
                    AccountId = "",
                    OfficeId = Guid.NewGuid().ToString()
                }
            ); 
        }
    }
}
