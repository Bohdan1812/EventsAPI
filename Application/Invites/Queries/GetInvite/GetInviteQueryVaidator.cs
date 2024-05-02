using FluentValidation;

namespace Application.Invites.Queries.GetInvite
{
    public class GetInviteQueryVaidator : AbstractValidator<GetInviteQuery>
    {
        public GetInviteQueryVaidator() 
        {
            RuleFor(q => q.ApplicatioUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(q => q.InviteId)
                .NotEmpty()
                .WithMessage("Invite id is required!");
        }
    }
}
