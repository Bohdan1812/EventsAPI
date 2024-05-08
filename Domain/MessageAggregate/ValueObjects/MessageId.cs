using Domain.Common.Models;

namespace Domain.MessageAggregate.ValueObjects
{
    public sealed class MessageId : ValueObject
    {
        public Guid Value { get; private set; }

        private MessageId(Guid value)
        {
            Value = value;
        }

        public static MessageId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public static MessageId Create(Guid value)
        {
            return new MessageId(value);
        }
    }
}
