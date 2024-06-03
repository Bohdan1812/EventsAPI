using Application.Events.Dto;
using Application.Persistence.Repositories;
using Domain.EventAggregate;
using ErrorOr;
using Mapster;
using MediatR;

namespace Application.Events.Queries.FindEvents
{
    public class FindEventsQueryHandler : IRequestHandler<FindEventsQuery, ErrorOr<List<Event>>>
    {
        private readonly IEventRepository _eventRepository;

        public FindEventsQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<ErrorOr<List<Event>>> Handle(FindEventsQuery request, CancellationToken cancellationToken)
        {
            return await _eventRepository.FindEvents(request.EventSearchQuery, request.StartDateTime, request.EndDateTime);
        }
    }
}
