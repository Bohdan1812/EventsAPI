using Domain.Common.Models;

namespace Domain.ParticipationAggregate.ValueObjects
{
    public sealed class ParticipationId : ValueObject
    {
        public Guid Value { get; }
        private ParticipationId(Guid value)
        {
            Value = value;
        }
        public static ParticipationId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
