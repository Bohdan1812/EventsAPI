using Application.Events.Dto;
using Domain.EventAggregate;
using ErrorOr;
using MediatR;

namespace Application.Events.Queries.FindEvents
{
    public record FindEventsQuery(string EventSearchQuery, DateTime StartDateTime, DateTime EndDateTime) : IRequest<ErrorOr<List<Event>>>;
}
