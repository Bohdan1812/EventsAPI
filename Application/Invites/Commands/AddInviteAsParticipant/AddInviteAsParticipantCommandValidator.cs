using Application.Invites.Commands.AddInvite;
using FluentValidation;

namespace Application.Invites.Commands.AddInviteAsParticipation
{
    public class AddInviteAsParticipationCommandValidator : AbstractValidator<AddInviteCommand>
    {
        public AddInviteAsParticipationCommandValidator()
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
