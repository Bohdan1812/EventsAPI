using FluentValidation;

namespace Application.JoinRequests.Queries.GetOwnJoinRequests
{
    public class GetOwnJoinRequestsValidator : AbstractValidator<GetOwnJoinRequestsQuery>
    {
        public GetOwnJoinRequestsValidator()
        {
            RuleFor(q => q.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");
        }
    }
}
