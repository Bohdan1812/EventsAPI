using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.EventAggregate.ValueObjects;
using Domain.InviteAggregate;
using ErrorOr;
using MediatR;

namespace Application.Invites.Queries.GetEventInvites
{
    public class GetEventInvitesQueryHandler
        : IRequestHandler<GetEventInvitesQuery, ErrorOr<List<Invite>>>
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IInviteRepository _inviteRepository;

        public GetEventInvitesQueryHandler(IOrganizerRepository organizerRepository, IInviteRepository inviteRepository)
        {
            _organizerRepository = organizerRepository;
            _inviteRepository = inviteRepository;
        }

        public async Task<ErrorOr<List<Invite>>> Handle(GetEventInvitesQuery request, CancellationToken cancellationToken)
        {
            var organizer = await _organizerRepository.GetOrganizer(request.ApplicationUserId);

            if (organizer is null)
                return OrganizerError.OrganizerNotFound;

            var eventId = EventId.Create(request.EventId);

            var invites = await _inviteRepository.GetInvitesByEvent(eventId);

            if(invites.Count > 0)
            {
                var invite = await _inviteRepository.GetFullInvite(invites[0].Id);

                if (invite is not null &&
                    invite.Event.OrganizerId != organizer.Id)
                    return InviteError.InviteNoPermission;
            }

            return invites;
        }
    }
}
