namespace Contracts.JoinRequest
{
    public record GetOwnJoinRequestsResponse(List<JoinRequestResponse> ownJoinRequestResponses);
    

    public record JoinRequestResponse(Guid JoinRequestId, Guid UserId, Guid EventId);
}
