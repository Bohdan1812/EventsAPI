using Domain.JoinRequestAggregate;
using ErrorOr;
using MediatR;

namespace Application.JoinRequests.Queries.GetEventJoinRequests
{
    public record GetEventJoinRequestsQuery(Guid ApplicatioUserId, Guid EventId) 
        : IRequest<ErrorOr<List<JoinRequest>>>;
}
