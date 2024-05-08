using Domain.ChatAggregate.Entities;
using Domain.EventAggregate.ValueObjects;
using Domain.MessageAggregate.ValueObjects;
using Domain.ParticipationAggregate;

namespace Application.Persistence.Repositories
{
    public interface IMessageRepository
    {
        Task AddMessage(Message message);
        Task UpdateMessage(Message message);
        Task DeleteMessage(MessageId messageId);
        Task<Message?> GetMessage(MessageId messageId);
        Task<List<Message>> GetEventMessages(EventId eventId);
    }
}
