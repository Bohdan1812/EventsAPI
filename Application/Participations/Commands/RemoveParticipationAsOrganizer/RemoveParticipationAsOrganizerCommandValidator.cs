using FluentValidation;

namespace Application.Participations.Commands.RemoveParticipationAsOrganizer
{
    public class RemoveParticipationAsOrganizerCommandValidator 
        : AbstractValidator<RemoveParticipationAsOrganizerCommand>
    {
        public RemoveParticipationAsOrganizerCommandValidator() 
        {
            RuleFor(c => c.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.ParticipationId)
                .NotEmpty()
                .WithMessage("Participation id is required!");
        }
    }
}
