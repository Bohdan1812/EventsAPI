namespace Contracts.Message
{
    public record UpdateMessageRequestModel(Guid MessageId, string Text);
}
