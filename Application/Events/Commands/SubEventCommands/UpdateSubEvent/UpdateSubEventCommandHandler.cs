using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Events.Commands.SubEventCommands.UpdateSubEvent
{
    public class UpdateSubEventCommandHandler : IRequestHandler<UpdateSubEventCommand, ErrorOr<string>>
    {
        IOrganizerRepository _organizerRepository;
        IEventRepository _eventRepository;

        public UpdateSubEventCommandHandler(IOrganizerRepository organizerRepository, IEventRepository eventRepository)
        {
            _organizerRepository = organizerRepository;
            _eventRepository = eventRepository;
        }

        public async Task<ErrorOr<string>> Handle(UpdateSubEventCommand request, CancellationToken cancellationToken)
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

            var subEventId = SubEventId.Create(request.SubEventId);

            var updatedSubEvent = @event.SubEvents.FirstOrDefault(s => s.Id == subEventId);

            if (updatedSubEvent is null)
                return EventError.SubEventError.SubEventNotFound;

            string description = "";

            if (request.Description is not null)
                description = request.Description;
       
            try
            {
                updatedSubEvent.Update(
                    request.Name,
                    description,
                    request.StartDateTime,
                    request.EndDateTime);
            }
            catch(Exception ex)
            {
                return EventError.SubEventError.SubEventNotUpdated(ex.Message);
            }

            try
            {
                @event.UpdateSubEvent(updatedSubEvent);
                await _eventRepository.Update(@event);
            }
            catch (Exception ex)
            {
                return EventError.SubEventNotUpdated(ex.Message);
            }

            @event = await _eventRepository.GetEvent(eventId);

            var subEvent = @event.SubEvents.FirstOrDefault(x => x.Id == updatedSubEvent.Id);

            if (subEvent is not null &&
                subEvent.Id == updatedSubEvent.Id &&
                subEvent.Name == updatedSubEvent.Name &&
                subEvent.Description == updatedSubEvent.Description &&
                subEvent.StartDateTime == updatedSubEvent.StartDateTime &&
                subEvent.EndDateTime == updatedSubEvent.EndDateTime)
                return "SubEvent updated successfully!";

            return EventError.SubEventError.SubEventNotUpdatedDb;
        }
    }
}
