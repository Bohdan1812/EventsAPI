using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.InviteAggregate.ValueObjects;
using Domain.ParticipationAggregate;
using ErrorOr;
using MediatR;

namespace Application.Participations.Commands.AddParticipationFromInvite
{
    public class AddParticipationFromInviteCommandHandler
        : IRequestHandler<AddParticipationFromInviteCommand, ErrorOr<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IInviteRepository _inviteRepository;
        private readonly IParticipationRepository _participationRepository;

        public AddParticipationFromInviteCommandHandler(IUserRepository userRepository, IInviteRepository inviteRepository, IParticipationRepository participationRepository)
        {
            _userRepository = userRepository;
            _inviteRepository = inviteRepository;
            _participationRepository = participationRepository;
        }

        public async Task<ErrorOr<string>> Handle(AddParticipationFromInviteCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicatioUserId);

            if (user is null)
                return UserError.UserNotFound;

            var inviteId = InviteId.Create(request.InviteId);
            var invite = await _inviteRepository.GetFullInvite(inviteId);

            if (invite is null)
                return InviteError.InviteNotFound;

            Participation? participation = null;

            try
            {
                participation = new Participation(invite, user.Id);
            }
            catch (Exception ex)
            {
                return ParticipationError.ParticipationNotInitialized(ex.Message);
            }

            var participationId = participation.Id;

            await _participationRepository.Add(participation);

            participation = await _participationRepository.GetParticipation(participationId);

            if (participation is not null &&
                participation.Id == participationId &&
                participation.UserId == invite.UserId &&
                participation.EventId == invite.EventId)
            {
                await _inviteRepository.Remove(inviteId);
                invite = await _inviteRepository.GetInvite(inviteId);

                if (invite is not null)
                    return InviteError.InviteNotRemoved;

                return "Participation created successfully!";
            }

            return ParticipationError.ParticipationNotAdded;
        }
    }
}
