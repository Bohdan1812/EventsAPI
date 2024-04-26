using Domain.Common.Models;

namespace Domain.OrganizerAggregate.ValueObjects
{
    public sealed class OrganizerId : ValueObject
    {
        public Guid Value { get; private set; }
        
        private OrganizerId(Guid value)
        { 
            Value = value; 
        }

        public static OrganizerId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public static OrganizerId Create(Guid value)
        {
            return new OrganizerId(value);
        }
    }
}
