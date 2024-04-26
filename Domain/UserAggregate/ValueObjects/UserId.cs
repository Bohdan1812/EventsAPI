using Domain.Common.Models;

namespace Domain.UserAggregate.ValueObjects
{
    public sealed class UserId : ValueObject
    {
#pragma warning disable CS8618
        private UserId()
        {

        }
#pragma warning restore CS8618
        public Guid Value { get; } = new();
        private UserId(Guid value)
        {
            Value = value;
        }
        public static UserId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static UserId Create(Guid value)
        {
            return new UserId(value);
        }
    }
}
