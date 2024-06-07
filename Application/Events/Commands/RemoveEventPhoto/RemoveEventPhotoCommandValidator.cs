using FluentValidation;

namespace Application.Events.Commands.RemoveEventPhoto
{
    public class RemoveEventPhotoCommandValidator : AbstractValidator<RemoveEventPhotoCommand>
    {
        public RemoveEventPhotoCommandValidator() 
        {
            RuleFor(c => c.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.EventId)
                .NotEmpty()
                .WithMessage("Event id is required!");
        }
    }
}
