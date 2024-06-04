namespace Contracts.Event
{
    public record CreateEventRequestModel(
        string Name,
        string? Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        List<SubEventRequest> SubEvents,
        AddressRequest? AddressRequest,
        string? Link,
        bool IsPrivate,
        bool AllowParticipantsInvite);

    public record SubEventRequest(
        string Name,
        string? Description,
        DateTime StartDateTime,
        DateTime EndDateTime);

    public record AddressRequest(
        string AddressName,
        double Longitude,
        double Latitude);
}
