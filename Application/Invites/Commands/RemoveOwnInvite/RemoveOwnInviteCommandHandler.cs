using Application.Common.Errors;
using Application.Invites.Commands.DeleteOwnInvite;
using Application.Persistence.Repositories;
using Domain.InviteAggregate.ValueObjects;
using ErrorOr;
using MediatR;
using System.Runtime.InteropServices;

namespace Application.Invites.Commands.RemoveOwnInvite
{
    public class RemoveOwnInviteCommandHandler : IRequestHandler<RemoveOwnInviteCommand, ErrorOr<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IInviteRepository _inviteRepository;

        public RemoveOwnInviteCommandHandler(IUserRepository userRepository, IInviteRepository inviteRepository)
        {
            _userRepository = userRepository;
            _inviteRepository = inviteRepository;
        }

        public async Task<ErrorOr<string>> Handle(RemoveOwnInviteCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicatonUserId);

            if (user is null) 
            {
                return UserError.UserNotFound;
            }

            var inviteId = InviteId.Create(request.InviteId);
            var invite = await _inviteRepository.GetInvite(inviteId);

            if (invite is null) 
            {
                return InviteError.InviteNotFound;
            }

            if (invite.UserId != user.Id)
            {
                return InviteError.InviteNoPermission;
            }

            await _inviteRepository.Remove(inviteId);

            invite = await _inviteRepository.GetInvite(inviteId);

            if (invite is null)
            {
                return "Invite remove successfully !";
            }

            return InviteError.InviteNotRemoved;
        }
    }
}
