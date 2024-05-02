using FluentValidation;

namespace Application.Invites.Commands.RemoveAsOrganizer
{
    public class RemoveOrganizerInviteCommandValidator : AbstractValidator<RemoveOrganizerInviteCommand>
    {
        public RemoveOrganizerInviteCommandValidator()
        {
            RuleFor(c => c.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.InviteId)
                .NotEmpty()
                .WithMessage("Invite id is required!");
        }
    }
}
