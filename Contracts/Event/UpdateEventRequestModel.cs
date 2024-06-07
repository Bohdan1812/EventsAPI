namespace Contracts.Event
{
    public record UpdateEventRequestModel(
        Guid eventId,
        string Name,
        string? Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        AddressRequest? Address,
        string? Link, 
        bool IsPrivate,
        bool AllowParticipantsInvite);
 
}
