using Domain.OrganizerAggregate;
using Domain.UserAggregate;
using Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureUserTable(builder);
            ConfigureApplicationUser(builder);
            ConfigureJoinRequests(builder);
            ConfigureInvites(builder);
            ConfigureParticipations(builder);
            ConfigureOrganizer(builder);
        }

        private static void ConfigureUserTable(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value));
        }

        private static void ConfigureJoinRequests(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.JoinRequests)
                            .WithOne(j => j.User)
                            .HasForeignKey(j => j.UserId)
                            .OnDelete(DeleteBehavior.Cascade);
        }
        private static void ConfigureInvites(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.Invites)
                            .WithOne(j => j.User)
                            .HasForeignKey(j => j.UserId)
                            .OnDelete(DeleteBehavior.Cascade);
        }

        private static void ConfigureParticipations(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.Participations)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void ConfigureApplicationUser(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.ApplicationUser)
                .WithOne(a => a.User)
                .HasForeignKey<User>(u => u.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void ConfigureOrganizer(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.Organizer)
                .WithOne(o => o.User)
                .HasForeignKey<Organizer>(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
    
}
