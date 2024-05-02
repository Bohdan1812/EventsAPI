using Domain.ParticipationAggregate;
using ErrorOr;
using MediatR;

namespace Application.Participations.Queries.GetParticipation
{
    public record GetParticipationQuery(
        Guid ApplicationUserId, 
        Guid ParticipationId) : IRequest<ErrorOr<Participation>>;
}
