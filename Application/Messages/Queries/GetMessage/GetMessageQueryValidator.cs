using FluentValidation;

namespace Application.Messages.Queries.GetMessage
{
    public class GetMessageQueryValidator : AbstractValidator<GetMessageQuery>
    {
        public GetMessageQueryValidator()
        {
            RuleFor(c => c.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(c => c.MessageId)
                .NotEmpty()
                .WithMessage("Message id is required!");
        }
    }
}
