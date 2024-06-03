namespace Contracts.Event
{
    public record UpdateEventRequestModel(
        Guid eventId,
        string Name,
        string? Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        AddressRequest? AddressRequest,
        string? Link, 
        bool IsPrivate,
        bool AllowParticipantsInvite);
 
}
