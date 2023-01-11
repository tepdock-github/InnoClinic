using Authorization.Domain.Configuration;
using Authorization.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Domain
{
    public class RepositoryContext : IdentityDbContext<Account>
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        protected RepositoryContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
