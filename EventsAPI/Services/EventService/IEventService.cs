

namespace EventsAPI.Services.EventSevices
{
    public interface IEventService
    {
        Task<ServiceResponse<List<Event>>> GetAllEvents(Guid userId);
        Task<ServiceResponse<Event>> GetEventById(Guid id);
        Task<ServiceResponse<Event>> AddEvent(Guid userId, AddEventDto newEvent);
        Task<ServiceResponse<Event>> UpdateEvent(Event updatedUser);
        Task<ServiceResponse<Event>> GetUpcomingEvent(Guid userId, DateTime time);
        Task<ServiceResponse<List<Event>>> GetUpcomingEvents(Guid userId, DateTime startTime, DateTime endTime);
        Task<ServiceResponse<List<Event>>> GetUpcomingEvents(Guid userId, DateTime startTime, int count);
        Task<ServiceResponse<List<Event>>> GetPastEvents(Guid userId, DateTime startTime, DateTime endTime);
        Task<ServiceResponse<List<Event>>> GetPastEvents(Guid userId, DateTime startTime, int count);
        Task<ServiceResponse<bool>> DeleteEvent(Guid userId, Guid id);
    }
}
