using Domain.Common.Models;
using Domain.MessageAggregate.Exceptions;
using Domain.MessageAggregate.ValueObjects;
using Domain.ParticipationAggregate;
using Domain.ParticipationAggregate.ValueObjects;
namespace Domain.ChatAggregate.Entities
{
    public class Message : AggregateRoot<MessageId>
    {
#pragma warning disable CS8618
        private Message()
        {

        }
#pragma warning restore CS8618

        public ParticipationId AuthorId { get; } = null!;

        public Participation Author { get; } = null!;

        public string Text { get; private set; } = null!;

        public DateTime CreatedDateTime { get; private set; }

        public DateTime UpdatedDateTime { get; private set; }

        public Message(Participation participation, string text)
            : base(MessageId.CreateUnique())
        {
            if (string.IsNullOrEmpty(text))
                throw new TextIsEmptyException();

            Author = participation;
            Text = text;
            CreatedDateTime = DateTime.UtcNow;
            UpdatedDateTime = DateTime.UtcNow;
        }

        public void UpdateText(ParticipationId authorId, string text)
        {
            if (authorId != AuthorId)
                throw new MessageNoPermissionException();

            if (string.IsNullOrEmpty(text))
                throw new TextIsEmptyException();

            Text = text;
            UpdatedDateTime = DateTime.UtcNow;
        }
    }
}
