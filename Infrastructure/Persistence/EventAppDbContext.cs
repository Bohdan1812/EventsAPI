using Domain.ChatAggregate;
using Domain.ChatAggregate.Entities;
using Domain.Common.Models;
using Domain.EventAggregate;
using Domain.InviteAggregate;
using Domain.JoinRequestAggregate;
using Domain.OrganizerAggregate;
using Domain.ParticipationAggregate;
using Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class EventAppDbContext: IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public EventAppDbContext(DbContextOptions<EventAppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfigurationsFromAssembly(typeof(EventAppDbContext).Assembly);

            base.OnModelCreating(builder);
        }

        public DbSet<User> DomainUsers { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<JoinRequest> JoinRequests { get; set; }
        public DbSet<Invite> Invites { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Message> Messages { get; set; }

        
    }
}
