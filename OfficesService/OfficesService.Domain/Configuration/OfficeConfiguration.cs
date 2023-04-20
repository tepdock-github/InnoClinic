using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficesService.Domain.Models;

namespace OfficesService.Domain.Configuration
{
    public class OfficeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(
                new Office 
                {
                    Id = "023018cf-e0ff-4f20-8192-700520ab36ff",
                    IsActive = true,
                    Address = "Gukova 29",
                    PhoneNumber = "1234567890",

                }
            );
        }
    }
}
