namespace Domain.MessageAggregate.Exceptions
{
    public class MessageNoPermissionException : Exception
    {
        public MessageNoPermissionException()
            : base("This user have no permission to this message!")
        { }
    }
}
