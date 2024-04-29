using ErrorOr;

namespace Application.Common.Errors
{
    public static class ApplicationUserError
    {
        public static Error ApplicationUserNotFound => Error.NotFound(
            code: "ApplicationUser.NotFound",
            description: "This applicationUser was not found!");
       
        public static Error DuplicateApplicationUser => Error.Conflict(
            code: "ApplicationUser.DuplicateApplicationUser",
            description: "This applicationUser is already registered");

        public static Error ApplicationUserNotAdded => Error.Failure(
            code: "ApplicationUser.NotAdded",
            description: "Unexpected error occured. This applicationUser was not added to database!");

        public static Error ApplicationUserNotDeleted(string code, string description) => Error.Failure(
          code: code,
          description: description);
    }
}
