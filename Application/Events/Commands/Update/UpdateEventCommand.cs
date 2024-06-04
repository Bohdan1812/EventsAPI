using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Events.Commands.Update
{
    public record UpdateEventCommand(
        Guid ApplicationUserId,
        Guid EventId,
        string Name, 
        string Description,
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
