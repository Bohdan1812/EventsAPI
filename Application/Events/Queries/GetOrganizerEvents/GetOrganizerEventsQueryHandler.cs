using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.EventAggregate;
using Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace Application.Events.Queries.GetUserEvents
{
    public class GetOrganizerEventsQueryHandler : IRequestHandler<GetOrganizerEventsQuery, ErrorOr<List<Event>>>
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IEventRepository _eventRepository;

        public GetOrganizerEventsQueryHandler(
            IOrganizerRepository organizerRepository, 
            IEventRepository eventRepository)
        {
            _organizerRepository = organizerRepository;
            _eventRepository = eventRepository;
        }
        public async Task<ErrorOr<List<Event>>> Handle(GetOrganizerEventsQuery request, CancellationToken cancellationToken)
        {
            var organizer = await _organizerRepository.GetOrganizer(request.appUserId);

            if (organizer is null)
                return OrganizerError.OrganizerNotFound;

            return await _eventRepository.GetOrganizerEvents(organizer.Id);
        }
    }
}
