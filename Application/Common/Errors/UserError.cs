using ErrorOr;

namespace Application.Common.Errors
{
    public static class UserError
    {
        public static Error UserDeleteWrongPassword => Error.Validation(
            code: "User.DeleteWrongPassword",
            description: "Wrong password! User is not deleted!");

        public static Error UserNotFound => Error.NotFound(
           code: "User.NotFound",
           description: "This user was not found!");

        public static Error UserNotUpdated => Error.Failure(
            code: "User.NotUpdated",
            description: "Unexpected error occured. This user was not updated in database!");
        public static Error UserPhotoDeleteError(string message) => Error.Failure(
           code: "User.PhotoNotDeleted",
           description: message);

        public static Error UserPreviousPhotoNotDeleted => Error.Failure(
           code: "User.PreviousPhotoNotDeleted",
           description: "Previous photo not deleted");

        public static Error UserPhotoUploadError(string message) => Error.Failure(
           code: "User.PhotoNotUploaded",
           description: message);

        public static Error PhotoNotUploaded => Error.Failure(
           code: "User.PhotoNotUploaded",
           description: "New photo did not uploaded");

        public static Error UserPhotoNotFound => Error.NotFound(
           code: "User.PhotoNotFound",
           description: "This user's photo was not found!");
    }
}
