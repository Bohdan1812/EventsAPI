using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.JoinRequestAggregate;
using Domain.OrganizerAggregate;
using ErrorOr;
using MediatR;

namespace Application.JoinRequests.Queries.GetOrganizerJoinRequests
{
    public class GetOrganizerJoinRequestsQueryHandler
        : IRequestHandler<GetOrganizerJoinRequestsQuery, ErrorOr<List<JoinRequest>>>
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IJoinRequestRepository _joinRequestRepository;

        public GetOrganizerJoinRequestsQueryHandler(IOrganizerRepository organizerRepository, IEventRepository eventRepository, IJoinRequestRepository joinRequestRepository)
        {
            _organizerRepository = organizerRepository;
            _eventRepository = eventRepository;
            _joinRequestRepository = joinRequestRepository;
        }

        public async Task<ErrorOr<List<JoinRequest>>> Handle(GetOrganizerJoinRequestsQuery request, CancellationToken cancellationToken)
        {
            var organizer = await _organizerRepository.GetOrganizer(request.ApplicationUserId);

            if (organizer is null)
                return OrganizerError.OrganizerNotFound;

            var events = await _eventRepository.GetOrganizerEvents(organizer.Id);
            List<JoinRequest> joinRequests = new List<JoinRequest>();

            foreach(var @event in events)
            {
                var eventJoinRequests = await _joinRequestRepository.GetJoinRequestsByEvent(@event.Id);
                joinRequests.AddRange(eventJoinRequests);
            }

            return joinRequests;
        }
    }
}
