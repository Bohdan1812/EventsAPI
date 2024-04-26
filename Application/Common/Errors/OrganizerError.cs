using ErrorOr;

namespace Application.Common.Errors
{
    public static class OrganizerError
    {
        public static Error OrganizerNotFound => Error.NotFound(
           code: "Organizer.NotFound",
           description: "This organizer was not found!");
    }
}
