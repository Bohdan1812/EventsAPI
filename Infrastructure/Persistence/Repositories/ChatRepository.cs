using Application.Persistence.Repositories;
using Domain.ChatAggregate;
using Domain.ChatAggregate.ValueObjects;
using Domain.EventAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly EventAppDbContext _dbContext;

        public ChatRepository(EventAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Chat chat)
        {
            _dbContext.Add(chat);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(ChatId chatId)
        {
            Chat? chat = await GetChat(chatId);

            if (chat is not null)
            {
                _dbContext.Remove(chat);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Chat?> GetChat(ChatId chatId)
        {
            return await _dbContext.Chats.FindAsync(chatId);
        }

        public async Task<Chat?> GetChat(EventId eventId)
        {
            return await _dbContext.Chats
                .FirstOrDefaultAsync(c => c.EventId == eventId);
        }

        public async Task Update(Chat chat)
        {
            var updatedChat = await GetChat(chat.Id);

            if(updatedChat is not null)
            {
                updatedChat = chat;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
