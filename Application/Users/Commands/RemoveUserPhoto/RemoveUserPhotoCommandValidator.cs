using FluentValidation;

namespace Application.Users.Commands.RemoveUserPhoto
{
    public class RemoveUserPhotoCommandValidator : AbstractValidator<RemoveUserPhotoCommand>
    {
        public RemoveUserPhotoCommandValidator() 
        {
            RuleFor(c => c.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUSer id is required!");
        }
    }
}
