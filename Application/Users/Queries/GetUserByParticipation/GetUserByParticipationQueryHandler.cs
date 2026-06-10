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

            var user = await _userRepository.GetUser(participation.UserId);

            if (user is null)
                return UserError.UserNotFound;
                
            var userInfo = new UserInfo(
                user.Id.Value, 
                user.FirstName,
                user.LastName,
                user.Email,
                user.PhotoPath);

            return userInfo;
        }
    }
}
