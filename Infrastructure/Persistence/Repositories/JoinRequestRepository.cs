
using Application.Persistence.Repositories;
using Domain.EventAggregate.ValueObjects;
using Domain.JoinRequestAggregate;
using Domain.JoinRequestAggregate.ValueObjects;
using Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class JoinRequestRepository : IJoinRequestRepository
    {
        private readonly EventAppDbContext _dbContext;

        public JoinRequestRepository(EventAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(JoinRequest joinRequest)
        {
            _dbContext.JoinRequests.Add(joinRequest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<JoinRequest?> GetFullJoinRequest(JoinRequestId joinRequestId)
        {
            return await _dbContext.JoinRequests
                .Include(jr => jr.User)
                .Include(jr => jr.Event)
                .FirstOrDefaultAsync(jr => jr.Id == joinRequestId);
        }

        public async Task<List<JoinRequest>> GetJoinRequestsByUser(UserId userId)
        {
            return await _dbContext.JoinRequests
                .Where(jr => jr.UserId == userId).ToListAsync();
        }

        public async Task<List<JoinRequest>> GetJoinRequestsByEvent(EventId eventId)
        {
            return await _dbContext.JoinRequests
                .Where(jr => jr.EventId == eventId).ToListAsync();
        }


        public async Task Remove(JoinRequestId joinRequestId)
        {
            var joinRequest = await _dbContext.JoinRequests
                .FirstOrDefaultAsync(j => j.Id == joinRequestId);

            if (joinRequest is not null)
            { 
                _dbContext.Remove(joinRequest);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<JoinRequest?> GetJoinRequest(JoinRequestId joinRequestId)
        {
            return await _dbContext.JoinRequests.FindAsync(joinRequestId);
        }
    }
}
