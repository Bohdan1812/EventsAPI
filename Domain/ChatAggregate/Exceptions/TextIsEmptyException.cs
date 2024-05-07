namespace Domain.ChatAggregate.Exceptions
{
    public class TextIsEmptyException : Exception
    {
        public TextIsEmptyException()
            : base("Text of the message must not be empty!")
        { }
    }
}
