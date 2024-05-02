using FluentValidation;

namespace Application.Invites.Queries.GetUserInvites
{
    public class GetUserInvitesQueryValidator : AbstractValidator<GetUserInvitesQuery>
    {
        public GetUserInvitesQueryValidator() 
        {
            RuleFor(q => q.ApplicatioUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");
        }
    }
}
