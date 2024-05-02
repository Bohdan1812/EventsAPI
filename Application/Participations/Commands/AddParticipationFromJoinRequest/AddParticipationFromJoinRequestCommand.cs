using ErrorOr;
using MediatR;

namespace Application.Participations.Commands.AddParticipationFromJoinRequest
{
    public record AddParticipationFromJoinRequestCommand(
        Guid ApplicationUserId, 
        Guid JoinRequestId) : IRequest<ErrorOr<string>>;
}
