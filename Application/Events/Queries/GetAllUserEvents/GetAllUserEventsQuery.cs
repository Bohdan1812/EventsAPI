using Domain.EventAggregate;
using ErrorOr;
using MediatR;

namespace Application.Events.Queries.GetAllUserEvents
{
    public record GetAllUserEventsQuery(
        Guid ApplicationUserId,
        DateTime StartDateTime) : IRequest<ErrorOr<List<Event>>>;
}
