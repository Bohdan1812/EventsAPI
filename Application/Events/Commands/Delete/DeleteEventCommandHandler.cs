using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Events.Commands.Delete
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, ErrorOr<string>>
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IEventRepository _eventRepository;

        public DeleteEventCommandHandler(IOrganizerRepository organizerRepository, IEventRepository eventRepository)
        {
            _organizerRepository = organizerRepository;
            _eventRepository = eventRepository;
        }

        public async Task<ErrorOr<string>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var organizer = await _organizerRepository.GetOrganizer(request.appUserId);

            if (organizer is null)
                return OrganizerError.OrganizerNotFound;

            var @event = await _eventRepository.GetEvent(EventId.Create(request.eventId));

            if (@event is null)
                return EventError.EventNotFound;

            if (organizer.Id != @event.OrganizerId)
                return EventError.EventUpdateNoPermission;

            await _eventRepository.Remove(@event.Id);

            @event = await _eventRepository.GetEvent(EventId.Create(request.eventId));

            if (@event is null)
                return "Event deleted successfully!";

            return EventError.EventNotDeleted;
        }
    }
}
