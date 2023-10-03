using Appoitments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appoitments.Domain.Configuration
{
    public class AppoitmentConfiguration : IEntityTypeConfiguration<Appoitment>
    {
        public void Configure(EntityTypeBuilder<Appoitment> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(a => a.Result)
                .WithOne(r => r.Appoitment)
                .HasForeignKey<Result>(r => r.AppoitmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
