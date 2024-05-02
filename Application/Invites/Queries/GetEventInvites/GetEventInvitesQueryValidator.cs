using FluentValidation;

namespace Application.Invites.Queries.GetEventInvites
{
    public class GetEventInvitesQueryValidator : AbstractValidator<GetEventInvitesQuery>
    {
        public GetEventInvitesQueryValidator() 
        {
            RuleFor(q => q.ApplicationUserId)
                .NotEmpty()
                .WithMessage("Application id is required!");

            RuleFor(q => q.EventId)
                .NotEmpty()
                .WithMessage("Event id is required!");
        }
    }
}
