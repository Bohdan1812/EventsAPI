using Application.Common.Errors;
using Application.Persistence.Repositories;
using Application.Users.Dto;
using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetParticipantsUserInfo
{
    public class GetParticipantsUserInfoQueryHandler : IRequestHandler<GetParticipantsUserInfoQuery, ErrorOr<List<UserInfo>>>
    {
        private readonly IParticipationRepository _participationRepository;
        private readonly IUserRepository _userRepository;

        public GetParticipantsUserInfoQueryHandler(IParticipationRepository participationRepository, IUserRepository userRepository)
        {
            _participationRepository = participationRepository;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<List<UserInfo>>> Handle(GetParticipantsUserInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicatinUserId);

            if (user is null)
                return UserError.UserNotFound;

            var eventId = EventId.Create(request.EventId);
            var participations = await _participationRepository.GetParticipationsByEvent(eventId);

            if (!participations.Any(p => p.UserId == user.Id))
                return ParticipationError.ParticipationNoPermission;

            List<UserInfo> users = [];

            foreach(var participation in participations)
            {
                var fullUser = await _userRepository.GetFullUser(participation.UserId);

                if (fullUser is null)
                    return UserError.UserNotFound;

                users.Add(new UserInfo(fullUser.Id.Value, fullUser.FirstName, fullUser.LastName, fullUser.ApplicationUser.Email, fullUser.PhotoPath)); 
            }

            return users;
        }
    }
}
