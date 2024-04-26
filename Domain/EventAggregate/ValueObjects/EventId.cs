using Domain.Common.Models;

namespace Domain.EventAggregate.ValueObjects
{
    public sealed class EventId : ValueObject
    {
        public Guid Value { get; private set; }

        private EventId(Guid value)
        {
            Value = value;
        }

        public static EventId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public static EventId Create(Guid value)
        {
            return new EventId(value);
        }
    }
}
