
namespace Domain.JoinRequestAggregate.Exceptions
{
    public class JoinRequestExistException : Exception
    {
        public JoinRequestExistException() 
            : base("This user already requested to join this event!")
        { }
    }
}
