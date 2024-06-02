using FluentValidation;

namespace Application.Users.Queries.GetUserByParticipation
{
    public class GetUserByParticipationQueryValidator : AbstractValidator<GetUserByParticipationQuery>
    {
        public GetUserByParticipationQueryValidator() 
        {
            RuleFor(q => q.ParticipationId)
                .NotEmpty()
                .WithMessage("Participation id is required!");
        }
    }
}
