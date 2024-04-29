using ErrorOr;

namespace Application.Common.Errors
{
    public static class JoinRequestError
    {
        public static Error JoinRequestNotInitialized(string description) => Error.Validation(
          code: "JoinRequest.NotInitialized",
          description: description);
        public static Error JoinRequestNotFound => Error.NotFound(
           code: "JoinRequest.NotFound",
           description: "This join request was not found!");

        public static Error JoinRequestNotAdded => Error.Failure(
            code: "JoinRequest.NotAdded",
            description: "Unexpected error occured. This join request was not added to database!");

        public static Error JoinRequestNoPermission => Error.Validation(
            code: "JoinRequest.NoPermission",
            description: "Current user have no permission to this join request!");
    }
}
