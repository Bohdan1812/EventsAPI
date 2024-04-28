namespace Contracts.Event
{
    public record CreateEventRequestModel(
        string Name,
        string? Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        List<SubEventRequest> SubEvents,
        AddressRequest? AddressRequest,
        string? Link);

    public record SubEventRequest(
        string Name,
        string? Description,
        DateTime StartDateTime,
        DateTime EndDateTime);

    public record AddressRequest(
        string Country,
        string? State,
        string City,
        string? Street,
        string? House);
}
