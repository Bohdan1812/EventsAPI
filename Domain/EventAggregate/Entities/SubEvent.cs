using Domain.Common.Models;
using Domain.EventAggregate.Exceptions;
using Domain.EventAggregate.ValueObjects;

namespace Domain.EventAggregate.Entities
{
    public sealed class SubEvent : Entity<SubEventId>
    {
#pragma warning disable CS8618
        private SubEvent()
        {

        }
#pragma warning restore CS8618
        public string Name { get; private set; } = null!;
        
        public string? Description { get; private set; }

        public DateTime StartDateTime { get; private set; }

        private DateTime _endDateTime;
        public DateTime EndDateTime
        {
            get
            {
                return _endDateTime;
            }
            private set
            {
                if (value <= StartDateTime)
                {
                    throw new InvalidSubEventEndDateTimeException();
                }
                _endDateTime = value;
            }
        }

        public SubEvent(SubEventId subEventId, string name, string? description, DateTime startDateTime, DateTime endDateTime)
            : base(subEventId)
        {
            Name = name;
            Description = description;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }

    }
}
