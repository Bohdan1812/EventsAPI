using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.InviteAggregate;
using Domain.InviteAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Invites.Queries.GetInvite
{
    public class GetInviteQueryHandler : IRequestHandler<GetInviteQuery, ErrorOr<Invite?>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IInviteRepository _inviteRepository;

        public GetInviteQueryHandler(IUserRepository userRepository, IOrganizerRepository organizerRepository, IInviteRepository inviteRepository)
        {
            _userRepository = userRepository;
            _organizerRepository = organizerRepository;
            _inviteRepository = inviteRepository;
        }

        public async Task<ErrorOr<Invite?>> Handle(GetInviteQuery request, CancellationToken cancellationToken)
        {
            var inviteId = InviteId.Create(request.InviteId);
            var invite = await _inviteRepository.GetFullInvite(inviteId);

            if (invite is null)
                return InviteError.InviteNotFound;

            var organizer = await _organizerRepository.GetOrganizer(request.ApplicatioUserId);

            if (organizer is not null)
            {
                if (invite.Event.OrganizerId != organizer.Id)
                    return InviteError.InviteNoPermission;

                return invite;
            }

            var user = await _userRepository.GetUser(request.InviteId);

            if (user is not null)
            {
                if (invite.UserId != user.Id)
                    return InviteError.InviteNoPermission;

                return invite;
            }
            
            return UserError.UserNotFound;
        }
    }
}
