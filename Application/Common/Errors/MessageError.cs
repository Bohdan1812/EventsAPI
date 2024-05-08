using ErrorOr;

namespace Application.Common.Errors
{
    public static class MessageError
    {
        public static Error MessageNotInitialized(string description) => Error.Validation(
           code: "Message.NotInitialized",
           description: description);

        public static Error MessageNotUpdated(string description) => Error.Validation(
           code: "Message.NotUpdated",
           description: description);

        public static Error MessageNoPermission => Error.Validation(
            code: "Message.NoPermission",
            description: "User has no permission to this message!");

        public static Error MessageNotFound => Error.NotFound(
            code: "Message.NotFound",
            description: "Message was not found!");

        public static Error MessageNotAdded => Error.Failure(
            code: "Message.NotAdded",
            description: "Unexpected error occured. This message was not added to database!");

        public static Error MessageNotUpdatedInDb => Error.Failure(
            code: "Message.NotUpdated",
            description: "Unexpected error occured. This message was not updated in database!");

        public static Error MessageNotDeleted => Error.Failure(
           code: "Message.NotDeleted",
           description: "Unexpected error occured. This message was not deleted in database!");
    }
}
