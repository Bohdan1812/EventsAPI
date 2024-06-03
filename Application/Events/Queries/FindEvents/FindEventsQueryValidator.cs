using FluentValidation;

namespace Application.Events.Queries.FindEvents
{
    public class FindEventsQueryValidator : AbstractValidator<FindEventsQuery>
    {
        public FindEventsQueryValidator() 
        {
            RuleFor(q => q.EventSearchQuery)
                .NotEmpty()
                .WithMessage("Event search string query is required!");
        }   
    }
}
