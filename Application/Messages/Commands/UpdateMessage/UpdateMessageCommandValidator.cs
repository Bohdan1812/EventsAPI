using FluentValidation;

namespace Application.Messages.Commands.UpdateMessage
{
    public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
    {
        public UpdateMessageCommandValidator()
        {
            RuleFor(c => c.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.MessageId)
                .NotEmpty()
                .WithMessage("Message id is required!");

            RuleFor(c => c.Text)
                .NotEmpty()
                .WithMessage("Message text must not be empty");
        }
    }
}
