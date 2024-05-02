using Domain.EventAggregate.ValueObjects;
using Domain.InviteAggregate;
using Domain.InviteAggregate.ValueObjects;
using Domain.UserAggregate.ValueObjects;
namespace Application.Persistence.Repositories
{
    public interface IInviteRepository
    {
        Task Add(Invite invite);
        Task Remove(InviteId inviteId);
        Task<Invite?> GetFullInvite(InviteId inviteId);
        Task<Invite?> GetInvite(InviteId inviteId); 
        Task<List<Invite>> GetInvitesByUser(UserId userId); 
        Task<List<Invite>> GetInvitesByEvent(EventId eventId);
    }
}
