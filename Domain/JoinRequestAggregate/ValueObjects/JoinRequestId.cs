using Domain.Common.Models;

namespace Domain.JoinRequestAggregate.ValueObjects
{
    public sealed class JoinRequestId : ValueObject
    {
        public Guid Value { get; }

        private JoinRequestId(Guid value)
        {
            Value = value;
        }

        public static JoinRequestId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static JoinRequestId Create(Guid value) 
        {
            return new(value);
        }
    }
}
