using FluentValidation;

namespace Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name is required");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email is not valid");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .Matches("[A-Z]")
                .WithMessage("Password must contain 1 uppercase letter")
                .Matches("[a-z]")
                .WithMessage("Password must contain 1 lowercase letter")
                .Matches("[0-9]")
                .WithMessage("Password must contain a number")
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("Password must contain non alphanumeric");
        }
    }
}
