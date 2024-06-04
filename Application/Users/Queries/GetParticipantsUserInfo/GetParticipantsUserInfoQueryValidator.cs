using FluentValidation;

namespace Application.Users.Queries.GetParticipantsUserInfo
{
    public class GetParticipantsUserInfoQueryValidator : AbstractValidator<GetParticipantsUserInfoQuery>
    {
        public GetParticipantsUserInfoQueryValidator() 
        {
            RuleFor(q => q.ApplicatinUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(q => q.EventId)
                .NotEmpty()
                .WithMessage("Event id is required!");
        }
    }
}
