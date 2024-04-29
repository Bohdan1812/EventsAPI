using Domain.EventAggregate.ValueObjects;
using Domain.JoinRequestAggregate;
using Domain.JoinRequestAggregate.ValueObjects;
using Domain.UserAggregate.ValueObjects;

namespace Application.Persistence.Repositories
{
    public interface IJoinRequestRepository
    {
        Task Add(JoinRequest joinRequest);
        Task Remove(JoinRequestId joinRequestId);
        Task<JoinRequest?> GetJoinRequest(JoinRequestId joinRequestId);
        Task<List<JoinRequest>> GetJoinRequestsByUser(UserId userId); 
        Task<List<JoinRequest>> GetJoinRequestsByEvent(EventId eventId);
    }
}
