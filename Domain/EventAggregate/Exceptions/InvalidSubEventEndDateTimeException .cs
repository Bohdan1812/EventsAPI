namespace Domain.EventAggregate.Exceptions
{
    public class InvalidSubEventEndDateTimeException : Exception
    {
        public InvalidSubEventEndDateTimeException() 
            : base("End datetime of subevent must be after start datetime!")
        { }
    }
}
