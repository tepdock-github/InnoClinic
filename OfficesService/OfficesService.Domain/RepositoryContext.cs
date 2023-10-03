using Microsoft.EntityFrameworkCore;
using OfficesService.Domain.Configuration;
using OfficesService.Domain.Models;

namespace OfficesService.Domain
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        protected RepositoryContext()
        {
        }

        public DbSet<Office> Offices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OfficeConfiguration());
        }
    }
}
