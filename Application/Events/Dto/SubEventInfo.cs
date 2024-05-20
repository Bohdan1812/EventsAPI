namespace Application.Events.Dto
{
    public record SubEventInfo(
        Guid Id,
        string Name,
        string? Description,
        DateTime Start,
        DateTime End);
}
