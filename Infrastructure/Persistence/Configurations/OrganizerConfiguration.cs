using Domain.OrganizerAggregate;
using Domain.OrganizerAggregate.ValueObjects;
using Domain.UserAggregate;
using Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OrganizerConfiguration : IEntityTypeConfiguration<Organizer>
    {
        public void Configure(EntityTypeBuilder<Organizer> builder)
        {
            builder.ToTable("Organizers");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => OrganizerId.Create(value));

            builder.Property(x => x.UserId)
                .ValueGeneratedNever()
                .HasConversion(
                    userId => userId.Value,
                    value => UserId.Create(value));

            builder.OwnsMany(x => x.EventIds, eId =>
            {
                eId.ToTable("OrganizerEventIds");

                eId.WithOwner().HasForeignKey("OrganizerId");

                eId.HasKey("Id");

                eId.Property(e => e.Value)
                .HasColumnName("EventId")
                .ValueGeneratedNever();

            });

            builder.Metadata.FindNavigation(nameof(Organizer.EventIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
