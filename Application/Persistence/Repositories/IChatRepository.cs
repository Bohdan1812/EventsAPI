using Domain.ChatAggregate.ValueObjects;
using Domain.EventAggregate.ValueObjects;
using ChatClass = Domain.ChatAggregate.Chat;

namespace Application.Persistence.Repositories
{
    public interface IChatRepository
    {
        Task Add(ChatClass chat);

        Task Update(ChatClass chat);

        Task Delete(ChatId chatId); 

        Task<ChatClass?> GetChat(ChatId chatId);

        Task<ChatClass?> GetChat(EventId eventId);
    }
}
