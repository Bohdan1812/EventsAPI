using Domain.InviteAggregate;
using ErrorOr;
using MediatR;

namespace Application.Invites.Queries.GetUserInvites
{
    public record GetUserInvitesQuery(
        Guid ApplicatioUserId) : IRequest<ErrorOr<List<Invite>>>;

}
