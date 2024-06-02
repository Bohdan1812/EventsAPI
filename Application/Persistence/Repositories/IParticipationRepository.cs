using Domain.UserAggregate.ValueObjects;
using Domain.ParticipationAggregate;
using Domain.ParticipationAggregate.ValueObjects;
using Domain.EventAggregate.ValueObjects;

namespace Application.Persistence.Repositories
{
    public interface IParticipationRepository
    {
        Task Add(Participation participation);
        Task Remove(ParticipationId participationId);
        Task<Participation?> GetFullParticipation(ParticipationId participationId);
        Task<Participation?> GetParticipation(ParticipationId participationId);
        Task<Participation?> GetParticipationByUserEvent(UserId userId, EventId eventId);
        Task<Participation?> GetParticipation(Guid appUserId, EventId eventId);
        Task<List<Participation>> GetParticipationsByUser(UserId userId);
        Task<List<Participation>> GetParticipationsByEvent(EventId eventId);
    }
}
