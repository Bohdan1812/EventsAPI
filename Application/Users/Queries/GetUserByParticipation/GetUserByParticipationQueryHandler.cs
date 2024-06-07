using Application.Common.Errors;
using Application.Persistence.Repositories;
using Application.Users.Dto;
using Domain.ParticipationAggregate;
using Domain.ParticipationAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetUserByParticipation
{
    public class GetUserByParticipationQueryHandler : IRequestHandler<GetUserByParticipationQuery, ErrorOr<UserInfo>>
    {
        private readonly IParticipationRepository _participationRepository;
        private readonly IUserRepository _userRepository;

        public GetUserByParticipationQueryHandler(IParticipationRepository participationRepository, IUserRepository userRepository)
        {
            _participationRepository = participationRepository;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<UserInfo>> Handle(GetUserByParticipationQuery request, CancellationToken cancellationToken)
        {
            var participationId = ParticipationId.Create(request.ParticipationId);
            var participation = await _participationRepository.GetParticipation(participationId);

            if (participation is null)
                return ParticipationError.ParticipationNotFound;

            var user = await _userRepository.GetFullUser(participation.UserId);

            var userInfo = new UserInfo(
                participation.User.Id.Value, 
                participation.User.FirstName,
                participation.User.LastName,
                participation.User.ApplicationUser.Email,
                participation.User.PhotoPath);

            return userInfo;
        }
    }
}
