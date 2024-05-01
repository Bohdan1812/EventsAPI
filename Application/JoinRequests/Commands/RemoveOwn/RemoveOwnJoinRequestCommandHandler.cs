using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.JoinRequestAggregate.ValueObjects;
using Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.JoinRequests.Commands.RemoveOwn
{
    public class RemoveOwnJoinRequestCommandHandler : IRequestHandler<RemoveOwnJoinRequestCommand, ErrorOr<string>>
    {
        private readonly IJoinRequestRepository _joinRequestRepository;
        private readonly IUserRepository _userRepository;

        public RemoveOwnJoinRequestCommandHandler(IJoinRequestRepository joinRequestRepository, IUserRepository userRepository)
        {
            _joinRequestRepository = joinRequestRepository;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<string>> Handle(RemoveOwnJoinRequestCommand request, CancellationToken cancellationToken)
        {
            var joinRequestId = JoinRequestId.Create(request.JoinRequestId);
            var joinRequest = await _joinRequestRepository.GetJoinRequest(joinRequestId);

            if (joinRequest is null)
                return JoinRequestError.JoinRequestNotFound;
            var user = await _userRepository.GetUser(request.ApplicationUserId);

            if (user is null)
                return UserError.UserNotFound;

            if (joinRequest.UserId != user.Id)
                return JoinRequestError.JoinRequestNoPermission;

            await _joinRequestRepository.Remove(joinRequestId);

            joinRequest = await _joinRequestRepository.GetJoinRequest(joinRequestId);

            if (joinRequest is null)
                return "Join request removed successfully!";

            return JoinRequestError.JoinRequestNotRemoved;
        }
    }
}
