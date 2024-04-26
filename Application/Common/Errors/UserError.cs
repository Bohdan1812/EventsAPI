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
    }
}
