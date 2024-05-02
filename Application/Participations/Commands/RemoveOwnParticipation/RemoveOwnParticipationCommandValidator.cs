using FluentValidation;

namespace Application.Participations.Commands.RemoveOwnParticipation
{
    public class RemoveOwnParticipationCommandValidator 
        : AbstractValidator<RemoveOwnParticipationCommand>
    {
        public RemoveOwnParticipationCommandValidator() 
        {
            RuleFor(c => c.ApplicatioUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.ParticipationId)
                .NotEmpty()
                .WithMessage("Participation id is required!");
        }
    }
}
