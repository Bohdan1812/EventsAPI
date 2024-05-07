using ErrorOr;

namespace Application.Common.Errors
{
    public static class ChatError
    {
        public static Error MessageNotInitialized(string description) => Error.Validation(
           code: "Chat.Message.NotInitialized",
           description: description);

        public static Error ChatNotFound => Error.NotFound(
            code: "Chat.NotFound",
            description: "Chat was not found!");

        public static Error MessageNotAddedToChat(string description) => Error.Validation(
            code: "Chat.Message.NotAddedToChat",
            description: description);

        public static Error MessageNotAdded() => Error.Failure(
            code: "Chat.Message.NotAdded",
            description: "Unexpected error occured. This message was not added to database!");
    }
}
