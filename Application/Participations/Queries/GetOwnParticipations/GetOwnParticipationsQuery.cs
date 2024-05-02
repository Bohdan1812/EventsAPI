using Domain.ParticipationAggregate;
using ErrorOr;
using MediatR;

namespace Application.Participations.Queries.GetOwnParticipations
{
    public record GetOwnParticipationsQuery(
        Guid ApplicationUserId) : IRequest<ErrorOr<List<Participation>>>;
}
