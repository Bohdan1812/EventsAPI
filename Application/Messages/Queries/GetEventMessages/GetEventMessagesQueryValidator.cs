using FluentValidation;

namespace Application.Messages.Queries.GetEventMessages
{
    public class GetEventMessagesQueryValidator : AbstractValidator<GetEventMessagesQuery>  
    {
        public GetEventMessagesQueryValidator() 
        {
            RuleFor(q => q.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(q => q.EventId)
                .NotEmpty()
                .WithMessage("Event id is required!");
        }
    }
}
