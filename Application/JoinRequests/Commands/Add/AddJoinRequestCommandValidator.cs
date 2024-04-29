using FluentValidation;

namespace Application.JoinRequests.Commands.Add
{
    public class AddJoinRequestCommandValidator : AbstractValidator<AddJoinRequestCommand>
    {
        public AddJoinRequestCommandValidator()
        {
            RuleFor(c => c.ApplicatioUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.EventId)
                .NotEmpty()
                .WithMessage("Event id is required!");
        }
    }
}
