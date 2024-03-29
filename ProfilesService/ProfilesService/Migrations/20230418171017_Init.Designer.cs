﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProfilesService.Domain;

#nullable disable

namespace ProfilesService.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20230418171017_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProfilesService.Domain.Entities.DoctorsProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CareerStartYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateOfBirth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfficeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpecializationId")
                        .HasColumnType("int");

                    b.Property<string>("SpecializationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DoctorsProfiles");

                    b.HasData(
                        new
                        {
                            Id = 997,
                            AccountId = "",
                            CareerStartYear = "1",
                            DateOfBirth = "1",
                            FirstName = "Hello",
                            LastName = "Hello",
                            MiddleName = "Hello",
                            OfficeId = "023018cf-e0ff-4f20-8192-700520ab36ff",
                            SpecializationId = 3,
                            SpecializationName = "Hello",
                            Status = "Remote"
                        },
                        new
                        {
                            Id = 998,
                            AccountId = "",
                            CareerStartYear = "1",
                            DateOfBirth = "1",
                            FirstName = "Bye",
                            LastName = "Bye",
                            MiddleName = "Hello",
                            OfficeId = "ad2cdf15-84e6-4ab2-a8c5-a57cda291346",
                            SpecializationId = 4,
                            SpecializationName = "spec4",
                            Status = "At office"
                        },
                        new
                        {
                            Id = 1000,
                            AccountId = "",
                            CareerStartYear = "1",
                            DateOfBirth = "1",
                            FirstName = "HelloBye",
                            LastName = "Bye",
                            MiddleName = "Hello",
                            OfficeId = "52a7821a-02e5-4e61-8549-4bb266f167fe",
                            SpecializationId = 4,
                            SpecializationName = "spec4",
                            Status = "At office"
                        });
                });

            modelBuilder.Entity("ProfilesService.Domain.Entities.PatientProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateOfBirth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsLinkedToAccount")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PatientProfiles");
                });

            modelBuilder.Entity("ProfilesService.Domain.Entities.ReceptionistProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfficeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ReceptionistProfiles");
                });
#pragma warning restore 612, 618
        }
    }
}
