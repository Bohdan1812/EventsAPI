using FluentValidation;

namespace Application.Invites.Commands.AddInvite
{
    public class AddInviteCommandValidator : AbstractValidator<AddInviteCommand>
    {
        public AddInviteCommandValidator() 
        {
            RuleFor(c => c.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.UserId)
                .NotEmpty()
                .WithMessage("Invited user id is required!");

            RuleFor(c => c.EventId)
                .NotEmpty()
                .WithMessage("Event id is required!");
        }
    }
}
