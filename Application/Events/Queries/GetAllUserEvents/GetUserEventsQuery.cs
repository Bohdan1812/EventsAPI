using Domain.EventAggregate;
using ErrorOr;
using MediatR;
using System.Reflection;

namespace Application.Events.Queries.GetAllUserEvents
{
    public record GetUserEventsQuery(
        Guid ApplicationUserId,
        DateTime StartDateTime) : IRequest<ErrorOr<List<Event>>>;
}
