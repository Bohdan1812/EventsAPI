using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.EventAggregate.Entities;
using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Events.Commands.SubEventCommands.AddSubEvent
{
    public class AddSubEventCommandHandler : IRequestHandler<AddSubEventCommand, ErrorOr<string>>
    {
        IEventRepository _eventRepository;

        IOrganizerRepository _organizerRepository;

        public AddSubEventCommandHandler(IEventRepository eventRepository, IOrganizerRepository organizerRepository)
        {
            _eventRepository = eventRepository;
            _organizerRepository = organizerRepository;
        }

        public async Task<ErrorOr<string>> Handle(AddSubEventCommand request, CancellationToken cancellationToken)
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

            if (request.Description is not null)
                description = request.Description;

            SubEvent newSubEvent = null;

            try
            {
                newSubEvent = new SubEvent(
                    request.Name,
                    description,
                    request.StartDateTime,
                    request.EndDateTime);
            }
            catch (Exception ex)
            {
                return EventError.SubEventError.SubEventNotInitialized(ex.Message);
            }

            try
            {
                @event.AddSubEvent(newSubEvent);
                await _eventRepository.Update(@event);
            }
            catch (Exception ex)
            {
                return EventError.SubEventError.SubEventNotAdded(ex.Message);
            }

            @event = await _eventRepository.GetEvent(eventId);

                if (@event is not null && 
                    @event.SubEvents.Contains(newSubEvent))
                    return "SubEvent added Successfully!";

            return EventError.SubEventError.SubEventNotAddedDb;
        }
    }
}
