using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.EventAggregate;
using Domain.EventAggregate.Entities;
using Domain.EventAggregate.ValueObjects;
using Domain.ParticipationAggregate;
using ErrorOr;
using MediatR;

namespace Application.Events.Commands.Create
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, ErrorOr<string>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IParticipationRepository _participationRepository;
        private readonly IChatRepository _chatRepository;

        public CreateEventCommandHandler(
            IEventRepository eventRepository,
            IOrganizerRepository organizerRepository,
            IParticipationRepository participationRepository,
            IUserRepository userRepository,
            IChatRepository chatRepository)
        {
            _eventRepository = eventRepository;
            _organizerRepository = organizerRepository;
            _participationRepository = participationRepository;
            _userRepository = userRepository;
            _chatRepository = chatRepository;
        }

        public async Task<ErrorOr<string>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var organizer = await _organizerRepository.GetOrganizer(request.appUserId);
            if (organizer is null)
            {
                return OrganizerError.OrganizerNotFound;
            }

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

            Participation? participation = null;

            var user = await _userRepository.GetUser(organizer.UserId);

            if (user is null)
                return UserError.UserNotFound;

            try
            {
                participation = new Participation(organizer, user, newEvent);
            }
            catch (Exception ex)
            {
                return ParticipationError.ParticipationNotInitialized(ex.Message);
            }

            

            if (newEvent is not null &&
                participation is not null)
            {
                await _eventRepository.Add(newEvent);
                await _participationRepository.Add(participation);

                return "Event created Successfully!";
            }

            return EventError.EventNotAdded;
        }
    }
}
