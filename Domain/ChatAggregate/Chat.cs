using Domain.ChatAggregate.Entities;
using Domain.ChatAggregate.ValueObjects;
using Domain.Common.Models;
using Domain.EventAggregate.ValueObjects;

namespace Domain.ChatAggregate
{
    public sealed class Chat : AggregateRoot<ChatId>
    {
        private readonly List<Message> _messages = new();

        public EventId EventId { get; } = null!;

        public IReadOnlyList<Message> Messages => _messages.AsReadOnly();

        public Chat(ChatId chatId, EventId eventId) 
            : base(chatId)
        {
            EventId = eventId;
        }

        public void AddMessage(Message message)
        { 
            
        }
        
    }
}
