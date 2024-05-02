using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.JoinRequestAggregate.ValueObjects;
using Domain.ParticipationAggregate;
using ErrorOr;
using MediatR;

namespace Application.Participations.Commands.AddParticipationFromJoinRequest
{
    public class AddParticipationFromJoinRequestCommandHandler
        : IRequestHandler<AddParticipationFromJoinRequestCommand, ErrorOr<string>>
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IJoinRequestRepository _joinRequestRepository;
        private readonly IParticipationRepository _participationRepository;
        public AddParticipationFromJoinRequestCommandHandler(IOrganizerRepository organizerRepository, IParticipationRepository participationRepository, IJoinRequestRepository joinRequestRepository)
        {
            _organizerRepository = organizerRepository;
            _participationRepository = participationRepository;
            _joinRequestRepository = joinRequestRepository;
        }

        public async Task<ErrorOr<string>> Handle(AddParticipationFromJoinRequestCommand request, CancellationToken cancellationToken)
        {
            var organizer = await _organizerRepository.GetOrganizer(request.ApplicationUserId);

            if (organizer is null)
                return OrganizerError.OrganizerNotFound;

            var joinRequestId = JoinRequestId.Create(request.JoinRequestId);
            var joinRequest = await _joinRequestRepository.GetFullJoinRequest(joinRequestId);

            if (joinRequest is null)
                return JoinRequestError.JoinRequestNotFound;

            Participation? participation = null;
            try 
            {
                participation = new Participation(joinRequest, organizer.Id);
            }
            catch(Exception ex)
            {
                return ParticipationError.ParticipationNotInitialized(ex.Message);
            }

            var participationId = participation.Id;

            await _participationRepository.Add(participation);

            participation = await _participationRepository.GetParticipation(participationId);

            if (participation is not null &&
                participation.Id == participationId &&
                participation.UserId == joinRequest.UserId &&
                participation.EventId == joinRequest.EventId)
            {
                await _joinRequestRepository.Remove(joinRequestId);

                joinRequest = await _joinRequestRepository.GetJoinRequest(joinRequestId);

                if (joinRequest is not null)
                    return JoinRequestError.JoinRequestNotRemoved;

                return "Participation created successfully!";
            }

            return ParticipationError.ParticipationNotAdded;
        }
    }
}
