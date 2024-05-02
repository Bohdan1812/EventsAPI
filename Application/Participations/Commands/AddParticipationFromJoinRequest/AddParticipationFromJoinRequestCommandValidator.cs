using FluentValidation;

namespace Application.Participations.Commands.AddParticipationFromJoinRequest
{
    public class AddParticipationFromJoinRequestCommandValidator
        : AbstractValidator<AddParticipationFromJoinRequestCommand>
    {
        public AddParticipationFromJoinRequestCommandValidator()
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
