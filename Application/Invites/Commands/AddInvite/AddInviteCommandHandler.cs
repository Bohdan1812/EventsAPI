using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.EventAggregate.ValueObjects;
using Domain.InviteAggregate;
using Domain.InviteAggregate.ValueObjects;
using Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Invites.Commands.AddInvite
{
    public class AddInviteCommandHandler : IRequestHandler<AddInviteCommand, ErrorOr<string>>
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IInviteRepository _inviteRepository;
        public AddInviteCommandHandler(IOrganizerRepository organizerRepository, IUserRepository userRepository, IEventRepository eventRepository, IInviteRepository inviteRepository)
        {
            _organizerRepository = organizerRepository;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _inviteRepository = inviteRepository;
        }

        public async Task<ErrorOr<string>> Handle(AddInviteCommand request, CancellationToken cancellationToken)
        {
            var organizer = await _organizerRepository.GetOrganizer(request.ApplicationUserId);

            if (organizer is null)
                return OrganizerError.OrganizerNotFound;

            var userId = UserId.Create(request.UserId);
            var user = await _userRepository.GetFullUser(userId);

            if (user is null)
                return UserError.UserNotFound;

            var eventId = EventId.Create(request.EventId);
            var @event = await _eventRepository.GetFullEvent(eventId);

            if (@event is null)
                return EventError.EventNotFound;

            if (@event.OrganizerId != organizer.Id)
                return InviteError.InviteNoPermission;

            Invite? invite = null;
            var inviteId = InviteId.CreateUnique();

            try
            {
                invite = new Invite(
                    inviteId,
                    organizer.Id,
                    user,
                    @event);
            }
            catch (Exception ex)
            {
                return InviteError.InviteNotInitialized(ex.Message);
            }

            if (invite is not null) 
            {
                await _inviteRepository.Add(invite);

                invite = await _inviteRepository.GetInvite(inviteId);

                if (invite is not null &&
                    invite.Id == inviteId &&
                    invite.UserId == user.Id &&
                    invite.EventId == @event.Id)
                    return "Invite created successfully !";
            }

            return InviteError.InviteNotAdded;
        }
    }
}
