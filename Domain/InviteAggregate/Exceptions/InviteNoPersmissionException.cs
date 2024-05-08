namespace Domain.InviteAggregate.Exceptions
{
    public class InviteNoPersmissionException : Exception
    {
        public InviteNoPersmissionException() 
            : base("You are not Organizer of event! You have no permission to invite users!") 
        { }
    }
}
