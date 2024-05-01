using ErrorOr;
using MediatR;

namespace Application.JoinRequests.Commands.RemoveAsOrganizer
{
    public record RemoveOrganizerJoinRequestCommand(
        Guid ApplicationUserId, 
        Guid JoinRequestId) : IRequest<ErrorOr<string>>;
}
