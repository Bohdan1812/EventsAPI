using ErrorOr;
using MediatR;

namespace Application.Invites.Commands.RemoveAsOrganizer
{
    public record class RemoveOrganizerInviteCommand(
        Guid ApplicationUserId, 
        Guid InviteId) : IRequest<ErrorOr<string>>;
}
