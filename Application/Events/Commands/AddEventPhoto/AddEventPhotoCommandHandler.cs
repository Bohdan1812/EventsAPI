using Application.Common.Errors;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Events.Commands.AddEventPhoto
{
    public class AddEventPhotoCommandHandler : IRequestHandler<AddEventPhotoCommand, ErrorOr<string>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IEventPhotoService _eventPhotoService;

        public AddEventPhotoCommandHandler(IEventRepository eventRepository, IOrganizerRepository organizerRepository, IEventPhotoService eventPhotoService)
        {
            _eventRepository = eventRepository;
            _organizerRepository = organizerRepository;
            _eventPhotoService = eventPhotoService;
        }

        public async Task<ErrorOr<string>> Handle(AddEventPhotoCommand request, CancellationToken cancellationToken)
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

            if (!string.IsNullOrEmpty(@event.PhotoPath))
            {
                var isPhotoDeleted = false;

                try
                {
                    isPhotoDeleted = _eventPhotoService.DeleteImage(@event.PhotoPath);
                }
                catch (Exception ex)
                {
                    return EventError.EventPhotoDeleteError(ex.Message);
                }
                if (!isPhotoDeleted)
                {
                    return EventError.EventPreviousPhotoNotDeleted;
                }
            }

                var path = string.Empty;
                try 
                {
                    path = _eventPhotoService.SaveImage(request.Photo);
                }
                catch (Exception ex) 
                {
                    return EventError.EventPhototUploadError(ex.Message);
                }

                @event.PhotoPath = path;
                await _eventRepository.Update(@event);

                @event = await _eventRepository.GetEvent(eventId);

                if (@event is not null &&
                    !string.IsNullOrEmpty(@event.PhotoPath) &&
                    @event.PhotoPath == path)
                    return "Event photo uploaded successfully!";

                return EventError.EventPhotoNotUploaded;
            
        }
    }
}
