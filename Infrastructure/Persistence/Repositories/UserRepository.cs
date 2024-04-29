using Application.Persistence.Repositories;
using Domain.UserAggregate;
using Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EventAppDbContext _dbContext;
        public UserRepository(EventAppDbContext dbContext) 
        {
           _dbContext = dbContext;
        }
        public async Task Add(User user)
        {
            _dbContext.Add(user);        
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUser(UserId userId)
        {
            return await _dbContext.DomainUsers
                .Include(u => u.JoinRequests)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetUser(Guid appUserId)
        {
            return await _dbContext.DomainUsers
                .Include(u => u.JoinRequests)
                .FirstOrDefaultAsync(u => u.ApplicationUserId == appUserId);
        }


        public async Task Remove(UserId userId)
        {
            User? user = await GetUser(userId);
            if (user is not null)
            {
                var organizer = await _dbContext.Organizers.Where(o => o.UserId == userId)
                    .FirstOrDefaultAsync();

                if (organizer is not null)
                {
                    _dbContext.Remove(organizer);
                    _dbContext.Remove(user);
                    await _dbContext.SaveChangesAsync();
                }
            }
            
        }

        public async Task Update(User user)
        {
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
