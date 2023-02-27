using Appoitments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appoitments.Domain.Configuration
{
    public class ResultConfiguration : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData
                (
                    new Result
                    {
                        Id = 1,
                        Complaints = "complains a lot",
                        Conclusion = "conclusion",
                        Recomendations = "pills",
                        AppoitmentId = 1
                    }
                );
        }
    }
}
