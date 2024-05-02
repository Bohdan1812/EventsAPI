using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.ParticipationAggregate;
using Domain.ParticipationAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Participations.Queries.GetParticipation
{
    public class GetParticipationQueryHandler
        : IRequestHandler<GetParticipationQuery, ErrorOr<Participation>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IParticipationRepository _participationRepository;

        public GetParticipationQueryHandler(IUserRepository userRepository, IOrganizerRepository organizerRepository, IParticipationRepository participationRepository)
        {
            _userRepository = userRepository;
            _organizerRepository = organizerRepository;
            _participationRepository = participationRepository;
        }

        public async Task<ErrorOr<Participation>> Handle(GetParticipationQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicationUserId);

            if (user is null)
                return UserError.UserNotFound;

            var participationId = ParticipationId.Create(request.ParticipationId);
            var participation = await _participationRepository.GetFullParticipation(participationId);

            if (participation is null)
                return ParticipationError.ParticipationNotFound;

            if (participation.UserId != user.Id)
            {
                var organizer = await _organizerRepository.GetOrganizer(request.ApplicationUserId);

                if (organizer is null)
                    return OrganizerError.OrganizerNotFound;

                if (participation.Event.OrganizerId != organizer.Id)
                    return ParticipationError.ParticipationNoPermission;
            }

            return participation;
        }
    }
}
