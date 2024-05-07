using FluentValidation;

namespace Application.Chat.Commands.AddMessage
{
    public class AddMessageCommandValidator : AbstractValidator<AddMessageCommand>
    {
        public AddMessageCommandValidator() 
        {
            RuleFor(c => c.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.EventId)
                .NotEmpty()
                .WithMessage("Event id is required!");

            RuleFor(c => c.Text)
                .NotEmpty()
                .WithMessage("Not empty message text is required!");
        }
    }
}
