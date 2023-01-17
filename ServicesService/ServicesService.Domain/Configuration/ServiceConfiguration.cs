using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicesService.Domain.Entities;

namespace ServicesService.Domain.Configuration
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(s => s.ServiceCategory)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.CategoryId);

            builder
                .HasOne(s => s.Specialization)
                .WithMany(s => s.Services)
                .HasForeignKey(s => s.SpecializationId);
        }
    }
}
