using FluentValidation;

namespace Application.Users.Queries.GetCurrentUserInfo
{
    public class GetCurrentUserInfoQueryValidator : AbstractValidator<GetCurrentUserInfoQuery>
    {
        public GetCurrentUserInfoQueryValidator() 
        {
            RuleFor(q => q.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicatioUser id is required!");
        }   
    }
}
