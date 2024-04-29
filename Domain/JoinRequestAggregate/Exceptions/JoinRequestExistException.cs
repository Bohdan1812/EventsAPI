
namespace Domain.JoinRequestAggregate.Exceptions
{
    public class JoinRequestExistException : Exception
    {
        public JoinRequestExistException() 
            : base("This joinRequest is already exists!")
        { }
    }
}
