using ErrorOr;

namespace Application.Common.Errors
{
    public static class EventError 
    {
        public static Error EventNotInitialized(string description) => Error.Validation(
           code: "Event.NotInitialized",
           description: description);

        public static Error EventNotAdded => Error.Failure(
           code: "Event.NotAdded",
           description: "Unexpected error occured. This applicationUser was not added to database!");

        public static Error EventNotFound => Error.NotFound(
           code: "Event.NotFound",
           description: "This event was not found!");

        public static Error EventUpdateNoPermission => Error.Validation(
            code: "Event.NoUpdatePermission",
            description: "This user has no permission to update event!");

        public static Error EventNotDeleted => Error.Failure(
            code: "Event.NotDeleted",
            description: "Unexpected error occured. This event was not deleted from database!");
    }
}
