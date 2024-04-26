namespace Domain.EventAggregate.Exceptions
{
    public class SubEventNotFoundException : Exception
    {
        public SubEventNotFoundException()
            : base("Event does not contain such subevent!")
        { }
    }
}
