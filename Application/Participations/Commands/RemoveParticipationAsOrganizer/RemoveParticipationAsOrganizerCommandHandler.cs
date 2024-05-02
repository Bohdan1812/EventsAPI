using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.ParticipationAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Participations.Commands.RemoveParticipationAsOrganizer
{
    public class RemoveParticipationAsOrganizerCommandHandler
        : IRequestHandler<RemoveParticipationAsOrganizerCommand, ErrorOr<string>>
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IParticipationRepository _participationRepository;

        public RemoveParticipationAsOrganizerCommandHandler(IOrganizerRepository organizerRepository, IParticipationRepository participationRepository)
        {
            _organizerRepository = organizerRepository;
            _participationRepository = participationRepository;
        }

        public async Task<ErrorOr<string>> Handle(RemoveParticipationAsOrganizerCommand request, CancellationToken cancellationToken)
        {
            var organizer = await _organizerRepository.GetOrganizer(request.ApplicationUserId);

            if (organizer is null)
                return OrganizerError.OrganizerNotFound;

            var participationId = ParticipationId.Create(request.ParticipationId);
            var participation = await _participationRepository.GetFullParticipation(participationId);

            if (participation is null)
                return ParticipationError.ParticipationNotFound;

            if (participation.Event.OrganizerId != organizer.Id)
                return ParticipationError.ParticipationNoPermission;

            await _participationRepository.Remove(participationId);

            participation = await _participationRepository.GetParticipation(participationId);

            if (participation is null)
                return "Participation removed successfully!";

            return ParticipationError.ParticipationNotRemoved;
        }
    }
}
