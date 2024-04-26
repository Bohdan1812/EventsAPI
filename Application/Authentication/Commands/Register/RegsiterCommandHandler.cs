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
        private readonly IOrganizerRepository _organizerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public RegsiterCommandHandler(
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager
,
            IOrganizerRepository organizerRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _organizerRepository = organizerRepository;
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
                        UserId.CreateUnique(),
                        request.FirstName,
                        request.LastName,
                        appUser);

                    await _userRepository.Add(user);

                    var organizer = new Organizer(
                        OrganizerId.CreateUnique(),
                        user.Id
                        );

                    await _organizerRepository.Add(organizer);

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
    

