using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.EventAggregate.ValueObjects;
using Domain.ParticipationAggregate;
using ErrorOr;
using MediatR;

namespace Application.Participations.Queries.GetParticipationsByEvent
{
    public class GetParticipationsByEventQueryHandler
        : IRequestHandler<GetParticipationsByEventQuery, ErrorOr<List<Participation>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IParticipationRepository _participationRepository;

        public GetParticipationsByEventQueryHandler(IUserRepository userRepository, IParticipationRepository participationRepository)
        {
            _userRepository = userRepository;
            _participationRepository = participationRepository;
        }

        public async Task<ErrorOr<List<Participation>>> Handle(GetParticipationsByEventQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicationUserId);

            if (user is null)
                return UserError.UserNotFound;

            var eventId = EventId.Create(request.EventId);
            var participations = await _participationRepository.GetParticipationsByEvent(eventId);

            if (participations.FirstOrDefault(p => p.UserId == user.Id) is null)
                return ParticipationError.ParticipationNoPermission;

            return participations;
        }
    }
}
