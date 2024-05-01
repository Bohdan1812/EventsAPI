using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.JoinRequestAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.JoinRequests.Commands.RemoveAsOrganizer
{
    public class RemoveOrganizerJoinRequestCommandHandler : IRequestHandler<RemoveOrganizerJoinRequestCommand, ErrorOr<string>>
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IJoinRequestRepository _joinRequestRepository;

        public RemoveOrganizerJoinRequestCommandHandler(IOrganizerRepository organizerRepository, IJoinRequestRepository joinRequestRepository)
        {
            _organizerRepository = organizerRepository;
            _joinRequestRepository = joinRequestRepository;
        }

        public async Task<ErrorOr<string>> Handle(RemoveOrganizerJoinRequestCommand request, CancellationToken cancellationToken)
        {
            var organizer = await _organizerRepository.GetOrganizer(request.ApplicationUserId);

            if (organizer is null)
                return OrganizerError.OrganizerNotFound;

            var joinRequestId = JoinRequestId.Create(request.JoinRequestId);
            var joinRequest = await _joinRequestRepository.GetFullJoinRequest(joinRequestId);

            if (joinRequest is null)
                return JoinRequestError.JoinRequestNotFound;

            if (joinRequest.Event.OrganizerId != organizer.Id)
                return JoinRequestError.JoinRequestNoPermission;

            await _joinRequestRepository.Remove(joinRequestId);

            joinRequest = await _joinRequestRepository.GetFullJoinRequest(joinRequestId);

            if (joinRequest is null)
                return "Join request removed successfully!";

            return JoinRequestError.JoinRequestNotRemoved;
        }
    }
}
