using FluentValidation;

namespace Application.Messages.Commands.DeleteMessage
{
    public class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
    {
        public DeleteMessageCommandValidator()
        {
            RuleFor(c => c.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.MessageId)
                .NotEmpty()
                .WithMessage("Message id is required!");
        }
    }
}
