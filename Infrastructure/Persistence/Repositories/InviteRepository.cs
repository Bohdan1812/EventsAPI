
using Application.Persistence.Repositories;
using Domain.EventAggregate.ValueObjects;
using Domain.InviteAggregate;
using Domain.InviteAggregate.ValueObjects;
using Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class InviteRepository : IInviteRepository
    {
        private readonly EventAppDbContext _dbContext;

        public InviteRepository(EventAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Invite invite)
        {
            _dbContext.Invites.Add(invite);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Invite?> GetFullInvite(InviteId inviteId)
        {
            return await _dbContext.Invites
                .Include(jr => jr.User)
                .Include(jr => jr.Event)
                .FirstOrDefaultAsync(jr => jr.Id == inviteId);
        }

        public async Task<List<Invite>> GetInvitesByUser(UserId userId)
        {
            return await _dbContext.Invites
                .Where(jr => jr.UserId == userId).ToListAsync();
        }

        public async Task<List<Invite>> GetInvitesByEvent(EventId eventId)
        {
            return await _dbContext.Invites
                .Where(jr => jr.EventId == eventId).ToListAsync();
        }


        public async Task Remove(InviteId inviteId)
        {
            var invite = await _dbContext.Invites
                .FirstOrDefaultAsync(j => j.Id == inviteId);

            if (invite is not null)
            { 
                _dbContext.Remove(invite);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Invite?> GetInvite(InviteId inviteId)
        {
            return await _dbContext.Invites.FindAsync(inviteId);
        }
    }
}
