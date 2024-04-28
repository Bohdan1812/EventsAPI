using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Events.Commands.SubEventCommands.UpdateSubEvent
{
    public record UpdateSubEventCommand(
        Guid ApplicationUserId,
        Guid EventId,
        Guid SubEventId,
        string Name,
        string? Description,
        DateTime StartDateTime,
        DateTime EndDateTime) : IRequest<ErrorOr<string>>;
}
