using Domain.EventAggregate;
using Domain.EventAggregate.Entities;
using Domain.EventAggregate.ValueObjects;
using Domain.OrganizerAggregate.ValueObjects;

namespace Application.Persistence.Repositories
{
    public interface IEventRepository
    {
        Task<Event?> GetEvent(EventId eventId);
        Task<Event?> GetFullEvent(EventId eventId);
        Task<List<Event>> GetOrganizerEvents(OrganizerId organizerId);
        Task Add(Event @event);
        Task Remove(EventId eventId);
        Task Update(Event @event);
       /*
        Task AddSubEvent(EventId eventId, SubEvent subEvent);

        Task RemoveSubEvent(EventId eventId, SubEventId subEventId);

        Task UpdateSubEvent(EventId eventId, SubEvent subEvent);
      */
    }
}
