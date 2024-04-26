using FluentValidation;

namespace Application.Users.Commands.Delete;

public class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
{
    public DeleteAccountCommandValidator()
    {
        RuleFor(x => x.AppUserId)
            .NotEmpty()
            .WithMessage("AppliccationUser Id is required!");

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
