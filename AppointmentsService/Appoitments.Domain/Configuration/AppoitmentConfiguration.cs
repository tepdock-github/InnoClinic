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
                        Id = 1,
                        PatientId = "8787a766-7517-4040-9fe3-8416b9326a66",
                        PatientFirstName = "Patient1_testData",
                        PatientLastName = "Patient1_testData",
                        DoctorId = Guid.NewGuid().ToString(),
                        DoctorFirstName = "Doctor1_testData",
                        DoctorLastName = "Doctor1_testData",
                        ServiceId = 1,
                        ServiceName = "Service1_testData",
                        Date = "20 jan 2022",
                        Time = "10 am",
                        isApproved = true,
                        isComplete = true,
                        ResultId = 1

                    },
                    new Appoitment
                    {
                        Id = 2,
                        PatientId = "8787a766-7517-4040-9fe3-8416b9326a66",
                        PatientFirstName = "Patient1_testData",
                        PatientLastName = "Patient1_testData",
                        DoctorId = Guid.NewGuid().ToString(),
                        DoctorFirstName = "Doctor2_testData",
                        DoctorLastName = "Doctor2_testData",
                        ServiceId = 1,
                        ServiceName = "Service1_testData",
                        Date = "20 jan 2024",
                        Time = "10 am",
                        isApproved = false,
                        isComplete = false
                    },
                    new Appoitment
                    {
                        Id = 3,
                        PatientId = "8787a766-7517-4040-9fe3-8416b9326a66",
                        PatientFirstName = "Patient2_testData",
                        PatientLastName = "Patient2_testData",
                        DoctorId = Guid.NewGuid().ToString(),
                        DoctorFirstName = "Doctor1_testData",
                        DoctorLastName = "Doctor1_testData",
                        ServiceId = 1,
                        ServiceName = "Service1_testData",
                        Date = "21 feb 2023",
                        Time = "10 am",
                        isApproved = true,
                        isComplete = false
                    }
                );
        }
    }
}
