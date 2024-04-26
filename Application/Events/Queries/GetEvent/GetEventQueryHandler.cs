using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.EventAggregate;
using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Events.Queries.GetEvent
{
    public class GetEventQueryHandler : IRequestHandler<GetEventQuery, ErrorOr<Event>>
    {
        private readonly IEventRepository _eventRepository;
        public GetEventQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<ErrorOr<Event>> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetEvent(EventId.Create(request.EventId));

            if (@event is null)
                return EventError.EventNotFound;

            return @event;
        }



    }
}
