using Domain.Common.Models;
using Domain.EventAggregate.ValueObjects;
using Domain.ParticipationRequestAggregate.ValueObjects;
using Domain.UserAggregate.ValueObjects;

namespace Domain.ParticipationRequestAggregate
{
    public sealed class ParticipationRequest : AggregateRoot<ParticipationRequestId>
    {
        public UserId UserId { get; } = null!;

        public EventId EventId { get; } = null!;

        public ParticipationRequest(ParticipationRequestId participationRequestId, UserId userId, EventId eventId)
            : base(participationRequestId)
        {
            UserId = userId;
            EventId = eventId;
        }
           

    }
}
