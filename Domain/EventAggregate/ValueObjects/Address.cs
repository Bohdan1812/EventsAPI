using Domain.Common.Models;

namespace Domain.EventAggregate.ValueObjects
{
    public sealed class Address : ValueObject
    {

        public Address(string house, string street, string city, string state, string country)
        {
            House = house;
            Street = street;
            City = city;
            State = state;
            Country = country;
        }
        public string House { get; private set; }

        public string Street { get; private set; }

        public string City { get; private set; }

        public string State { get; private set; }

        public string Country { get; private set; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Country;
            yield return State;
            yield return City;
            yield return Street;
            yield return House;
        }
    }
}
