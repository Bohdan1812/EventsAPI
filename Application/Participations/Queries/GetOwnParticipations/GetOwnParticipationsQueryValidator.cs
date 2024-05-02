using FluentValidation;

namespace Application.Participations.Queries.GetOwnParticipations
{
    public class GetOwnParticipationsQueryValidator : AbstractValidator<GetOwnParticipationsQuery>
    {
        public GetOwnParticipationsQueryValidator() 
        {
            RuleFor(q => q.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");
        }
    }
}
