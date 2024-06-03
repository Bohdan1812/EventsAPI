namespace Domain.InviteAggregate.Exceptions
{
    public class InviteNoPersmissionException : Exception
    {
        public InviteNoPersmissionException() 
            : base("You have no permission to invite users!") 
        { }
    }
}
