using Application.Common.Errors;
using Application.Persistence.Repositories;
using Application.Users.Dto;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetCurrentUserInfo
{
    public class GetCurrentUserInfoQueryHandler : IRequestHandler<GetCurrentUserInfoQuery, ErrorOr<UserInfo>>
    {
        private readonly IUserRepository _userRepository;

        public GetCurrentUserInfoQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<UserInfo>> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicationUserId);

            if (user is null)
                return UserError.UserNotFound;

            user = await _userRepository.GetFullUser(user.Id);

            if (user is null)
                return UserError.UserNotFound;

            return new UserInfo(user.Id.Value, user.FirstName, user.LastName, user.ApplicationUser.Email);
        }
    }
}
