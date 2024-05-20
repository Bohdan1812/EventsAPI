using Application.Common.Errors;
using Application.Persistence.Repositories;
using Application.Users.Dto;
using Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetUserInfo
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, ErrorOr<UserInfo>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserInfoQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<UserInfo>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var userId = UserId.Create(request.UserId);
            var user = await _userRepository.GetFullUser(userId);

            if (user is null)
                return UserError.UserNotFound;

            return new UserInfo(user.Id.Value, user.FirstName, user.LastName, user.ApplicationUser.Email);
        }
    }
}
