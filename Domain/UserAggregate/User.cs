using Domain.Common.Models;
using Domain.InviteAggregate;
using Domain.JoinRequestAggregate;
using Domain.OrganizerAggregate;
using Domain.ParticipationAggregate;
using Domain.UserAggregate.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.UserAggregate
{
    public class User: AggregateRoot<UserId>
    {
#pragma warning disable CS8618
        private User()
        {

        }
#pragma warning restore CS8618
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }
        public Guid ApplicationUserId { get; set; } 
        public string? PhotoPath { get; set; }

        private readonly List<Invite> _invites = [];
        public IReadOnlyList<Invite> Invites => _invites.AsReadOnly();

        private readonly List<JoinRequest> _joinRequests = [];
        public IReadOnlyList<JoinRequest> JoinRequests => _joinRequests.AsReadOnly();

        private readonly List<Participation> _participations = [];
        public IReadOnlyList<Participation> Participations => _participations.AsReadOnly();

        public Organizer Organizer { get; private set; } = null!;

        public User(
            string firstName,
            string lastName,
            string email,
            Guid applicationUserId
        )
            : base(UserId.CreateUnique())
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
           
            Organizer = new Organizer(this);
           

            CreatedDateTime = DateTime.UtcNow;
            UpdatedDateTime = DateTime.UtcNow;
            ApplicationUserId = applicationUserId;
        }
    }
}
