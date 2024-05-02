using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.ParticipationAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Participations.Commands.RemoveOwnParticipation
{
    public class RemoveOwnParticipationCommandHandler
        : IRequestHandler<RemoveOwnParticipationCommand, ErrorOr<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IParticipationRepository _participationRepository;

        public RemoveOwnParticipationCommandHandler(IUserRepository userRepository, IParticipationRepository participationRepository)
        {
            _userRepository = userRepository;
            _participationRepository = participationRepository;
        }

        public async Task<ErrorOr<string>> Handle(RemoveOwnParticipationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicatioUserId);

            if (user is null)
                return UserError.UserNotFound;

            var participationId = ParticipationId.Create(request.ParticipationId);
            var participation = await _participationRepository.GetParticipation(participationId);

            if (participation is null)
                return ParticipationError.ParticipationNotFound;

            if (participation.UserId != user.Id)
                return ParticipationError.ParticipationNoPermission;

            await _participationRepository.Remove(participationId);

            participation = await _participationRepository.GetParticipation(participationId);

            if (participation is null)
                return "Participation removed successfully!";

            return ParticipationError.ParticipationNotRemoved;
        }
    }
}
