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

            builder.HasData
                (
                    new Appoitment
                    {
                        Id = Guid.NewGuid().ToString(),
                        PatientId = "123",
                        DoctorId = "123",
                        ServiceId = "123",
                        Date = "13.01.2023",
                        Time = "10am",
                        isApproved= true,
                        isComplete= false
                    },
                    new Appoitment
                    {
                        Id = Guid.NewGuid().ToString(),
                        PatientId = "123",
                        DoctorId = "204",
                        ServiceId = "123",
                        Date = "12.01.2023",
                        Time = "12am",
                        isApproved = true,
                        isComplete = true
                    },
                    new Appoitment
                    {
                        Id = Guid.NewGuid().ToString(),
                        PatientId = "204",
                        DoctorId = "123",
                        ServiceId = "123",
                        Date = "13.01.2023",
                        Time = "1pm",
                        isApproved = false,
                        isComplete = false
                    }
                );
        }
    }
}
