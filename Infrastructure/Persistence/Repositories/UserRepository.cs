using Application.Persistence.Repositories;
using Domain.UserAggregate;
using Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

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

        public async Task<List<User>> FindUsers(string email, string firstName, string lastName)
        {
            if (!email.IsNullOrEmpty() && IsEmail(email))
                return await _dbContext.DomainUsers
                    .Include(u => u.ApplicationUser)
                    .Where(u => u.ApplicationUser.Email == email).ToListAsync();
            else 
            {
                if (firstName.IsNullOrEmpty() && !lastName.IsNullOrEmpty())
                    return await _dbContext.DomainUsers
                        .Include(u => u.ApplicationUser)
                        .Where(u => u.LastName == lastName).ToListAsync();
                else if (!firstName.IsNullOrEmpty() && lastName.IsNullOrEmpty())
                    return await  _dbContext.DomainUsers
                        .Include(u => u.ApplicationUser)
                        .Where(u => u.FirstName == firstName).ToListAsync();
                else
                    return await _dbContext.DomainUsers
                        .Include(u => u.ApplicationUser)
                        .Where(u => u.LastName == lastName
                        && u.FirstName == firstName).ToListAsync();
            }
        }

        static bool IsEmail(string word)
        {
            // Use a simple regex pattern to check if the word resembles an email address
            return Regex.IsMatch(word, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }

        public async Task<User?> GetFullUser(UserId userId)
        {
            return await _dbContext.DomainUsers
                .Include(u => u.ApplicationUser)
                .Include(u => u.JoinRequests)
                .Include(u => u.Invites)
                .Include(u => u.Organizer)
                .Include(u => u.Organizer.Events)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<User?> GetUser(UserId userId)
        {
            return await _dbContext.DomainUsers
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetUser(Guid appUserId)
        {
            return await _dbContext.DomainUsers
                .FirstOrDefaultAsync(u => u.ApplicationUserId == appUserId);
        }


        public async Task Remove(UserId userId)
        {
            User? user = await GetFullUser(userId);
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
            throw new NotImplementedException();
        }
    }
}
