using Domain.Common.Models;
using Domain.EventAggregate.ValueObjects;
using Domain.ParticipationAggregate.ValueObjects;
using Domain.UserAggregate.ValueObjects;

namespace Domain.ParticipationAggregate
{
    public sealed class Participation : AggregateRoot<ParticipationId>
    {
        public UserId UserId { get; } = null!;
        
        public EventId EventId { get; } = null!;

        public Participation(ParticipationId participationId, UserId userId, EventId eventId) 
            : base(participationId)
        {
            UserId = userId;
            EventId = eventId;
        }
    }
}
