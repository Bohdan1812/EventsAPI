using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Events.Commands.SubEventCommands.RemoveSubEvent
{
    public record RemoveSubEventCommand(
        Guid ApplicationUserId,
        Guid EventId,
        Guid SubEventId) : IRequest<ErrorOr<string>>;
}
