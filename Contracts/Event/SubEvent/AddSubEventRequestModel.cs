namespace Contracts.Event.SubEvent
{
    public record AddSubEventRequestModel(
        Guid EventId,
        string Name,
        string? Description,
        DateTime StartDateTime,
        DateTime EndDateTime);
}
