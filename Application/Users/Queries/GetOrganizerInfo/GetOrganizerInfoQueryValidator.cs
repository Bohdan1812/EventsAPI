using FluentValidation;

namespace Application.Users.Queries.GetOrganizerInfo
{
    public class GetOrganizerInfoQueryValidator : AbstractValidator<GetOrganizerInfoQuery>
    {
        public GetOrganizerInfoQueryValidator() 
        {
            RuleFor(q => q.OrganizerId).NotEmpty().WithMessage("Organizer id is required!");
        }
    }
}
