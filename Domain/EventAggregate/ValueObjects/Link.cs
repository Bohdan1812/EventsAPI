using Domain.Common.Models;

namespace Domain.EventAggregate.ValueObjects
{
    public sealed class Link : ValueObject
    {
        public string Value { get; private set; }

        public Link(string value)
        {
            Value = value;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
      
    }
}
