using ErrorOr;

namespace Application.Common.Errors
{
    public static class InviteError
    {
        public static Error InviteNotInitialized(string description) => Error.Validation(
          code: "Invite.NotInitialized",
          description: description);
        public static Error InviteNotFound => Error.NotFound(
           code: "Invite.NotFound",
           description: "This invite was not found!");

        public static Error InviteNotAdded => Error.Failure(
            code: "Invite.NotAdded",
            description: "Unexpected error occured. This invite was not added to database!");
  
        public static Error InviteNotRemoved => Error.Failure(
           code: "Invite.NotRemoved",
           description: "Unexpected error occured. This invite was not removed to database!");

        public static Error InviteNoPermission => Error.Validation(
            code: "Invite.NoPermission",
            description: "Current user have no permission to this invite!");
    }
}
