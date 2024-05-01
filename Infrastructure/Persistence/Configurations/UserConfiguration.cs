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
            ConnfigureApplicationUser(builder);
            ConfigureJoinRequests(builder);
            ConfigureInvites(builder);
        }

        private void ConfigureUserTable(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value));
        }

        private void ConfigureJoinRequests(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.JoinRequests)
                            .WithOne(j => j.User)
                            .HasForeignKey(j => j.UserId)
                            .OnDelete(DeleteBehavior.Cascade);
        }
        private void ConfigureInvites(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.Invites)
                            .WithOne(j => j.User)
                            .HasForeignKey(j => j.UserId)
                            .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConnfigureApplicationUser(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.ApplicationUser)
                .WithOne(a => a.User)
                .HasForeignKey<User>(u => u.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
    
}
