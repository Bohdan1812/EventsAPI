using FluentValidation;

namespace Application.JoinRequests.Commands.RemoveOwn
{
    public class RemoveOwnJoinRequestCommandValidator : AbstractValidator<RemoveOwnJoinRequestCommand>
    {
        public RemoveOwnJoinRequestCommandValidator() 
        {
            RuleFor(c => c.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.JoinRequestId)
                .NotEmpty()
                .WithMessage("Join request id is required!");
        }
    }
}
