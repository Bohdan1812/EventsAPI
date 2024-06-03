using FluentValidation;

namespace Application.Events.Commands.Create
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            SetEventRules();
            SetEventAddressRules();
            SetEventLinkRules();
            SetSubEventRules();
        }

        private void SetEventRules()
        {
            RuleFor(e => e.Name)
                    .NotEmpty()
                    .WithMessage("Event name is required!");

            RuleFor(e => e.StartDateTime)
                .NotEmpty()
                .WithMessage("Event start datetime is required!");

            RuleFor(e => e.EndDateTime)
                .NotEmpty()
                .WithMessage("Event end datetime is required!")
                .GreaterThan(e => e.StartDateTime)
                .WithMessage("Event's end datetime must be greater then start datetime!");

            RuleFor(e => e.appUserId)
                .NotEmpty()
                .WithMessage("ApplicationUser Id is required!");

            RuleFor(e => e.IsPrivate)
                .NotNull()
                .WithMessage("Is private value is required!");

            RuleFor(e => e.AllowParticipantsInvite)
                .NotNull()
                .WithMessage("Allow participants to invite value is required!");
        }
        private void SetEventAddressRules()
        {
            RuleFor(e => e.Address)
               .NotNull()
               .When(e => e.Link is null)
               .WithMessage("Address or link is required!");

            RuleFor(e => e.Address)
                .ChildRules(address =>
                {
                    address.RuleFor(a => a.Country)
                    .NotEmpty()
                    .WithMessage("Address requires country!");

                    address.RuleFor(a => a.City)
                    .NotEmpty()
                    .WithMessage("Address requires city!");
                })
                .When(e => e.Address is not null);
        }
        private void SetEventLinkRules()
        {
            RuleFor(e => e.Link)
                .NotNull()
                .When(e => e.Address is null)
                .WithMessage("Address or link is required!");

            RuleFor(e => e.Link)
                .ChildRules(link =>
                {
                    link.RuleFor(l => l.Link)
                    .NotEmpty()
                    .WithMessage("Link requires value!")
                    .When(e => e.Link is not null);
                });
                
        }
        private void SetSubEventRules()
        {
            RuleForEach(e => e.SubEvents)
            .Must((dto, subEvent) =>
            {
                return subEvent.StartDateTime >= dto.StartDateTime
                       && subEvent.EndDateTime <= dto.EndDateTime;
            })
            .When(e => e.SubEvents is not null)
            .WithMessage("SubEvent DateTime Start must be greater or equal than " +
            "Event StartDateTime and SubEvent endDateTime less than Event DateTime End.");
        }

    }
}

