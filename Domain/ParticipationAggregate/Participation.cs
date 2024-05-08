using Domain.ChatAggregate.Entities;
using Domain.Common.Models;
using Domain.EventAggregate;
using Domain.EventAggregate.ValueObjects;
using Domain.InviteAggregate;
using Domain.JoinRequestAggregate;
using Domain.OrganizerAggregate;
using Domain.OrganizerAggregate.ValueObjects;
using Domain.ParticipationAggregate.Exceptions;
using Domain.ParticipationAggregate.ValueObjects;
using Domain.UserAggregate;
using Domain.UserAggregate.ValueObjects;

namespace Domain.ParticipationAggregate
{
    public sealed class Participation : AggregateRoot<ParticipationId>
    {
#pragma warning disable CS8618
        private Participation()
        {

        }
#pragma warning restore CS8618
        private readonly List<Message> _messages = [];
        public UserId UserId { get; } = null!;
        public User User { get; } = null!;
        public EventId EventId { get; } = null!;
        public Event Event { get; } = null!;
        public IReadOnlyList<Message> Messages => _messages.AsReadOnly();

        public Participation(
            JoinRequest joinRequest,
            OrganizerId organizerId)
            : base(ParticipationId.CreateUnique())
        {
            if (joinRequest.Event.OrganizerId != organizerId)
                throw new ParticipationNoPersmissionException();

            User = joinRequest.User;
            Event = joinRequest.Event;
        }

        public Participation(
            Invite invite,
            UserId userId)
            : base(ParticipationId.CreateUnique())
        {
            if (invite.UserId != userId)
                throw new ParticipationNoPersmissionException();

            User = invite.User;
            Event = invite.Event;
        }
        public Participation(
            Organizer organizer,
            User user,
            Event @event) : base(ParticipationId.CreateUnique())
        {
            if (@event.OrganizerId != organizer.Id)
                throw new ParticipationNoPersmissionException();

            if (organizer.UserId != user.Id)
                throw new ParticipationNoPersmissionException();

            User = user;
            Event = @event;
        }
    }
}
