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

            try
            {
                if (request.Address is not null)
                    address = new Address(
                        request.Address.AddressName,
                        request.Address.Longitude,
                        request.Address.Latitude);
            }
            catch(Exception ex)
            { 
                return EventError.EventNotInitialized(ex.Message); 
            }

            if (request.Link is not null)
                link = new Link(request.Link.Link);

            try
            {
                @event.Update(request.Name,
                    description,
                    request.StartDateTime.ToUniversalTime() ,
                    request.EndDateTime.ToUniversalTime(),
                    address,
                    link,
                    request.IsPrivate,
                    request.AllowParticipantsInvite);

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
                @event.StartDateTime == request.StartDateTime.ToUniversalTime() &&
                @event.EndDateTime == request.EndDateTime.ToUniversalTime() &&
                @event.Address == address &&
                @event.Link == link && 
                @event.IsPrivate == request.IsPrivate &&
                @event.AllowParticipantsInvite == request.AllowParticipantsInvite)
                return "Event updated successfully!";

            return EventError.EventNotUpdated;
        }
    }
}
