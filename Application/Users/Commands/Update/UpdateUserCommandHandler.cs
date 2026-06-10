using MediatR;
using Microsoft.AspNetCore.Identity;
using ErrorOr;
using Application.Persistence.Repositories;
using Application.Common.Errors;

namespace Application.Users.Commands.Update
{

    public class UpdateUserCommandHandler
        : IRequestHandler<UpdateUserCommand, ErrorOr<string>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetUser(request.appUserId);

            if (user is null)
                return UserError.UserNotFound;

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            await _userRepository.Update(user);

            user = await _userRepository.GetUser(user.Id);

            if (user is not null)
            {
                if (user.FirstName == request.FirstName &&
                    user.LastName == request.LastName)
                {
                    return "User updateted successfully!";
                }
            }
            return UserError.UserNotUpdated;
        }
    }
}


