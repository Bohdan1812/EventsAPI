namespace Domain.ChatAggregate.Exceptions
{
    public class InvalidAuthorException : Exception
    {
        public InvalidAuthorException()
            : base("Author of message is not participated in event!")
        { }
    }
}
