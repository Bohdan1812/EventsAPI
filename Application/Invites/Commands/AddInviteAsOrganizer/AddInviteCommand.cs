using ErrorOr;
using MediatR;

namespace Application.Invites.Commands.AddInvite
{
    public record AddInviteCommand(
        Guid ApplicationUserId, 
        Guid EventId, 
        Guid UserId) : IRequest<ErrorOr<string>>;
}
