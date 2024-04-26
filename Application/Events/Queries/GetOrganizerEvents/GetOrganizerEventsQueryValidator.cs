using FluentValidation;

namespace Application.Events.Queries.GetUserEvents
{
    public class GetOrganizerEventsQueryValidator : AbstractValidator<GetOrganizerEventsQuery>
    {
        public GetOrganizerEventsQueryValidator() 
        {
            RuleFor(q => q.appUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");
        }
    }
}
