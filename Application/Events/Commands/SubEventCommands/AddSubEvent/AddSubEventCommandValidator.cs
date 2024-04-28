using FluentValidation;

namespace Application.Events.Commands.SubEventCommands.AddSubEvent
{
    public class AddSubEventCommandValidator : AbstractValidator<AddSubEventCommand>
    {
        public AddSubEventCommandValidator()
        {
            RuleFor(c => c.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.EventId)
                .NotEmpty()
                .WithMessage("Event id is required!");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("SubEvent name is required!");

            RuleFor(e => e.StartDateTime)
                .NotEmpty()
                .WithMessage("SubEvent start datetime is required!");

            RuleFor(e => e.EndDateTime)
                .NotEmpty()
                .WithMessage("SubEvent end datetime is required!")
                .GreaterThan(e => e.StartDateTime)
                .WithMessage("SubEvent's end datetime must be greater then start datetime!");
        }
    }
}
