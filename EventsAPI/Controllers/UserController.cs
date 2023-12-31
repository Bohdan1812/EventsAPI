using Azure;

namespace EventsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetAllUsers()
        {
            
            return Ok(await _userService.GetAllUsers());
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<User>>> GetUser(Guid id)
        {
            return Ok(await _userService.GetUserById(id));
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            var user = await _userManager.GetUserAsync(User);
            var eventUser = user.EventUser;
            if (eventUser is null) 
            {
                return NotFound();
            }
            return Ok(eventUser);
        }
        [Authorize]
        [HttpPost("AddUser")]
        public async Task<ActionResult<ServiceResponse<User>>> AddUser(AddUserDto newCharacter)
        {
            var user = await _userManager.GetUserAsync(User);
            return Ok(await _userService.AddUser(newCharacter, user.Id));
        }
        [Authorize]
        [HttpPut("UpdateUser")]
        public async Task<ActionResult<ServiceResponse<User>>> UpdateUser(User updatedUser)
        {
            var response = await _userService.UpdateUser(updatedUser);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [Authorize]
        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteUser()
        {
            var appUser = await _userManager.GetUserAsync(User);

            var response = new ServiceResponse<User>();//await _userService.DeleteUser(, user.Id);
            if (response.Success is false)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
