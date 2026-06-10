using Application.Common.Errors;
using Domain.Common.Models;
using Domain.UserAggregate;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Application.Persistence.Repositories;
using Infrastructure.Models;

namespace Application.Persistence.Services.Authentication
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        public IdentityService(
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            JwtTokenGenerator jwtTokenGenerator)
            {
                _userRepository = userRepository;
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtTokenGenerator = jwtTokenGenerator;
            }

        public async Task<ErrorOr<Guid>> DeleteUserAsync(Guid appUserId, string password)
        {
            var appUser = await _userManager.FindByIdAsync(appUserId.ToString());

            if (appUser is null)
                return ApplicationUserError.ApplicationUserNotFound;

            var isPasswordCorrect = await _userManager
                .CheckPasswordAsync(appUser, password);

            if (!isPasswordCorrect)
                return UserError.UserDeleteWrongPassword;

            var deleteAppUserResult = await _userManager.DeleteAsync(appUser);
            
            if (deleteAppUserResult.Succeeded)
                return appUserId;
 
            return ApplicationUserError.ApplicationUserNotDeleted(
                string.Join(", ", deleteAppUserResult.Errors
                    .Select(e => e.Code)),
                string.Join(", ", deleteAppUserResult.Errors
                    .Select(e => e.Description)));

        }

        public async Task<ErrorOr<(string token, int expiryMinutes)>> LoginUserAsync(string email, string password)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return ApplicationUserError.ApplicationUserFailedLogin;

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
           
            if (!result.Succeeded)
                return ApplicationUserError.ApplicationUserFailedLogin;

            var token = _jwtTokenGenerator.GenerateToken(user);
            int expiryMinutes = _jwtTokenGenerator.GetExpiryMinutes();
            
            return (token, expiryMinutes);
        }

        public async Task<ErrorOr<Guid>> RegisterUserAsync(string email, string password, string firstName, string lastName)
        {
            var appUser = await _userManager.FindByEmailAsync(email);

            if (appUser is not null)
            {
                return ApplicationUserError.DuplicateApplicationUser;
            }

            appUser = new ApplicationUser()
            {
                Email = email,
                UserName = email,
            };

            var result = await _userManager.CreateAsync(appUser, password);

            if (result.Succeeded)
            {
                appUser = await _userManager.FindByEmailAsync(appUser.Email);

                if (appUser is not null)
                {
                    var user = new User(
                        firstName,
                        lastName,
                        email,
                        appUser.Id);

                    await _userRepository.Add(user);

                    return user.ApplicationUserId;
                }
                return ApplicationUserError.ApplicationUserNotAdded;
            }
            else 
            {
                return Error.Validation(code: string.Join(", ", result.Errors
                    .Select(e => e.Code)),
                        description: string.Join(", ", result.Errors
                    .Select(e => e.Description)));
            }
        }
    }
}