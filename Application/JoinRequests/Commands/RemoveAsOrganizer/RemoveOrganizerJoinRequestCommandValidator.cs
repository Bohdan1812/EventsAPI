using FluentValidation;

namespace Application.JoinRequests.Commands.RemoveAsOrganizer
{
    public class RemoveOrganizerJoinRequestCommandValidator 
        : AbstractValidator<RemoveOrganizerJoinRequestCommand>
    {
        public RemoveOrganizerJoinRequestCommandValidator() 
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
