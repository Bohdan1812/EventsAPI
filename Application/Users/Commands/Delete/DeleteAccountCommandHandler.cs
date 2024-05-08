using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.Common.Models;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands.Delete
{
    public class DeleteAccountCommandHandler
        : IRequestHandler<DeleteAccountCommand, ErrorOr<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;

        public DeleteAccountCommandHandler(UserManager<ApplicationUser> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<string>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var appUser = await _userManager.FindByIdAsync(request.AppUserId.ToString());

            if (appUser is null)
            {
                return ApplicationUserError.ApplicationUserNotFound;
            }

            var isPasswordCorrect = await _userManager
                .CheckPasswordAsync(appUser, request.Password);

            if (!isPasswordCorrect)
            {
                return UserError.UserDeleteWrongPassword;
            }

            var user = await _userRepository.GetUser(request.AppUserId);

            if (user is null)
                return UserError.UserNotFound;

            user = await _userRepository.GetFullUser(user.Id);

            if (user is null)
                return UserError.UserNotFound;

            if (user.Organizer.Events.Count > 0)
                return OrganizerError.OrganizerContiansEvents;

            var deleteAppUserResult = await _userManager.DeleteAsync(appUser);

            if (user is not null)
            {
                await _userRepository.Remove(user.Id);
            }

            if (deleteAppUserResult.Succeeded)
            {
                return "Account deleted successfully!";
            }

            return ApplicationUserError.ApplicationUserNotDeleted(
                string.Join(", ", deleteAppUserResult.Errors
                    .Select(e => e.Code)),
                string.Join(", ", deleteAppUserResult.Errors
                    .Select(e => e.Description)));
        }
    }
}
