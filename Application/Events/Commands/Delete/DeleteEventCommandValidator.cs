using FluentValidation;

namespace Application.Events.Commands.Delete
{
    public class DeleteEventCommandValidator : AbstractValidator<DeleteEventCommand>
    {
        public DeleteEventCommandValidator() 
        {
            RuleFor(c => c.appUserId)
                .NotEmpty()
                .WithMessage("ApplicationUseId is required!");

            RuleFor(c => c.eventId)
                .NotEmpty()
                .WithMessage("EventId is required!");
              
        }    
    }
}
