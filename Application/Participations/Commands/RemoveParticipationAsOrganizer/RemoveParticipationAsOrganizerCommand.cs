using ErrorOr;
using MediatR;

namespace Application.Participations.Commands.RemoveParticipationAsOrganizer
{
    public record RemoveParticipationAsOrganizerCommand(
        Guid ApplicationUserId,
        Guid ParticipationId) : IRequest<ErrorOr<string>>;
}
