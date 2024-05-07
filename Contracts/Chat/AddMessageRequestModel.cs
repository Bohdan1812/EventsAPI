namespace Contracts.Chat
{
    public record AddMessageRequestModel(Guid EventId, string Text);
}
