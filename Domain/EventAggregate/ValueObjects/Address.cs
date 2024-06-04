using Domain.Common.Models;
using Domain.EventAggregate.Exceptions;

namespace Domain.EventAggregate.ValueObjects
{
    public sealed class Address : ValueObject
    {

        public Address(string addressName, double longitude, double latitude)
        {
            AddressName = addressName;
            Longitude = longitude;
            Latitude = latitude;
        }
        private double latitude;
        public string AddressName { get; private set; }
        public double Latitude
        {
            get
            {
                return latitude;
            }
            private set
            {
                if (value < -90 || value > 90)
                    throw new InvalidLatitudeException();
                else
                    latitude = value;
            }
        }
        private double longitude;
        public double Longitude
        {
            get
            {
                return longitude;
            }
            private set 
            {
                if(value < -180 || value > 180)
                    throw new InvalidLongitudeException();  
                else 
                    longitude = value;
            }
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return AddressName;
            yield return Longitude;
            yield return Latitude;
        }
    }
}
