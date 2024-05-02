using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.InviteAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Invites.Commands.RemoveAsOrganizer
{
    public class RemoveOrganizerInviteCommandHandler : IRequestHandler<RemoveOrganizerInviteCommand, ErrorOr<string>>
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IInviteRepository  _inviteRepository;

        public RemoveOrganizerInviteCommandHandler(IOrganizerRepository organizerRepository, IInviteRepository inviteRepository)
        {
            _organizerRepository = organizerRepository;
            _inviteRepository = inviteRepository;
        }
        public async Task<ErrorOr<string>> Handle(RemoveOrganizerInviteCommand request, CancellationToken cancellationToken)
        {
            var organizer = await _organizerRepository.GetOrganizer(request.ApplicationUserId);

            if (organizer is null)
                return OrganizerError.OrganizerNotFound;

            var inviteId = InviteId.Create(request.InviteId);
            var invite = await _inviteRepository.GetFullInvite(inviteId);

            if (invite is null)
                return InviteError.InviteNotFound;

            if (invite.Event.OrganizerId != organizer.Id)
                return InviteError.InviteNotRemoved;

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
