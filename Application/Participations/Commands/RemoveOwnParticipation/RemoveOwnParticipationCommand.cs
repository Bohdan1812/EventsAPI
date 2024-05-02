using ErrorOr;
using MediatR;

namespace Application.Participations.Commands.RemoveOwnParticipation
{
    public record RemoveOwnParticipationCommand(
        Guid ApplicatioUserId, 
        Guid ParticipationId) : IRequest<ErrorOr<string>>;
}
