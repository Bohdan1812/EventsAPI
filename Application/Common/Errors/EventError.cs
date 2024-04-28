using Domain.EventAggregate.Entities;
using ErrorOr;

namespace Application.Common.Errors
{
    public static class EventError
    {
        public static Error EventNotInitialized(string description) => Error.Validation(
           code: "Event.NotInitialized",
           description: description);

        public static Error SubEventNotUpdated(string description) => Error.Validation(
           code: "Event.SubEventNotUpdated",
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

        public static Error EventNotUpdated => Error.Failure(
            code: "Event.NotUpdated",
            description: "Unexpected error occured. This event was not updated in database!");

        

        public static class SubEventError
        {
            public static Error SubEventNotFound => Error.NotFound(
               code: "SubEvent.NotFound",
               description: "This subEvent was not found!");

            public static Error SubEventNotAddedDb => Error.Failure(
                code: "Event.SubEvent.NotAddedInDb",
                description: "SubEvent was not added to database!");

            public static Error SubEventNotInitialized(string description) => Error.Validation(
               code: "SubEvent.NotInitialized",
               description: description);

            public static Error SubEventNotAdded(string description) => Error.Validation(
               code: "Event.SubEvent.NotAddedToEvent",
               description: description);

            public static Error SubEventNotUpdated(string description) => Error.Validation(
                code: "Event.SubEvent.NotUpdatedInEvent",
                description: description);

            public static Error SubEventNotUpdatedDb => Error.Failure(
                code: "Event.SubEvent.NotUpdatedInDb",
                description: "SubEvent was not updated in database!");
            public static Error SubEventNotDeleted(string description) => Error.Validation(
                code: "Event.SubEvent.DeletedInEvent",
                description: description);

            public static Error SubEventNotDeletedDb => Error.Failure(
                code: "Event.SubEvent.NotDeletedInDb",
                description: "SubEvent was not Deleted in database!");
        }
    }
}
