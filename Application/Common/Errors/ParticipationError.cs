using ErrorOr;

namespace Application.Common.Errors
{
    public static class ParticipationError
    {
        public static Error ParticipationNotInitialized(string description) => Error.Validation(
          code: "Participation.NotInitialized",
          description: description);
        public static Error ParticipationNotFound => Error.NotFound(
           code: "Participation.NotFound",
           description: "This participation was not found!");

        public static Error ParticipationNotAdded => Error.Failure(
            code: "Participation.NotAdded",
            description: "Unexpected error occured. This participation was not added to database!");

        public static Error ParticipationNotRemoved => Error.Failure(
           code: "Participation.NotRemoved",
           description: "Unexpected error occured. This participation was not removed to database!");

        public static Error ParticipationNoPermission => Error.Validation(
            code: "Participation.NoPermission",
            description: "Current user have no permission to this participation!");
    }
}
