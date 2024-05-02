namespace Contracts.Participation
{
    public record ParticipationResponse(
        Guid Id,
        Guid UserId,
        Guid EventId);
}
