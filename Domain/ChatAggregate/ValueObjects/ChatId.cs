using Domain.Common.Models;

namespace Domain.ChatAggregate.ValueObjects
{
    public sealed class ChatId : ValueObject
    {
        public Guid Value { get; private set; }

        private ChatId(Guid value)
        {
            Value = value;
        }

        public static ChatId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public static ChatId Create(Guid value)
        {
            return new ChatId(value);
        }
    }
}
