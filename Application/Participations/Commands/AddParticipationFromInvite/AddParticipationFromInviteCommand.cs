using ErrorOr;
using MediatR;

namespace Application.Participations.Commands.AddParticipationFromInvite
{
    public record AddParticipationFromInviteCommand(
        Guid ApplicatioUserId,
        Guid InviteId) : IRequest<ErrorOr<string>>;
   
}
