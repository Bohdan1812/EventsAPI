using Application.Messages.Dto;
using Domain.ChatAggregate.Entities;
using ErrorOr;
using MediatR;

namespace Application.Messages.Queries.GetEventMessages
{
    public record GetEventMessagesQuery(Guid ApplicationUserId, Guid EventId) : IRequest<ErrorOr<List<MessageResponseDto>>>;
}
