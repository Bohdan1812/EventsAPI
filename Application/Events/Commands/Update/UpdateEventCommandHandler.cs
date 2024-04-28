using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.EventAggregate;
using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Events.Commands.Update
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, ErrorOr<string>>
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IEventRepository _eventRepository;

        public UpdateEventCommandHandler(IOrganizerRepository organizerRepository, IEventRepository eventRepository)
        {
            _organizerRepository = organizerRepository;
            _eventRepository = eventRepository;
        }

        public async Task<ErrorOr<string>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var organizer = await _organizerRepository.GetOrganizer(request.ApplicationUserId);
            
            if (organizer is null)
                return OrganizerError.OrganizerNotFound;

            var eventId = EventId.Create(request.EventId);

            var @event = await _eventRepository.GetEvent(eventId);

            if (@event is null)
                return EventError.EventNotFound;

            if (@event.OrganizerId != organizer.Id)
                return EventError.EventUpdateNoPermission;

            string description = "";
            Address? address = null;
            Link? link = null;

            if (request.Description is not null)
                description = request.Description;

            if (request.Address is not null)
                address = new Address(
                    request.Address.House,
                    request.Address.Street,
                    request.Address.City,
                    request.Address.State,
                    request.Address.Country);

            if (request.Link is not null)
                link = new Link(request.Link.Link);

            try
            {
                @event.Update(request.Name,
                    description,
                    request.StartDateTime,
                    request.EndDateTime,
                    address,
                    link);

                await _eventRepository.Update(@event);
            }
            catch (Exception ex)
            {
                return EventError.EventNotInitialized(ex.Message);
            }
            @event = await _eventRepository.GetEvent(eventId);

            if (@event is not null &&
                @event.Id == eventId &&
                @event.Name == request.Name &&
                @event.Description == description &&
                @event.StartDateTime == request.StartDateTime &&
                @event.EndDateTime == request.EndDateTime &&
                @event.Address == address &&
                @event.Link == link)
                return "Event updated successfully!";

            return EventError.EventNotUpdated;
        }
    }
}
