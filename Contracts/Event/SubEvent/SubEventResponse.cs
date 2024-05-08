using Domain.EventAggregate.ValueObjects;

namespace Contracts.Event.SubEvent
{
    public record SubEventResponse(
        Guid SubEventId,
        string Name,
        string? Description,
        DateTime StartDateTime,
        DateTime EndDateTime);
}
