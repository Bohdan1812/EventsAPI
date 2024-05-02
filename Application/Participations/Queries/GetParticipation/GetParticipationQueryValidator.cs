using FluentValidation;

namespace Application.Participations.Queries.GetParticipation
{
    public class GetParticipationQueryValidator : AbstractValidator<GetParticipationQuery>
    {
        public GetParticipationQueryValidator() 
        {
            RuleFor(q => q.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(q => q.ParticipationId)
                .NotEmpty()
                .WithMessage("Participation id is required!");
        }
    }
}
