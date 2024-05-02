namespace Contracts.Invite
{
    public record GetInviteResponse(
        Guid Id,
        Guid EventId,
        Guid UserId);
}
