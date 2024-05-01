namespace Domain.InviteAggregate.Exceptions
{
    public class InviteExistException : Exception
    {
        public InviteExistException()
            : base("You already invited this user to event!")
        { }
    }
}
