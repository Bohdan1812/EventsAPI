using ErrorOr;
using MediatR;

namespace Application.Events.Commands.Delete
{
    public record DeleteEventCommand (
        Guid appUserId,
        Guid eventId) : IRequest<ErrorOr<string>>;
}
