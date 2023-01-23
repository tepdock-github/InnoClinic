using Appoitments.Domain.Configuration;
using Appoitments.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appoitments.Domain
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }
        public RepositoryContext() { }

        public DbSet<Appoitment> Appoitments { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AppoitmentConfiguration());
            modelBuilder.ApplyConfiguration(new ResultConfiguration());
        }
    }
}
