using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.ParticipationAggregate;
using ErrorOr;
using MediatR;

namespace Application.Participations.Queries.GetOwnParticipations
{
    public class GetOwnParticipationsQueryHandler
        : IRequestHandler<GetOwnParticipationsQuery, ErrorOr<List<Participation>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IParticipationRepository _participationRepository;

        public GetOwnParticipationsQueryHandler(IUserRepository userRepository, IParticipationRepository participationRepository)
        {
            _userRepository = userRepository;
            _participationRepository = participationRepository;
        }

        public async Task<ErrorOr<List<Participation>>> Handle(GetOwnParticipationsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicationUserId);

            if (user is null)
                return UserError.UserNotFound;

            var participations = await _participationRepository.GetParticipationsByUser(user.Id);

            return participations;
        }
    }
}
