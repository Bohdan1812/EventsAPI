using FluentValidation;

namespace Application.JoinRequests.Queries.GetJoinRequest
{
    public class GetJoinRequestQueryValidator : AbstractValidator<GetJoinRequestQuery>  
    {
        public GetJoinRequestQueryValidator() 
        {
            RuleFor(q => q.ApplicationUserID)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(q => q.JoinRequestId)
                .NotEmpty()
                .WithMessage("Join request id is required!");
        }
    }
}
