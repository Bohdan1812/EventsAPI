namespace Contracts.Event.SubEvent
{
    public record UpdateSubEventRequestModel(
        Guid EventId,
        Guid SubEventId,
        string Name,
        string? Description,
        DateTime StartDateTime,
        DateTime EndDateTime);
}
