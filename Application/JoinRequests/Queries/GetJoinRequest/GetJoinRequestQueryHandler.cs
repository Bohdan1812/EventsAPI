using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.JoinRequestAggregate;
using Domain.JoinRequestAggregate.ValueObjects;
using Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.JoinRequests.Queries.GetJoinRequest
{
    public class GetJoinRequestQueryHandler
        : IRequestHandler<GetJoinRequestQuery, ErrorOr<JoinRequest>>
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJoinRequestRepository _joinRequestRepository;
        private readonly IEventRepository _eventRepository;

        public GetJoinRequestQueryHandler(IOrganizerRepository organizerRepository, IUserRepository userRepository, IJoinRequestRepository joinRequestRepository, IEventRepository eventRepository)
        {
            _organizerRepository = organizerRepository;
            _userRepository = userRepository;
            _joinRequestRepository = joinRequestRepository;
            _eventRepository = eventRepository;
        }

        public async Task<ErrorOr<JoinRequest>> Handle(GetJoinRequestQuery request, CancellationToken cancellationToken)
        {
            var joinRequestId = JoinRequestId.Create(request.JoinRequestId);
            var joinRequest = await _joinRequestRepository.GetJoinRequest(joinRequestId);

            if (joinRequest is null)
                return JoinRequestError.JoinRequestNotFound;

            var user = await _userRepository.GetUser(request.ApplicationUserID);

            if (user is null)
                return UserError.UserNotFound;

            if(joinRequest.UserId != user.Id)
            {
                var organizer = await _organizerRepository.GetOrganizer(request.ApplicationUserID);

                if (organizer is null)
                    return OrganizerError.OrganizerNotFound;

                var @event = await _eventRepository.GetEvent(joinRequest.EventId);

                if (@event is null)
                    return EventError.EventNotFound;

                if (@event.OrganizerId != organizer.Id)
                    return JoinRequestError.JoinRequestNoPermission;
            }

            return joinRequest;
        }
    }
}
