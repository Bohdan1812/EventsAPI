
using FluentValidation;

namespace Application.JoinRequests.Queries.GetEventJoinRequests
{
    public class GetEventJoinRequestsQueryValidator : AbstractValidator<GetEventJoinRequestsQuery>
    {
        public GetEventJoinRequestsQueryValidator() 
        {
            RuleFor(q => q.ApplicatioUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(q => q.EventId)
                .NotEmpty()
                .WithMessage("Event id is required!");
        }
    }
}
