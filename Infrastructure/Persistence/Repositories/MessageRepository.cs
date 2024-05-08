using Application.Persistence.Repositories;
using Domain.ChatAggregate.Entities;
using Domain.EventAggregate.ValueObjects;
using Domain.MessageAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Repositories
{
    public class MessageRepository(EventAppDbContext dbContext) : IMessageRepository
    {
        private readonly EventAppDbContext _dbContext = dbContext;

        public async Task AddMessage(Message message)
        {
            _dbContext.Add(message);
            await _dbContext.SaveChangesAsync();    
        }

        public async Task DeleteMessage(MessageId messageId)
        {
            var message = await _dbContext.Messages.FindAsync(messageId);

            if(message is not null)
            {
                _dbContext.Remove(message);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateMessage(Message editedMessage)
        {
            _dbContext.Entry(editedMessage).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<Message>> GetEventMessages(EventId eventId)
        {
            return _dbContext.Events.Where(e => e.Id == eventId)
                .SelectMany(e => e.Participations)
                .SelectMany(p => p.Messages)
                .OrderBy(m => m.CreatedDateTime)
                .ToListAsync();
        }

        public async Task<Message?> GetMessage(MessageId messageId)
        {
            return await _dbContext.Messages.FindAsync(messageId);
        }
    }
}
