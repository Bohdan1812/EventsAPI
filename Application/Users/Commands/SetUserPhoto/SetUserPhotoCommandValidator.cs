using FluentValidation;

namespace Application.Users.Commands.SetUserPhoto
{
    public class SetUserPhotoCommandValidator : AbstractValidator<SetUserPhotoCommand>
    {
        public SetUserPhotoCommandValidator() 
        {
            RuleFor(c => c.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.Photo)
                .NotEmpty()
                .WithMessage("Photo is required!");
        }
    }
}
