using Microsoft.EntityFrameworkCore;
using ProfilesService.Domain.Configuration;
using ProfilesService.Domain.Entities;

namespace ProfilesService.Domain
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }

        protected RepositoryContext() { }

        public DbSet<DoctorsProfile> DoctorsProfiles { get; set; }
        public DbSet<PatientProfile> PatientProfiles { get; set; }
        public DbSet<ReceptionistProfile> ReceptionistProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DoctorProfileConfiguration());
            modelBuilder.ApplyConfiguration(new PatientProfileConfiguration());
            modelBuilder.ApplyConfiguration(new ReceptionistProfileConfiguration());
        }
    }
}
