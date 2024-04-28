using Domain.Common.Models;
using Domain.EventAggregate.Entities;
using Domain.EventAggregate.Exceptions;
using Domain.EventAggregate.ValueObjects;
using Domain.InviteAggregate.ValueObjects;
using Domain.OrganizerAggregate.ValueObjects;
using Domain.ParticipationAggregate.ValueObjects;
using Domain.ParticipationRequestAggregate.ValueObjects;

namespace Domain.EventAggregate
{
    public sealed class Event : AggregateRoot<EventId>
    {
#pragma warning disable CS8618
        private Event()
        {

        }
#pragma warning restore CS8618

        //private readonly List<ParticipationId> _participationIds = new List<ParticipationId>();

        //private readonly List<ParticipationRequestId> _participationRequestIds = new List<ParticipationRequestId>();

        //private readonly List<InviteId> _inviteIds = new List<InviteId>();

        private readonly List<SubEvent> _subEvents = new List<SubEvent>();

        public string Name { get; private set; } = null!;

        public string? Description { get; private set; }

        public OrganizerId OrganizerId { get; private set; }

        //public IReadOnlyList<ParticipationId> ParticipationIds => _participationIds.AsReadOnly();

        //public IReadOnlyList<ParticipationRequestId> ParticipationRequestIds => _participationRequestIds.AsReadOnly();

        //public IReadOnlyList<InviteId> InviteIds => _inviteIds.AsReadOnly();

        public IReadOnlyList<SubEvent> SubEvents => _subEvents.AsReadOnly();

        public Address? Address { get; private set; }

        public Link? Link { get; private set; }

        public DateTime StartDateTime { get; private set; }
        private DateTime _endDateTime;

        public DateTime EndDateTime {
            get
            {
                return _endDateTime;
            }
            private set
            {
                if (value <= StartDateTime)
                {
                    throw new InvalidEventEndDateTimeException();
                }
                else
                {
                    _endDateTime = value;
                }
            }
        }
        public DateTime CreatedDateTime { get; private set; }

        public DateTime UpdatedDateTime { get; private set; }

        public Event(
            EventId eventId,
            string name,
            string description,
            OrganizerId organizerId,
            DateTime startDateTime,
            DateTime endDateTime,
            List<SubEvent> subEvents,
            Address? address,
            Link? link)
            : base(eventId)
        {
            if (address is null && link is null)
            {
                throw new EventAddressLinkMissingException();
            }

            Name = name;
            Description = description;
            OrganizerId = organizerId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            _subEvents = subEvents;
            Address = address;
            Link = link;
            CreatedDateTime = DateTime.UtcNow;
            UpdatedDateTime = DateTime.UtcNow;
        }

        public void AddSubEvent(SubEvent newSubEvent)
        {
            if (newSubEvent.StartDateTime < StartDateTime ||
                    newSubEvent.EndDateTime > EndDateTime)
                throw new EventTimeViolationException();

            if (_subEvents.Exists(s => s.Id == newSubEvent.Id))
                throw new SubEventAlreadyExistsException();

            _subEvents.Add(newSubEvent);

            UpdatedDateTime = DateTime.UtcNow;
        }

        public void RemoveSubEvent(SubEventId subEventId)
        {
            var deletedSubEvent = SubEvents.FirstOrDefault(se => se.Id == subEventId);

            if (deletedSubEvent is null)
            {
                throw new SubEventNotFoundException();
            }

            _subEvents.Remove(deletedSubEvent);

            UpdatedDateTime = DateTime.UtcNow;
        }

        public void UpdateSubEvent(SubEvent subEvent)
        {
            var updatedSubEvent = SubEvents.FirstOrDefault(se => se.Id == subEvent.Id);

            if (updatedSubEvent is null)
            {
                throw new SubEventNotFoundException();
            }

            if (updatedSubEvent.StartDateTime < StartDateTime ||
                updatedSubEvent.EndDateTime > EndDateTime)
            {
                throw new EventTimeViolationException();
            }

            _subEvents.Remove(updatedSubEvent);
            _subEvents.Add(subEvent);

            UpdatedDateTime = DateTime.UtcNow;
        }

        public void Update(
            string name,
            string? description,
            DateTime startDateTime,
            DateTime endDateTime,
            Address? address,
            Link? link)
        {
            Name = name;
            Description = description;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Address = address;
            Link = link;
            UpdatedDateTime = DateTime.UtcNow;
        }

        public void AddParticipationId(ParticipationId participationId)
        {
            /*if (ParticipationIds.Contains(participationId))
            {
                throw new Exception("This particpation is already exist");
            }

            _participationIds.Add(participationId);*/
            UpdatedDateTime = DateTime.UtcNow;
        }

        public void RemoveParticipationId(ParticipationId participationId)
        {
            /*
            if (!ParticipationIds.Contains(participationId))
            {
                throw new Exception("This particpation is not exist");
            }

            _participationIds.Remove(participationId);
            */
            UpdatedDateTime = DateTime.UtcNow;
        }
    }
}
