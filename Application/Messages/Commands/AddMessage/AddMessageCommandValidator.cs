using FluentValidation;

namespace Application.Messages.Commands.AddMessage
{
    public class AddMessageCommandValidator : AbstractValidator<AddMessageCommand>
    {
        public AddMessageCommandValidator()
        {
            RuleFor(c => c.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.ParticipationId)
                .NotEmpty()
                .WithMessage("Participation id is required!");

            RuleFor(c => c.Text)
                .NotEmpty()
                .WithMessage("Message text must not be empty");
        }
    }
}
