using Authorization.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Domain.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);

            //builder
            //    .HasOne(a => a.Doctor)
            //    .WithOne(d => d.Account)
            //    .HasForeignKey<Doctor>(d => d.AccountId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder
            //    .HasOne(a => a.Patient)
            //    .WithOne(p => p.Account)
            //    .HasForeignKey<Patient>(p => p.AccountId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder
            //    .HasOne(a => a.Photo)
            //    .WithOne(p => p.Account)
            //    .HasForeignKey<Photo>(p => p.AccountId)
            //    .OnDelete(DeleteBehavior.Cascade);*/

            //builder
            //    .HasOne(a => a.Receptionist)
            //    .WithOne(r => r.Account)
            //    .HasForeignKey<Receptionist>(r => r.AccountId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
