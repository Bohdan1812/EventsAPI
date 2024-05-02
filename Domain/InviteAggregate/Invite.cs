using Domain.Common.Models;
using Domain.EventAggregate;
using Domain.EventAggregate.ValueObjects;
using Domain.InviteAggregate.Exceptions;
using Domain.InviteAggregate.ValueObjects;
using Domain.JoinRequestAggregate.Exceptions;
using Domain.OrganizerAggregate.ValueObjects;
using Domain.ParticipationAggregate.Exceptions;
using Domain.UserAggregate;
using Domain.UserAggregate.ValueObjects;

namespace Domain.InviteAggregate
{
    public sealed class Invite : AggregateRoot<InviteId>
    {
#pragma warning disable CS8618
        private Invite()
        {

        }
#pragma warning restore CS8618
        public UserId UserId { get; } = null!;

        public User User { get; } = null!;

        public EventId EventId { get; } = null!;

        public Event Event { get; } = null!;

        public Invite(
            InviteId inviteId,
            OrganizerId organizerId,
            User user, 
            Event @event)
            :base(inviteId)
        {
            if (@event.OrganizerId != organizerId)
                throw new InviteNoPersmissionException();

            if (@event.Participations
                .FirstOrDefault(p => p.UserId == user.Id &&
                    p.EventId == @event.Id) is not null)
                throw new ParticipationExistException();

            if (@event.Invites.
                FirstOrDefault(i => i.UserId == user.Id) is not null)
                throw new InviteExistException();

            if (user.JoinRequests
                .FirstOrDefault(j => j.EventId == @event.Id) is not null)
                throw new JoinRequestExistException();

            User = user;
            Event = @event;
        }
    }
}
