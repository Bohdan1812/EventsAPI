using Domain.Common.Models;
using Domain.EventAggregate.ValueObjects;
using Domain.OrganizerAggregate.ValueObjects;
using Domain.UserAggregate.ValueObjects;

namespace Domain.OrganizerAggregate
{
    public sealed class Organizer : AggregateRoot<OrganizerId>
    {
        #pragma warning disable CS8618
        private Organizer()
        {

        }
        #pragma warning restore CS8618

        private readonly List<EventId> _eventIds = new List<EventId>();

        public UserId UserId { get; }

        public IReadOnlyCollection<EventId> EventIds => _eventIds.AsReadOnly();

        public Organizer(OrganizerId organizerId, UserId userId ) 
            : base(organizerId)
        { 
            UserId = userId;
        }

        public void AddEventId( EventId eventId )
        {
            _eventIds.Add( eventId );
        }

        public void RemoveEventId( EventId eventId ) 
        {
            if (!_eventIds.Contains(eventId))
            {
                throw new Exception("There is no such event in your list");
            }

            _eventIds.Remove(eventId);
        }

    }
}
