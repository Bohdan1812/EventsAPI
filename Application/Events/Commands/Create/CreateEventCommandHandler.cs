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
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandHandler(
            IOrganizerRepository organizerRepository,
            IUserRepository userRepository,
            IEventRepository eventRepository)
        {
            _organizerRepository = organizerRepository;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
        }

        public async Task<ErrorOr<string>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var organizer = await _organizerRepository.GetOrganizer(request.appUserId);

            if (organizer is null)
            {
                return OrganizerError.OrganizerNotFound;
            }

            var user = await _userRepository.GetUser(organizer.UserId);

            if (user is null)
                return UserError.UserNotFound;

            string description = "";

            if(request.Description is not null) 
            {
                description = request.Description;
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
                description,
                organizer,
                request.StartDateTime.ToUniversalTime(),
                request.EndDateTime.ToUniversalTime(),
                request.SubEvents.ConvertAll(subEvent => new SubEvent(
                    subEvent.Name,
                    subEvent.Description,
                    subEvent.StartDateTime,
                    subEvent.EndDateTime)),
                address,
                link,
                request.IsPrivate,
                request.AllowParticipantsInvite);
            }

            catch (Exception ex)
            {
                return EventError.EventNotInitialized(ex.Message);
            }
           
            await _eventRepository.Add(newEvent);

            var @event = await _eventRepository.GetEvent(newEvent.Id);

            if (@event is not null && 
                @event.Equals(newEvent))
                return "Event created successfully!";

            return EventError.EventNotAdded;
        }
    }
}
