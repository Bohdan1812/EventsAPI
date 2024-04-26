using Domain.ChatAggregate.ValueObjects;
using Domain.Common.Models;
using Domain.ParticipationAggregate.ValueObjects;

namespace Domain.ChatAggregate.Entities
{
    public class Message : Entity<MessageId>
    {
        public ParticipationId Author { get; }

        public string Text { get; private set; } = null!;

        public DateTime CreatedDateTime { get; }

        public DateTime UpdatedDateTime { get; private set; }

        public Message(MessageId messageId, ParticipationId participationId, string text)
            : base(messageId)
        { 
            Author = participationId;
            Text = text;
            CreatedDateTime = DateTime.UtcNow;
            UpdatedDateTime = DateTime.UtcNow;   
        }

        public void UpdateText(ParticipationId author, string text) 
        {
            if (author != Author)
            {
                throw new Exception("You are not author of this message!");
            }

            if(text != string.Empty)
            {
                Text = text;
                UpdatedDateTime = DateTime.UtcNow;
            }
            else
            {
                throw new Exception("The text of message must not be empty!");
            }
            
            
        }
       
    }
}
