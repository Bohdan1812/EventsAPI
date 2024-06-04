using Domain.OrganizerAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Events.Commands.Create
{
    public record CreateEventCommand(
        Guid appUserId,
        string Name,
        string Description,
        List<SubEventCommand> SubEvents,
        DateTime StartDateTime,
        DateTime EndDateTime,
        AddressCommand Address,
        LinkCommand Link,
        bool IsPrivate,
        bool AllowParticipantsInvite) : IRequest<ErrorOr<string>>;

    public record SubEventCommand(
        string Name,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime);

    public record AddressCommand(
        string AddressName,
        double Longitude,
        double Latitude);

    public record LinkCommand(
        string Link);


}
