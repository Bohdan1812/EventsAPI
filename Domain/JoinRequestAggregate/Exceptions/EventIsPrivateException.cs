
namespace Domain.JoinRequestAggregate.Exceptions
{
    public class EventIsPrivateException : Exception
    {
        public EventIsPrivateException()
            : base("This event is private! You cannot make join request!")    
        { }
    }
}
