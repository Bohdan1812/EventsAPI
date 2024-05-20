namespace Application.Events.Dto
{
    public record EventInfo(
        Guid Id,
        string Name,
        string? Description,
        Guid OrganizerId,
        int ParticipationCount,
        DateTime Start,
        DateTime End,
        List<SubEventInfo> SubEvents,
        AddressInfo AddressInfo,
        string? Link);
}
