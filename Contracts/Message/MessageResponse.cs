namespace Contracts.Message
{
    public record MessageResponse(
        Guid AuthorId, 
        string Text, 
        DateTime CreatedDateTime, 
        DateTime UpdatedDateTime);
}
