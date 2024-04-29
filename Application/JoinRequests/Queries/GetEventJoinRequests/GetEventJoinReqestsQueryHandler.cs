using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.EventAggregate.ValueObjects;
using Domain.JoinRequestAggregate;
using ErrorOr;
using MediatR;

namespace Application.JoinRequests.Queries.GetEventJoinRequests
{
    public class GetEventJoinReqestsQueryHandler
        : IRequestHandler<GetEventJoinRequestsQuery, ErrorOr<List<JoinRequest>>>
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IJoinRequestRepository _joinRequestRepository;

        public GetEventJoinReqestsQueryHandler(
            IOrganizerRepository organizerRepository, 
            IEventRepository eventRepository, 
            IJoinRequestRepository joinRequestRepository)
        {
            _organizerRepository = organizerRepository;
            _eventRepository = eventRepository;
            _joinRequestRepository = joinRequestRepository;
        }

        public async Task<ErrorOr<List<JoinRequest>>> Handle(GetEventJoinRequestsQuery request, CancellationToken cancellationToken)
        {
            var organizer = await _organizerRepository.GetOrganizer(request.ApplicatioUserId);

            if (organizer is null)
                return OrganizerError.OrganizerNotFound;

            var eventId = EventId.Create(request.EventId);

            var @event = await _eventRepository.GetEvent(eventId);
            
            if (@event is null)
                return EventError.EventNotFound;

            if (@event.OrganizerId != organizer.Id)
                return JoinRequestError.JoinRequestNoPermission;

            return await _joinRequestRepository.GetJoinRequestsByEvent(eventId);
        }
    }
}
