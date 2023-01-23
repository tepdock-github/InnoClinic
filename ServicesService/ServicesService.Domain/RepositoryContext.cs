using Microsoft.EntityFrameworkCore;
using ServicesService.Domain.Configuration;
using ServicesService.Domain.Entities;

namespace ServicesService.Domain
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }
        public RepositoryContext() { }

        public DbSet<Service> Services { get; set; }
        public DbSet<Category> ServiceCategories { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new SpecializationConfiguration());
        }
    }
}
