using Domain.ChatAggregate.Exceptions;
using Domain.ChatAggregate.ValueObjects;
using Domain.Common.Models;
using Domain.ParticipationAggregate.ValueObjects;

namespace Domain.ChatAggregate.Entities
{
    public class Message : Entity<MessageId>
    {
#pragma warning disable CS8618
        private Message()
        {

        }
#pragma warning restore CS8618

        public ParticipationId Author { get; }

        public string Text { get; private set; } = null!;

        public DateTime CreatedDateTime { get; }

        public DateTime UpdatedDateTime { get; private set; }

        public Message(ParticipationId participationId, string text)
            : base(MessageId.CreateUnique())
        {
            if (string.IsNullOrEmpty(text))
                throw new TextIsEmptyException();

            Author = participationId;
            Text = text;
            CreatedDateTime = DateTime.UtcNow;
            UpdatedDateTime = DateTime.UtcNow;
        }

        public void UpdateText(ParticipationId author, string text)
        {
            if (author != Author)
                throw new MessageNoPermissionException();

            if (string.IsNullOrEmpty(text))
                throw new TextIsEmptyException();

            Text = text;
            UpdatedDateTime = DateTime.UtcNow;
        }
    }
}
