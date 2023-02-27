﻿// <auto-generated />
using System;
using Appoitments.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppointmentsService.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Appoitments.Domain.Entities.Appoitment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoctorFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("DoctorLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<string>("PatientLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ResultId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("isComplete")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Appoitments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = "20 jan 2022",
                            DoctorFirstName = "Doctor1_testData",
                            DoctorId = 1,
                            DoctorLastName = "Doctor1_testData",
                            PatientFirstName = "Patient1_testData",
                            PatientId = 1,
                            PatientLastName = "Patient1_testData",
                            ResultId = 1,
                            ServiceId = 1,
                            ServiceName = "Service1_testData",
                            Time = "10 am",
                            isApproved = true,
                            isComplete = true
                        },
                        new
                        {
                            Id = 2,
                            Date = "20 jan 2024",
                            DoctorFirstName = "Doctor2_testData",
                            DoctorId = 2,
                            DoctorLastName = "Doctor2_testData",
                            PatientFirstName = "Patient1_testData",
                            PatientId = 1,
                            PatientLastName = "Patient1_testData",
                            ServiceId = 1,
                            ServiceName = "Service1_testData",
                            Time = "10 am",
                            isApproved = false,
                            isComplete = false
                        },
                        new
                        {
                            Id = 3,
                            Date = "21 feb 2023",
                            DoctorFirstName = "Doctor1_testData",
                            DoctorId = 1,
                            DoctorLastName = "Doctor1_testData",
                            PatientFirstName = "Patient2_testData",
                            PatientId = 2,
                            PatientLastName = "Patient2_testData",
                            ServiceId = 1,
                            ServiceName = "Service1_testData",
                            Time = "10 am",
                            isApproved = true,
                            isComplete = false
                        });
                });

            modelBuilder.Entity("Appoitments.Domain.Entities.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppoitmentId")
                        .HasColumnType("int");

                    b.Property<string>("Complaints")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Conclusion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diagnosis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Recomendations")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppoitmentId")
                        .IsUnique();

                    b.ToTable("Results");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AppoitmentId = 1,
                            Complaints = "complains a lot",
                            Conclusion = "conclusion",
                            Recomendations = "pills"
                        });
                });

            modelBuilder.Entity("Appoitments.Domain.Entities.Result", b =>
                {
                    b.HasOne("Appoitments.Domain.Entities.Appoitment", "Appoitment")
                        .WithOne("Result")
                        .HasForeignKey("Appoitments.Domain.Entities.Result", "AppoitmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appoitment");
                });

            modelBuilder.Entity("Appoitments.Domain.Entities.Appoitment", b =>
                {
                    b.Navigation("Result");
                });
#pragma warning restore 612, 618
        }
    }
}
