using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfilesService.Domain.Entities;

namespace ProfilesService.Domain.Configuration
{
    public class ReceptionistProfileConfiguration : IEntityTypeConfiguration<ReceptionistProfile>
    {
        public void Configure(EntityTypeBuilder<ReceptionistProfile> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
