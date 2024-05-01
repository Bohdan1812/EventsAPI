namespace Domain.InviteAggregate.Exceptions
{
    public class JoinRequestExistException : Exception
    {
        public JoinRequestExistException() 
            : base("Invited user already created join request! Accept the request.") 
        { }
    }
}
