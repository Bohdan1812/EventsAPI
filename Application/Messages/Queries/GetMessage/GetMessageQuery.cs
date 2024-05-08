using Domain.ChatAggregate.Entities;
using ErrorOr;
using MediatR;

namespace Application.Messages.Queries.GetMessage
{
    public record GetMessageQuery(
        Guid ApplicationUserId,
        Guid MessageId) : IRequest<ErrorOr<Message>>;
}
