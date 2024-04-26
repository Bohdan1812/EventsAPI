using Domain.Common.Models;

namespace Domain.EventAggregate.ValueObjects
{
    public sealed class SubEventId : ValueObject
    {
        public Guid Value { get; private set; }

        private SubEventId(Guid value)
        {
            Value = value;
        }

        public static SubEventId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public static SubEventId Create(Guid value)
        {
            return new SubEventId(value);
        }
    }
}
