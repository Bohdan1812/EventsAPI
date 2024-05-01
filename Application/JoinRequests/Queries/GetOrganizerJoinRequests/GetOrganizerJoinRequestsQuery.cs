using Domain.JoinRequestAggregate;
using ErrorOr;
using MediatR;

namespace Application.JoinRequests.Queries.GetOrganizerJoinRequests
{
    public record GetOrganizerJoinRequestsQuery (Guid ApplicationUserId) 
        : IRequest<ErrorOr<List<JoinRequest>>>;
   
}
