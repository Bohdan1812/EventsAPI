using FluentValidation;

namespace Application.Users.Queries.FindUsers
{
    public class FindUsersQueryValidator : AbstractValidator<FindUsersQuery>
    {
        public FindUsersQueryValidator() 
        {
            RuleFor(q => q.Email)
                .NotEmpty()
                .WithMessage("Email or first name or last name is required!")
                .When(q => string.IsNullOrEmpty(q.FirstName) && string.IsNullOrEmpty(q.LastName));
        }    
    }
}
