using Application.Common.Errors;
using Application.Persistence.Repositories;
using Application.Users.Dto;
using Domain.OrganizerAggregate;
using Domain.OrganizerAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetOrganizerInfo
{
    public class GetOrganizerInfoQueryHandler : IRequestHandler<GetOrganizerInfoQuery, ErrorOr<UserInfo>>
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IUserRepository _userRepository;

        public GetOrganizerInfoQueryHandler(IOrganizerRepository organizerRepository, IUserRepository userRepository)
        {
            _organizerRepository = organizerRepository;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<UserInfo>> Handle(GetOrganizerInfoQuery request, CancellationToken cancellationToken)
        {
            var organizerId = OrganizerId.Create(request.OrganizerId);
            var organizer = await _organizerRepository.GetOrganizer(organizerId);

            if (organizer is null)
                return OrganizerError.OrganizerNotFound;

            var user = await _userRepository.GetFullUser(organizer.UserId);

            if (user is null)
                return UserError.UserNotFound;

            return new UserInfo(user.Id.Value, user.FirstName, user.LastName, user.ApplicationUser.Email, user.PhotoPath);
        }
    }
}
