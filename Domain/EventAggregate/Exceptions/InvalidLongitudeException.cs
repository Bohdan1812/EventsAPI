namespace Domain.EventAggregate.Exceptions
{
    public class InvalidLongitudeException : Exception
    {
        public InvalidLongitudeException()
            : base ("Invalid longitude value")
        { }  
    }
}
