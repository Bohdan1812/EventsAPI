using Application.Persistence.Repositories;
using Domain.EventAggregate;
using Domain.EventAggregate.ValueObjects;
using Domain.OrganizerAggregate.ValueObjects;
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
                .FirstOrDefaultAsync(e => e.Id == eventId);
        }

        public async Task<List<Event>> GetOrganizerEvents(OrganizerId organizerId)
        {

            return await _dbContext.Events
                .Where(e => e.OrganizerId == organizerId).ToListAsync();
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
            var existingEvent = await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == updatedEvent.Id);

            if (existingEvent is not null)
            {
                existingEvent.Update(
                    updatedEvent.Name, 
                    updatedEvent.Description,
                    updatedEvent.StartDateTime,
                    updatedEvent.EndDateTime,
                    updatedEvent.Address,
                    updatedEvent.Link);

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
