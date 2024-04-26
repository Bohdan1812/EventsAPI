using FluentValidation;

namespace Application.Events.Queries.GetEvent
{
    public class GetEventQueryValidator : AbstractValidator<GetEventQuery>
    {
        public GetEventQueryValidator() 
        {
            RuleFor(q => q.EventId)
                .NotNull()
                .WithMessage("Event id is required!");
        }
    }
}
