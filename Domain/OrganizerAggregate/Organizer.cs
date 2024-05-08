using Domain.Common.Models;
using Domain.EventAggregate;
using Domain.OrganizerAggregate.ValueObjects;
using Domain.UserAggregate;
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

        private readonly List<Event> _events = [];
        public UserId UserId { get; private set; } = null!;
        public User User { get; private set; } = null!;

        public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

        public Organizer(User user) 
            : base(OrganizerId.CreateUnique())
        {
            if (user.Organizer is not null)
                throw new Exception("organizer already exists");
            User = user;
        }

        public void AddEvent(UserId userId, Event @event)
        {
            if (User.Id != userId)
                throw new Exception();

            _events.Add(@event);
        }

        public void RemoveEvent(UserId userId, Event @event) 
        {
            if(User.Id == userId)

            if (!_events.Contains(@event))
            {
                throw new Exception("There is no such event in your list");
            }

            _events.Remove(@event);
        }

    }
}
