namespace Domain.InviteAggregate.Exceptions
{
    public class InviteExistException : Exception
    {
        public InviteExistException()
            : base("User is already invited to event!")
        { }
    }
}
