using FluentValidation;

namespace Application.Participations.Queries.GetParticipationsByEvent
{
    public class GetParticipationsByEventQueryValidator : AbstractValidator<GetParticipationsByEventQuery>
    {
        public GetParticipationsByEventQueryValidator() 
        {
            RuleFor(q => q.ApplicationUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser id is required!");

            RuleFor(q => q.EventId)
                .NotEmpty()
                .WithMessage("Event id is required!");
        }
    }
}
