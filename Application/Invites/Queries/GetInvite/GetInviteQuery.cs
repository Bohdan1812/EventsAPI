using Domain.InviteAggregate;
using ErrorOr;
using MediatR;

namespace Application.Invites.Queries.GetInvite
{
    public record GetInviteQuery (
        Guid ApplicatioUserId, 
        Guid InviteId) : IRequest<ErrorOr<Invite?>>;
}
