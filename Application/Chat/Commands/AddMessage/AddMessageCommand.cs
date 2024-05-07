using Domain.ChatAggregate.Entities;
using ErrorOr;
using MediatR;

namespace Application.Chat.Commands.AddMessage
{
    public record AddMessageCommand(
        Guid ApplicationUserId,
        Guid EventId,
        string Text) : IRequest<ErrorOr<string>>;
    
}
