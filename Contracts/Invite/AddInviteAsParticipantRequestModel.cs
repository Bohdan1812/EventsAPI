namespace Contracts.Invite
{
    public record AddInviteAsParticipantRequestModel(
        Guid EventId,
        Guid UserId);
}
