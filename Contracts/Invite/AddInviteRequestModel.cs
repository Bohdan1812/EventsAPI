namespace Contracts.Invite
{
    public record AddInviteRequestModel (
        Guid EventId, 
        Guid UserId);
}
