using ErrorOr;
using MediatR;

namespace Application.Messages.Commands.DeleteMessage
{
    public record DeleteMessageCommand(
        Guid ApplicationUserId,
        Guid MessageId) : IRequest<ErrorOr<string>>;
}
