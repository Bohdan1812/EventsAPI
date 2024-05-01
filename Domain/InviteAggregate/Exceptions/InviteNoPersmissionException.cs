namespace Domain.InviteAggregate.Exceptions
{
    public class InviteNoPersmissionException : Exception
    {
        public InviteNoPersmissionException() 
            : base("You are not organizer of event! You have no permission to invite users!") 
        { }
    }
}
