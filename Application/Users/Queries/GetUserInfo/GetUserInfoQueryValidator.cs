using FluentValidation;

namespace Application.Users.Queries.GetUserInfo
{
    public class GetUserInfoQueryValidator : AbstractValidator<GetUserInfoQuery>
    {
        public GetUserInfoQueryValidator() 
        {
            RuleFor(q => q.UserId)
                .NotEmpty()
                .WithMessage("User id is required!");
        }
    }
}
