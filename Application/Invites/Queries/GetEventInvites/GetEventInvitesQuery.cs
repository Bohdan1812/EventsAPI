using Domain.InviteAggregate;
using ErrorOr;
using MediatR;

namespace Application.Invites.Queries.GetEventInvites
{
    public record GetEventInvitesQuery(
        Guid ApplicationUserId,
        Guid EventId) : IRequest<ErrorOr<List<Invite>>>;
}
