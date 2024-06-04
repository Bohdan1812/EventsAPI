namespace Domain.EventAggregate.Exceptions
{
    public class InvalidLatitudeException : Exception
    {
        public InvalidLatitudeException()
            : base("Invalid latitude value")
        { }
    }
}
