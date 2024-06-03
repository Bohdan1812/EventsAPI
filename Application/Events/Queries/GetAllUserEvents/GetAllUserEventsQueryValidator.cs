using FluentValidation;

namespace Application.Events.Queries.GetAllUserEvents
{
    public class GetAllUserEventsQueryValidator : AbstractValidator<GetAllUserEventsQuery>
    {
        public GetAllUserEventsQueryValidator() 
        {
            RuleFor(q => q.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(q => q.StartDateTime)
                .NotEmpty()
                .WithMessage("Start datetime is required!");
        }
    }
}
