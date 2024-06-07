using Application.Common.Errors;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Events.Commands.RemoveEventPhoto
{
    public class RemoveEventPhotoCommandHandler : IRequestHandler<RemoveEventPhotoCommand, ErrorOr<string>>
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IEventPhotoService _eventPhotoService;

        public RemoveEventPhotoCommandHandler(IOrganizerRepository organizerRepository, IEventRepository eventRepository, IEventPhotoService eventPhotoService)
        {
            _organizerRepository = organizerRepository;
            _eventRepository = eventRepository;
            _eventPhotoService = eventPhotoService;
        }

        public async Task<ErrorOr<string>> Handle(RemoveEventPhotoCommand request, CancellationToken cancellationToken)
        {
            var organizer = await _organizerRepository.GetOrganizer(request.ApplicationUserId);

            if (organizer is null)
                return OrganizerError.OrganizerNotFound;

            var eventId = EventId.Create(request.EventId);
            var @event = await _eventRepository.GetEvent(eventId);

            if (@event is null)
                return EventError.EventNotFound;

            if (string.IsNullOrEmpty(@event.PhotoPath))
                return "Event does not contain a photo!";

            bool isDeleted = false;
            try 
            {
                isDeleted = _eventPhotoService.DeleteImage(@event.PhotoPath);
            }
            catch(Exception ex)
            {
                return EventError.EventPhotoDeleteError(ex.Message);
            }

            if (!isDeleted)
                return EventError.EventPreviousPhotoNotDeleted;

            @event.PhotoPath = null;

            await _eventRepository.Update(@event);

            @event = await _eventRepository.GetEvent(eventId);

            if (@event is not null && @event.PhotoPath is null)
                return "Event photo deleted successfully!";


            return EventError.EventPhotoDeleteError("Photo was not deleted!");
        }
    }
}
