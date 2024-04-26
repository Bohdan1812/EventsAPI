using Domain.EventAggregate;
using ErrorOr;
using MediatR;

namespace Application.Events.Queries.GetEvent
{
   public record GetEventQuery(Guid EventId): IRequest<ErrorOr<Event>>;
}
