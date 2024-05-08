using Contracts.Event.SubEvent;
using Domain.EventAggregate.ValueObjects;

namespace Contracts.Event
{
    public record EventResponse(
        Guid Id,
        string Name,
        string Description,
        Guid OrganizerId,
        List<SubEventResponse> SubEvents,
        AddressResponse Address,
        LinkResponse Link,
        DateTime StartDateTime,
        DateTime EndDateTime);
}
