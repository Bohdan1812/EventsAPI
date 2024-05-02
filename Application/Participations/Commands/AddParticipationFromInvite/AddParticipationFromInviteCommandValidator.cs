using FluentValidation;

namespace Application.Participations.Commands.AddParticipationFromInvite
{
    public class AddParticipationFromInviteCommandValidator 
        : AbstractValidator<AddParticipationFromInviteCommand>
    {
        public AddParticipationFromInviteCommandValidator() 
        {
            RuleFor(c => c.ApplicatioUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.InviteId)
                .NotEmpty()
                .WithMessage("Invite id is required!");
        }
    }
}
