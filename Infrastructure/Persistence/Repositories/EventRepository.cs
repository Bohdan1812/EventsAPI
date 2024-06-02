using Application.Persistence.Repositories;
using Domain.EventAggregate;
using Domain.EventAggregate.ValueObjects;
using Domain.OrganizerAggregate.ValueObjects;
using Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventAppDbContext _dbContext;

        public EventRepository(EventAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Event @event)
        {
            _dbContext.Add(@event);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Event?> GetEvent(EventId eventId)
        {
            return await _dbContext.Events.FindAsync(eventId);
        }

        public async Task<Event?> GetFullEvent(EventId eventId)
        {
            return await _dbContext.Events
                .Include(e => e.Invites)
                .Include(e => e.JoinRequests)
                .Include(e => e.Participations)
                .FirstOrDefaultAsync(e => e.Id == eventId);
        }

        public async Task<List<Event>> GetOrganizerEvents(OrganizerId organizerId)
        {

            return await _dbContext.Events
                .Where(e => e.OrganizerId == organizerId).ToListAsync();
        }

        public async Task<List<Event>> GetUserEvents(UserId userId)
        {
            return await _dbContext.Events
                .Include(e => e.Participations)
                .Where(e => e.Participations.Any(p => p.UserId == userId))
                .OrderBy(e => e.StartDateTime)
                .ToListAsync();
        }

        public async Task Remove(EventId eventId)
        {
            Event? @event = await GetEvent(eventId);
            if (@event is not null)
            {
                _dbContext.Remove(@event);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Update(Event updatedEvent)
        {
            _dbContext.Entry(updatedEvent).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            
        }
    }
}
