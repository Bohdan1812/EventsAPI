using EventsAPI.Data;
using EventsAPI.Models;
using EventsAPI.Services.EventSevices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventsAPI.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public EventService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<Event>> AddEvent(Guid userId, AddEventDto newEvent)
        {
            var serviceResponse = new ServiceResponse<Event>();
            try
            {
                var @event = _mapper.Map<Event>(newEvent);
                if(@event.EventCreation.User.Id != userId)
                    throw new Exception($"This user is not cretor!");
                _context.Events.Add(@event);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Events.OrderByDescending(e => e.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> DeleteEvent(Guid userId, Guid eventId)
        {
            var serviceResponse = new ServiceResponse<bool>();

            try
            {
                var @event = await _context.Events.FirstOrDefaultAsync(e => e.Id == eventId);
                
                if(@event.EventCreation.User.Id != userId)
                    throw new Exception($"This user has not permission to delete event '{eventId}'!");

                if (@event is null)
                    throw new Exception($"Event with eventId '{eventId}' not found");
 

                _context.Events.Remove(@event);
                serviceResponse.Data = await _context.SaveChangesAsync() > 0;


            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Data = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Event>>> GetAllEvents(Guid userId)
        {
            var serviceResponse = new ServiceResponse<List<Event>>();
            serviceResponse.Data = await _context.Events.Where(e => e.EventCreation.UserId == userId).ToListAsync(); 
            return serviceResponse;
        }

        public async Task<ServiceResponse<Event>> GetEventById(Guid id)
        {
            var serviceResponse = new ServiceResponse<Event>();
            try
            {
                var @event = await _context.Events.FirstOrDefaultAsync(c => c.Id == id);
                if (@event is null)
                    throw new Exception($"Event with eventId '{id}' not found");
                serviceResponse.Data = @event;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Event>>> GetPastEvents(Guid userId, DateTime startTime, DateTime endTime)
        {
            var serviceResponse = new ServiceResponse<List<Event>>();
            serviceResponse.Data = await _context.Events.Where(e => e.EventCreation.UserId == userId && e.StartTime <= startTime && e.EndTime >= endTime).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Event>>> GetPastEvents(Guid userId, DateTime startTime, int count)
        {
            var serviceResponse = new ServiceResponse<List<Event>>();
            serviceResponse.Data = await _context.Events.Where(e => e.EventCreation.UserId == userId && e.StartTime <= startTime).Take(count).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<Event>> GetUpcomingEvent(Guid userId, DateTime time)
        {
            var serviceResponse = new ServiceResponse<Event>();
            serviceResponse.Data = await _context.Events.Where(e => e.EventCreation.UserId == userId).OrderBy(e => e.StartTime).FirstOrDefaultAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Event>>> GetUpcomingEvents(Guid userId, DateTime startTime, DateTime endTime)
        {
            var serviceResponse = new ServiceResponse<List<Event>>();
            serviceResponse.Data = await _context.Events.Where(e => e.EventCreation.UserId == userId && e.StartTime <= startTime && e.EndTime >= endTime).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Event>>> GetUpcomingEvents(Guid userId, DateTime startTime, int count)
        {
            var serviceResponse = new ServiceResponse<List<Event>>();
            serviceResponse.Data = await _context.Events.Where(e => e.EventCreation.UserId == userId && e.StartTime >= startTime).Take(count).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<Event>> UpdateEvent(Event updatedEvent)
        {
            var serviceResponse = new ServiceResponse<Event>();

            try
            {

                var @event = await _context.Events.FirstOrDefaultAsync(c => c.Id == updatedEvent.Id);
                

                if (@event is null)
                    throw new Exception($"Event with eventId '{updatedEvent.Id}' not found");


                @event.Name = updatedEvent.Name;
                @event.Description = updatedEvent.Description;
                @event.StartTime = updatedEvent.StartTime;
                @event.EndTime = updatedEvent.EndTime;
                @event.Longitude = updatedEvent.Longitude;
                @event.Latitude = updatedEvent.Latitude;
                @event.Link = updatedEvent.Link;
                @event.EventCreation = updatedEvent.EventCreation;
                @event.Participations = updatedEvent.Participations;

                _context.Update(@event);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Events.FirstOrDefaultAsync(c => c.Id == updatedEvent.Id);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
