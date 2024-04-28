namespace Domain.EventAggregate.Exceptions
{
    public class SubEventAlreadyExistsException : Exception
    {
        public SubEventAlreadyExistsException() 
            : base("SubEvent is already in Event!")
        { }
    }
}
