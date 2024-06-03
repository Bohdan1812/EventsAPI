namespace Contracts.Event
{
    public record FindEventsRequestModel(string EventSearchQuery, DateTime StartDateTime, DateTime EndDateTime);
}
