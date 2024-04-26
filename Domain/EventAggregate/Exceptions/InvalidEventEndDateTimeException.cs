namespace Domain.EventAggregate.Exceptions
{
    public class InvalidEventEndDateTimeException : Exception
    {
        public InvalidEventEndDateTimeException() : base("End datetime of event must be after start datetime!")
        {}
    }
}
