using Application.Invites.Commands.DeleteOwnInvite;
using FluentValidation;

namespace Application.Invites.Commands.RemoveOwnInvite
{
    public class RemoveOwnInviteCommandValidator : AbstractValidator<RemoveOwnInviteCommand>
    {
        public RemoveOwnInviteCommandValidator() 
        {
            RuleFor(c => c.ApplicatonUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser is required!");

            RuleFor(c => c.InviteId)
                .NotEmpty()
                .WithMessage("Invite id is required!");
        }
    }
}
