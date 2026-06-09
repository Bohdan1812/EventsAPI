using Application.Common.Errors;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using Domain.Common.Models;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginCommandHandler(
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtTokenGenerator jwtTokenGenerator
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<ErrorOr<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return ApplicationUserError.ApplicationUserNotFound;

            var result = await _signInManager.CheckPasswordSignInAsync(
                user, 
                request.Password, 
                lockoutOnFailure: false);

            if (!result.Succeeded)
                return ApplicationUserError.ApplicationUserWrongPassword;
            
            var token = _jwtTokenGenerator.GenerateToken(user);

            return token;
        }
    }
}
