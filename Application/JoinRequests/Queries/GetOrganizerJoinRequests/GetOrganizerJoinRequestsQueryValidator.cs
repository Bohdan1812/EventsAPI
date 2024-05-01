using FluentValidation;

namespace Application.JoinRequests.Queries.GetOrganizerJoinRequests
{
    public class GetOrganizerJoinRequestsQueryValidator : AbstractValidator<GetOrganizerJoinRequestsQuery>
    {
        public GetOrganizerJoinRequestsQueryValidator() 
        {
            RuleFor(q => q.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");
        }
    }
}
