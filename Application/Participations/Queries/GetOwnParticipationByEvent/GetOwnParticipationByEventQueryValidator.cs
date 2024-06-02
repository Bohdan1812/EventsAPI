using FluentValidation;

namespace Application.Participations.Queries.GetOwnParticipationByEvent
{
    public class GetOwnParticipationByEventQueryValidator : AbstractValidator<GetOwnParticipationByEventQuery>
    {
        public GetOwnParticipationByEventQueryValidator()
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
