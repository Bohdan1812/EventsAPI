namespace Domain.MessageAggregate.Exceptions
{
    public class TextIsEmptyException : Exception
    {
        public TextIsEmptyException()
            : base("Message text must not be empty!")
        { }
    }
}
