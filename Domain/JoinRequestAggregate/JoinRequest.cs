using Domain.Common.Models;
using Domain.EventAggregate;
using Domain.EventAggregate.ValueObjects;
using Domain.JoinRequestAggregate.Exceptions;
using Domain.JoinRequestAggregate.ValueObjects;
using Domain.UserAggregate;
using Domain.UserAggregate.ValueObjects;

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
            var joinRequest = user.JoinRequests.Where(j => j.Id == joinRequestId || 
            j.User == user && j.Event == @event).FirstOrDefault();

            if (joinRequest is not null)
                throw new JoinRequestExistException();

            User = user;
            Event = @event;
        }
    }
}
