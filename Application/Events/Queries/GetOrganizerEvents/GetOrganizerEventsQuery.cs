using Domain.EventAggregate;
using ErrorOr;
using MediatR;

namespace Application.Events.Queries.GetUserEvents
{
    public record GetOrganizerEventsQuery(Guid appUserId) :IRequest<ErrorOr<List<Event>>>;
}
