using Domain.Common.Models;

namespace Domain.ParticipationRequestAggregate.ValueObjects
{
    public sealed class ParticipationRequestId : ValueObject
    {
        public Guid Value { get; }

        private ParticipationRequestId(Guid value)
        {
            Value = value;
        }

        public static ParticipationRequestId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
