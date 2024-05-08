using Domain.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ErrorOr;
using Domain.UserAggregate;
using Domain.UserAggregate.ValueObjects;
using Application.Persistence.Repositories;
using Domain.OrganizerAggregate;
using Domain.OrganizerAggregate.ValueObjects;
using Application.Common.Errors;

namespace Application.Authentication.Commands.Register
{

    public class RegsiterCommandHandler
        : IRequestHandler<RegisterCommand, ErrorOr<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public RegsiterCommandHandler(
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<ErrorOr<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var appUser = await _userManager.FindByEmailAsync(request.Email);

            if (appUser is not null)
            {
                return ApplicationUserError.DuplicateApplicationUser;
            }

            appUser = new ApplicationUser()
            {
                Email = request.Email,
                UserName = request.Email,
            };

            var result = await _userManager.CreateAsync(appUser, request.Password);

            if (result.Succeeded)
            {
                appUser = await _userManager.FindByEmailAsync(appUser.Email);

                if (appUser is not null)
                {
                    var user = new User(
                        request.FirstName,
                        request.LastName,
                        appUser);

                    await _userRepository.Add(user);

                    return "User created successfully!";
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
    

