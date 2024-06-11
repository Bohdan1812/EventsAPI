using Application.Common.Errors;
using Application.Events.Dto;
using Application.JoinRequests.Dto;
using Application.Persistence.Repositories;
using Application.Users.Dto;
using Domain.EventAggregate.ValueObjects;
using Domain.JoinRequestAggregate;
using ErrorOr;
using Mapster;
using MediatR;

namespace Application.JoinRequests.Queries.GetEventJoinRequests
{
    public class GetEventJoinReqestsQueryHandler
        : IRequestHandler<GetEventJoinRequestsQuery, ErrorOr<List<JoinRequest>>>
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IJoinRequestRepository _joinRequestRepository;
        private readonly IUserRepository _userRepository;

        public GetEventJoinReqestsQueryHandler(
            IOrganizerRepository organizerRepository,
            IEventRepository eventRepository,
            IJoinRequestRepository joinRequestRepository,
            IUserRepository userRepository)
        {
            _organizerRepository = organizerRepository;
            _eventRepository = eventRepository;
            _joinRequestRepository = joinRequestRepository;
            _userRepository = userRepository;
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

            /*var joinRequestsResponse = new List<JoinRequestInfoResponse>();

            foreach(var joinRequest in joinRequests)
            {
                joinRequestsResponse.Add(new JoinRequestInfoResponse
                    (joinRequest.Id.Value,
                    new UserInfo(
                        joinRequest.Id.Value, 
                        joinRequest.User.FirstName, 
                        joinRequest.User.LastName, 
                        joinRequest.User.ApplicationUser.Email, 
                        joinRequest.User.PhotoPath),
                    new Events.Dto.EventInfo(
                        joinRequest.Event.Id.Value, 
                        joinRequest.Event.Name, 
                        joinRequest.Event.Description,
                        joinRequest.Event.OrganizerId.Value, 
                        joinRequest.Event.Participations.Count(), 
                        joinRequest.Event.StartDateTime,
                        joinRequest.Event.EndDateTime, 
                        joinRequest.Event.SubEvents.Adapt<List<SubEventInfo>>(), 
                        new AddressInfo(
                            joinRequest.Event.Address.AddressName, 
                            joinRequest.Event.Address.Longitude,
                            joinRequest.Event.Address.Latitude), 
                        joinRequest.Event.Link.Value)));
            }

            return joinRequestsResponse;
            */
        }
    }
}
