using FluentValidation;
using System.Data;

namespace Application.Events.Commands.SubEventCommands.RemoveSubEvent
{
    public class RemoveSubEventCommandValidator : AbstractValidator<RemoveSubEventCommand> 
    {
        public RemoveSubEventCommandValidator() 
        {

            RuleFor(c => c.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.EventId)
                .NotEmpty()
                .WithMessage("Event id is required!");

            RuleFor(c => c.SubEventId)
                .NotEmpty()
                .WithMessage("SubEvent id is required!");


        }
    }
}
