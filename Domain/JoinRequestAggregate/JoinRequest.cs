using Domain.Common.Models;
using Domain.EventAggregate;
using Domain.EventAggregate.ValueObjects;
using Domain.JoinRequestAggregate.ValueObjects;
using Domain.UserAggregate;
using Domain.UserAggregate.ValueObjects;
using Domain.JoinRequestAggregate.Exceptions;
using Domain.ParticipationAggregate.Exceptions;
using Domain.InviteAggregate.Exceptions;

namespace Domain.JoinRequestAggregate
{
    public sealed class JoinRequest : AggregateRoot<JoinRequestId>
    {
#pragma warning disable CS8618
        private JoinRequest()
        {

        }
#pragma warning restore CS8618
        public UserId UserId { get; } = null!;

        public User User { get; } = null!;

        public EventId EventId { get; } = null!;

        public Event Event { get; } = null!;

        public JoinRequest(JoinRequestId joinRequestId, User user, Event @event)
            : base(joinRequestId)
        {
            if (@event.Invites.FirstOrDefault(i => i.UserId == user.Id) is not null)
                throw new InviteExistException();

            if (@event.Participations.FirstOrDefault(p => p.UserId == user.Id &&
                p.EventId == @event.Id) is not null)
                throw new ParticipationExistException();

            var joinRequest = user.JoinRequests.Where(j => j.Id == joinRequestId || 
            j.User == user && j.Event == @event).FirstOrDefault();

            if (joinRequest is not null)
                throw new JoinRequestExistException();

            User = user;
            Event = @event;
        }
    }
}
