using ErrorOr;
using MediatR;

namespace Application.Invites.Commands.DeleteOwnInvite
{
    public record RemoveOwnInviteCommand(Guid ApplicatonUserId, Guid InviteId) : IRequest<ErrorOr<string>>;
}
