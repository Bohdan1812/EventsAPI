using EventsAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace EventsAPI.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCreation> EventCreators { get; set; }
        public DbSet<Participation> Participants { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<InvitationSending> InvitationSenders { get; set; }
        public DbSet<InvitationReceiving> InvitationReceivers { get; set; }

       

    }
}
