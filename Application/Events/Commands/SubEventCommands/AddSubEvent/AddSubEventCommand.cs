using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Events.Commands.SubEventCommands.AddSubEvent
{
    public record AddSubEventCommand(
        Guid ApplicationUserId,
        Guid EventId,
        string Name,
        string? Description,
        DateTime StartDateTime,
        DateTime EndDateTime
        ) : IRequest<ErrorOr<string>>;
}
