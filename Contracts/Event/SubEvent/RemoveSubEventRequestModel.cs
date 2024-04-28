namespace Contracts.Event.SubEvent
{
    public record RemoveSubEventRequestModel(
        Guid EventId,
        Guid SubEventId);
}
