using Domain.Common.Models;
using Domain.EventAggregate.ValueObjects;
using Domain.InviteAggregate.ValueObjects;
using Domain.UserAggregate.ValueObjects;

namespace Domain.InviteAggregate
{
    public sealed class Invite : AggregateRoot<InviteId>
    {
        public UserId UserId { get; } = null!;

        public EventId EventId { get; } = null!;

        public Invite(InviteId inviteId, UserId userId, EventId eventId)
            :base(inviteId)
        {
            UserId = userId;
            EventId = eventId;
        }
    }
}
