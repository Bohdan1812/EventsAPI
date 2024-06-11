using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.EventAggregate.ValueObjects;
using Domain.JoinRequestAggregate;
using Domain.JoinRequestAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.JoinRequests.Commands.Add
{
    public class AddJoinRequestCommandHanlder : IRequestHandler<AddJoinRequestCommand, ErrorOr<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IJoinRequestRepository _joinRequestRepository;

        public AddJoinRequestCommandHanlder(IUserRepository userRepository, IEventRepository eventRepository, IJoinRequestRepository joinRequestRepository)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _joinRequestRepository = joinRequestRepository;
        }

        public async Task<ErrorOr<string>> Handle(AddJoinRequestCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicatioUserId);

            if (user is null)
                return UserError.UserNotFound;

            var eventId = EventId.Create(request.EventId);

            var @event = await _eventRepository.GetFullEvent(eventId);

            if (@event is null)
                return EventError.EventNotFound;

            var joinRequestId = JoinRequestId.CreateUnique();

            try
            {
                var newJoinRequest = new JoinRequest(
                    joinRequestId,
                    user,
                    @event);

                await _joinRequestRepository.Add(newJoinRequest);
            }
            catch (Exception ex)
            {
                return JoinRequestError.JoinRequestNotInitialized(ex.Message);
            }

            var joinRequest = await _joinRequestRepository.GetFullJoinRequest(joinRequestId);

            if (joinRequest is not null &&
                joinRequest.Id == joinRequestId &&
                joinRequest.User == user &&
                joinRequest.Event == @event)
                return "Join request created successfully!";

            return JoinRequestError.JoinRequestNotAdded;
        }
    }
}
