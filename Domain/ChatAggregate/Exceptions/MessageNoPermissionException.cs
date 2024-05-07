namespace Domain.ChatAggregate.Exceptions
{
    public class MessageNoPermissionException : Exception
    {
        public MessageNoPermissionException()
            : base("User cannot edit this message!")
        { }   
    }
}
