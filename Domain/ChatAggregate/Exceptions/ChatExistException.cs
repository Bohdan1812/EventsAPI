namespace Domain.ChatAggregate.Exceptions
{
    public class ChatExistException : Exception
    {
        public ChatExistException()
            : base("This event already has chat!")
        { }
    }
}
