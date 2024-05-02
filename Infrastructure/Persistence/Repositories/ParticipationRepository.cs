﻿using Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Domain.ParticipationAggregate;
using Domain.ParticipationAggregate.ValueObjects;
using Domain.EventAggregate.ValueObjects;
using Application.Persistence.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class ParticipationRepository : IParticipationRepository
    {
        private readonly EventAppDbContext _dbContext;

        public ParticipationRepository(EventAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Participation participation)
        {
            _dbContext.Participations.Add(participation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Participation?> GetFullParticipation(ParticipationId participationId)
        {
            return await _dbContext.Participations
                .Include(p => p.User)
                .Include(p => p.Event)
                .FirstOrDefaultAsync(jr => jr.Id == participationId);
        }

        public async Task<List<Participation>> GetParticipationsByUser(UserId userId)
        {
            return await _dbContext.Participations
                .Where(jr => jr.UserId == userId).ToListAsync();
        }

        public async Task<List<Participation>> GetParticipationsByEvent(EventId eventId)
        {
            return await _dbContext.Participations
                .Where(jr => jr.EventId == eventId).ToListAsync();
        }


        public async Task Remove(ParticipationId participationId)
        {
            var invite = await _dbContext.Invites
                .FirstOrDefaultAsync(j => j.Id == participationId);

            if (invite is not null)
            {
                _dbContext.Remove(invite);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Participation?> GetParticipation(ParticipationId participationId)
        {
            return await _dbContext.Participations.FindAsync(participationId);
        }
    }
}

