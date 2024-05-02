using Domain.ParticipationAggregate;
using ErrorOr;
using MediatR;

namespace Application.Participations.Queries.GetParticipationsByEvent
{
    public record GetParticipationsByEventQuery(
        Guid ApplicationUserId,
        Guid EventId) : IRequest<ErrorOr<List<Participation>>>;
}
