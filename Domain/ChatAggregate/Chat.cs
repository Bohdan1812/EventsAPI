using Domain.ChatAggregate.Entities;
using Domain.ChatAggregate.Exceptions;
using Domain.ChatAggregate.ValueObjects;
using Domain.Common.Models;
using Domain.EventAggregate;
using Domain.EventAggregate.ValueObjects;

namespace Domain.ChatAggregate
{
    public sealed class Chat : AggregateRoot<ChatId>
    {

#pragma warning disable CS8618
        private Chat()
        {

        }
#pragma warning restore CS8618

        private readonly List<Message> _messages = new();

        public EventId EventId { get; } = null!;
        public Event Event { get; } = null!;

        public IReadOnlyList<Message> Messages => _messages.AsReadOnly();

        public Chat(Event @event)
            : base(ChatId.CreateUnique())
        {
            if (@event.Chat is not null)
                throw new ChatExistException();

            Event = @event;
        }

        public void AddMessage(Message message)
        {
            if (Event.Participations.FirstOrDefault(p => p.Id == message.Author) is null)
                throw new InvalidAuthorException();

            _messages.Add(message);
        }

    }
}
