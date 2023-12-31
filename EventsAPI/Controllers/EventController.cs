
namespace EventsAPI.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventController(IEventService eventService, UserManager<ApplicationUser> userManager)
        {
            _eventService = eventService;
            _userManager = userManager; ;
        }


        [Authorize]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Event>>>> GetAllEvents()
        {
            var user = await _userManager.GetUserAsync(User);
            return Ok(await _eventService.GetAllEvents(user.EventUser.Id));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Event>>> GetEvent(Guid id)
        {
            return Ok(await _eventService.GetEventById(id));
        }

        [Authorize]
        [HttpPost("AddEvent")]
        public async Task<ActionResult<ServiceResponse<User>>> AddUser(AddEventDto newEvent)
        {
            var user = await _userManager.GetUserAsync(User);
            return Ok(await _eventService.AddEvent(user.EventUser.Id, newEvent));
        }

        [Authorize]
        [HttpPut("UpdateEvent")]
        public async Task<ActionResult<ServiceResponse<Event>>> UpdateEvent(Event updatedEvent)
        {
            var response = await _eventService.UpdateEvent(updatedEvent);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("DeleteEvent")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteEvent(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            var response = await _eventService.DeleteEvent(user.EventUser.Id, id);
            if (!response.Data)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
