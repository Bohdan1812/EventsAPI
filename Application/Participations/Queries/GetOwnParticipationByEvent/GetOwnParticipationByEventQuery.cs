using Domain.ParticipationAggregate;
using ErrorOr;
using MediatR;

namespace Application.Participations.Queries.GetOwnParticipationByEvent
{
    public record GetOwnParticipationByEventQuery(Guid ApplicationUserId, Guid EventId) : IRequest<ErrorOr<Participation?>> ;
}
