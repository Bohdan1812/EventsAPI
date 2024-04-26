using Domain.Common.Models;
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

        private readonly UserManager<ApplicationUser> _userManager;
        public UpdateUserCommandHandler(
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
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


