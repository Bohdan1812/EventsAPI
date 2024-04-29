using Domain.JoinRequestAggregate;
using ErrorOr;
using MediatR;

namespace Application.JoinRequests.Queries.GetOwnJoinRequests
{
    public record GetOwnJoinRequestsQuery(Guid ApplicationUserId)
        :IRequest<ErrorOr<List<JoinRequest>>>;
}
