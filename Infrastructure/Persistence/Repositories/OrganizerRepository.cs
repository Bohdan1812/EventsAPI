using Application.Persistence.Repositories;
using Domain.OrganizerAggregate;
using Domain.OrganizerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class OrganizerRepository : IOrganizerRepository
    {
        private readonly EventAppDbContext _dbContext;

        public OrganizerRepository(EventAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Organizer organizer)
        {
            _dbContext.Add(organizer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Organizer?> GetOrganizer(Guid appUserId)
        {
            var user = await _dbContext.DomainUsers
                .Where(u => u.ApplicationUserId == appUserId).FirstOrDefaultAsync();
            Organizer? organizer = null;

            if (user is not null)
            {
                organizer = await _dbContext.Organizers
                    .Where(o => o.UserId == user.Id).FirstOrDefaultAsync();
            }

            return organizer;
        }

        public async Task<Organizer?> GetOrganizer(OrganizerId organizerId)
        {
            return await _dbContext.Organizers.FindAsync(organizerId);
        }

        public async Task Remove(OrganizerId organizerId)
        {
            Organizer? organizer = await GetOrganizer(organizerId);
            if (organizer is not null)
            {
                _dbContext.Remove(organizer);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Update(Organizer organizer)
        {
            _dbContext.Update(organizer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
