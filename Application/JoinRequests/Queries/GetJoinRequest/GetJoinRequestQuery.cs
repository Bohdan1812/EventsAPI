using Domain.JoinRequestAggregate;
using ErrorOr;
using MediatR;

namespace Application.JoinRequests.Queries.GetJoinRequest
{
    public record GetJoinRequestQuery(
        Guid ApplicationUserID, 
        Guid JoinRequestId) : IRequest<ErrorOr<JoinRequest>>;
}
