using Application.Common.Errors;
using Application.Invites.Commands.AddInvite;
using Application.Persistence.Repositories;
using Domain.EventAggregate.ValueObjects;
using Domain.InviteAggregate;
using Domain.InviteAggregate.ValueObjects;
using Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Invites.Commands.AddInviteAsParticipant
{
    public class AddInviteAsParticipantCommandHandler : IRequestHandler<AddInviteCommand, ErrorOr<string>>
    {
        private readonly IParticipationRepository _participationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IInviteRepository _inviteRepository;
        public AddInviteAsParticipantCommandHandler(IParticipationRepository participationRepository, IUserRepository userRepository, IEventRepository eventRepository, IInviteRepository inviteRepository)
        {
            _participationRepository = participationRepository;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _inviteRepository = inviteRepository;
        }

        public async Task<ErrorOr<string>> Handle(AddInviteCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicationUserId);

            if (user is null)
                return UserError.UserNotFound;

            var eventId = EventId.Create(request.EventId);
            var participation = await _participationRepository.GetParticipationByUserEvent(user.Id, eventId);

            if (participation is null)
                return ParticipationError.ParticipationNotFound;

            var userId = UserId.Create(request.UserId);
            var invitedUser = await _userRepository.GetFullUser(userId);

            if (invitedUser is null)
                return UserError.UserNotFound;

            var @event = await _eventRepository.GetFullEvent(eventId);

            if (@event is null)
                return EventError.EventNotFound;

            if (!@event.Participations.Any(p => p.Id == participation.Id))
                return InviteError.InviteNoPermission;

            Invite? invite = null;
            var inviteId = InviteId.CreateUnique();

            try
            {
                invite = new Invite(
                    inviteId,
                    participation.Id,
                    invitedUser,
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
                    invite.UserId == invitedUser.Id &&
                    invite.EventId == @event.Id)
                    return "Invite created successfully !";
            }

            return InviteError.InviteNotAdded;
        }
    }
}
