using Domain.EventAggregate;
using Domain.EventAggregate.ValueObjects;
using Domain.OrganizerAggregate;
using Domain.OrganizerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            ConfigureEventsTable(builder);
            ConfigureSubEvents(builder);
            ConfigureJoinRequests(builder);
            ConfigureInvites(builder);
        }

        private void ConfigureEventsTable(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => EventId.Create(value));

            builder.Property(e => e.Link)
                .ValueGeneratedNever()
                .HasConversion(
                    link => link.Value,
                    value => new Link(value));

            builder.Property(e => e.OrganizerId)
                .ValueGeneratedNever()
                .HasConversion(
                organizerId => organizerId.Value,
                value => OrganizerId.Create(value));

            builder.OwnsOne(e => e.Address, address =>
            {
                address.Property(a => a.Country)
                    .HasColumnName("Country");
                address.Property(a => a.State)
                    .HasColumnName("State");
                address.Property(a => a.City)
                    .HasColumnName("City");
                address.Property(a => a.Street)
                    .HasColumnName("Street");
                address.Property(a => a.House)
                    .HasColumnName("House");
            });
        }
        private void ConfigureSubEvents(EntityTypeBuilder<Event> builder)
        {
            builder.OwnsMany(m => m.SubEvents, se =>
            {
                se.ToTable("SubEvents");

                se.WithOwner().HasForeignKey("EventId");

                se.HasKey("Id", "EventId");

                se.Property(s => s.Id)
                    .HasColumnName("SubEventId")
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => SubEventId.Create(value));
            });

            builder.Metadata.FindNavigation(nameof(Event.SubEvents))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
        private void ConfigureJoinRequests(EntityTypeBuilder<Event> builder)
        {
            builder.HasMany(e => e.JoinRequests)
                .WithOne(j => j.Event)
                .HasForeignKey(j => j.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        private void ConfigureInvites(EntityTypeBuilder<Event> builder)
        {
            builder.HasMany(e => e.Invites)
                .WithOne(i => i.Event)
                .HasForeignKey(i => i.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
