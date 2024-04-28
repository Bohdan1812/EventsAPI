using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;
using EventId = Domain.EventAggregate.ValueObjects.EventId;

namespace Application.Events.Commands.SubEventCommands.RemoveSubEvent
{
    public class RemoveSubEventCommandHandler : IRequestHandler<RemoveSubEventCommand, ErrorOr<string>>
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IEventRepository _eventRepository;

        public RemoveSubEventCommandHandler(IOrganizerRepository organizerRepository, IEventRepository eventRepository)
        {
            _organizerRepository = organizerRepository;
            _eventRepository = eventRepository;
        }

        public async Task<ErrorOr<string>> Handle(RemoveSubEventCommand request, CancellationToken cancellationToken)
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
            try
            {
                @event.RemoveSubEvent(subEventId);
                await _eventRepository.Update(@event);
            }
            catch (Exception ex)
            {
                return EventError.SubEventError.SubEventNotDeleted(ex.Message);
            }

            @event = await _eventRepository.GetEvent(eventId);

            if (@event is not null &&
                @event.SubEvents.FirstOrDefault(s => s.Id == subEventId) is null)
                return "SubEvent deleted successfully!";

            return EventError.SubEventError.SubEventNotDeletedDb;
        }
    }
}
