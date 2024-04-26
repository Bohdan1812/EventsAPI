using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.EventAggregate;
using Domain.EventAggregate.Entities;
using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Events.Commands.Create
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, ErrorOr<string>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IOrganizerRepository _organizerRepository;

        public CreateEventCommandHandler(IEventRepository eventRepository, IOrganizerRepository organizerRepository)
        {
            _eventRepository = eventRepository;
            _organizerRepository = organizerRepository;
        }

        public async Task<ErrorOr<string>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var organizer = await _organizerRepository.GetOrganizer(request.appUserId);
            if (organizer is null)
            {
                return OrganizerError.OrganizerNotFound;
            }

            Address? address = null;

            if (request.Address is not null)
            {
                address = new Address(
                    request.Address.House,
                    request.Address.Street,
                    request.Address.City,
                    request.Address.State,
                    request.Address.Country);
            }

            Link? link = null;

            if(request.Link is not null)
            {
                link = new Link(request.Link.Link);
            }

            Event? newEvent = null;

            try
            {
                newEvent = new Event(
                EventId.CreateUnique(),
                request.Name,
                request.Description,
                organizer.Id,
                request.StartDateTime,
                request.EndDateTime,
                request.SubEvents.ConvertAll(subEvent => new SubEvent(
                    SubEventId.CreateUnique(),
                    subEvent.Name,
                    subEvent.Description,
                    subEvent.StartDateTime,
                    subEvent.EndDateTime)),
                address,
                link);
            }

            catch (Exception ex)
            {
                return EventError.EventNotInitialized(ex.Message);
            }

            if (newEvent is not null)
            {
                await _eventRepository.Add(newEvent);
                return "Event created Successfully!";
            }

            return EventError.EventNotAdded;
        }
    }
}
