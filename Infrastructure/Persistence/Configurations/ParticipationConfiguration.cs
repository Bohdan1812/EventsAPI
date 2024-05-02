using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.ParticipationAggregate;
using Domain.ParticipationAggregate.ValueObjects;

namespace Infrastructure.Persistence.Configurations
{
    public class ParticipationConfiguration : IEntityTypeConfiguration<Participation>
    {
        public void Configure(EntityTypeBuilder<Participation> builder)
        {
            builder.ToTable("Participations");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ParticipationId.Create(value));
        }
    }
}

