using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.EventAggregate;
using ErrorOr;
using MediatR;

namespace Application.Events.Queries.GetAllUserEvents
{
    public class GetAllUserEventsQueryHandler : IRequestHandler<GetUserEventsQuery, ErrorOr<List<Event>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;

        public GetAllUserEventsQueryHandler(
            IOrganizerRepository organizerRepository,
            IEventRepository eventRepository,
            IParticipationRepository participationRepository,
            IUserRepository userRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<List<Event>>> Handle(GetUserEventsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicationUserId);

            if (user is null)
                return UserError.UserNotFound;

            var userEvents = await _eventRepository.GetUserEvents(user.Id);
            return userEvents.Where(e => e.StartDateTime >= request.StartDateTime).ToList();
        }
    }
}
