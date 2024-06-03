using ErrorOr;
using MediatR;

namespace Application.Invites.Commands.AddInviteAsParticipation
{
    public record AddInviteAsParticipationCommand(
        Guid ApplicationUserId,
        Guid EventId,
        Guid UserId) : IRequest<ErrorOr<string>>;
}
