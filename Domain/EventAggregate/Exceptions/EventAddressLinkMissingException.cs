namespace Domain.EventAggregate.Exceptions
{
    public class EventAddressLinkMissingException : Exception
    {
        public EventAddressLinkMissingException() : base("Address and link are missing! 1 of them must be defined")
        { }
    }
}
