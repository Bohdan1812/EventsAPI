using Domain.UserAggregate.ValueObjects;
using Domain.UserAggregate;
using Domain.EventAggregate;
using Domain.EventAggregate.ValueObjects;
using Domain.OrganizerAggregate.ValueObjects;

namespace Application.Persistence.Repositories
{
    public interface IEventRepository
    {
        Task<Event?> GetEvent(EventId eventId);

        Task<List<Event>> GetOrganizerEvents(OrganizerId organizerId);

        Task Add(Event @event);

        Task Remove(EventId eventId);

        Task Update(Event @event);
      
    }
}
