using FluentValidation;

namespace Application.Events.Commands.AddEventPhoto
{
    public class AddEventPhotoCommandValidator : AbstractValidator<AddEventPhotoCommand>
    {
        public AddEventPhotoCommandValidator() 
        {
            RuleFor(c => c.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.EventId)
                .NotEmpty()
                .WithMessage("Event id is required!");
            
            RuleFor(c => c.Photo)
                .NotEmpty()
                .WithMessage("Photo is required!");
        }  
    }
}
