using ErrorOr;

namespace Application.Common.Errors
{
    public static class OrganizerError
    {
        public static Error OrganizerNotFound => Error.NotFound(
           code: "Organizer.NotFound",
           description: "This Organizer was not found!");

        public static Error OrganizerContiansEvents => Error.Conflict(
            code: "Organizer.CanNotBeDeleted",
            description: "Organizer has events. To delete organizer, remove all created events!");
    }
}
