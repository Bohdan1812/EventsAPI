using ErrorOr;
using MediatR;

namespace Application.Messages.Commands.AddMessage
{
    public record AddMessageCommand(
        Guid ApplicationUserId,
        Guid ParticipationId,
        string Text) : IRequest<ErrorOr<string>>;
}
