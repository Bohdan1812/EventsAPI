namespace Domain.EventAggregate.Exceptions
{
    public class EventTimeViolationException : Exception
    {
        public EventTimeViolationException()
            : base("A sub-event must start after or at the start of the event " +
                  "and end before or at the end of the event!")
        { }

    }
}
