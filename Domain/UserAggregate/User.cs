﻿using Domain.Common.Models;
using Domain.InviteAggregate;
using Domain.JoinRequestAggregate;
using Domain.ParticipationAggregate;
using Domain.UserAggregate.ValueObjects;

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
 
        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }

        public Guid ApplicationUserId { get; set; } 
        public ApplicationUser ApplicationUser { get; set; }

        private readonly List<Invite> _invites = new List<Invite>();
        public IReadOnlyList<Invite> Invites => _invites.AsReadOnly();

        private List<JoinRequest> _joinRequests = new List<JoinRequest>();
        public IReadOnlyList<JoinRequest> JoinRequests => _joinRequests.AsReadOnly();

        private readonly List<Participation> _participations = new List<Participation>();
        public IReadOnlyList<Participation> Participations => _participations.AsReadOnly();

        public User(
            UserId userId,
            string firstName,
            string lastName,
            ApplicationUser appUser
        )
            : base(userId)
        {
            FirstName = firstName;
            LastName = lastName;
            CreatedDateTime = DateTime.UtcNow;
            UpdatedDateTime = DateTime.UtcNow;
            ApplicationUser = appUser;
        }

        /*public static User Create(string firsName,
            string lastName,
            string email,
            string password)
        {
            return new(
                UserId.CreateUnique(),
                firsName,
                lastName,
                email,
                password,
                DateTime.UtcNow,
                DateTime.UtcNow
            );
        }*/

    }
}
