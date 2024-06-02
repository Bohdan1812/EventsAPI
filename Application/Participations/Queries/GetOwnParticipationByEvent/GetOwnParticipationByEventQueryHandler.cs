using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.EventAggregate.ValueObjects;
using Domain.ParticipationAggregate;
using ErrorOr;
using MediatR;

namespace Application.Participations.Queries.GetOwnParticipationByEvent
{
    public class GetOwnParticipationByEventQueryHandler : IRequestHandler<GetOwnParticipationByEventQuery, ErrorOr<Participation?>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IParticipationRepository _participationRepository;

        public GetOwnParticipationByEventQueryHandler(IUserRepository userRepository, IParticipationRepository participationRepository)
        {
            _userRepository = userRepository;
            _participationRepository = participationRepository;
        }

        public async Task<ErrorOr<Participation?>> Handle(GetOwnParticipationByEventQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicationUserId);

            if (user is null)
                return UserError.UserNotFound;

            return await _participationRepository.GetParticipationByUserEvent(user.Id, EventId.Create(request.EventId));
        }
    }
}
