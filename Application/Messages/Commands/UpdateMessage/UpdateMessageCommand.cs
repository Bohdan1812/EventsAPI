using ErrorOr;
using MediatR;

namespace Application.Messages.Commands.UpdateMessage
{
    public record UpdateMessageCommand(
        Guid ApplicationUserId,
        Guid MessageId,
        string Text) : IRequest<ErrorOr<string>>;
}
