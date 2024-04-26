using Application.Events.Commands.Create;
using Application.Events.Commands.Delete;
using Application.Events.Queries.GetEvent;
using Application.Events.Queries.GetUserEvents;
using Application.Users.Commands.Update;
using Contracts.Event;
using Domain.Common.Models;
using Domain.EventAggregate;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("Event")]
    public class EventController : ApiController
    {
        private readonly ISender _mediator;

        private readonly IMapper _mapper;

        private readonly UserManager<ApplicationUser> _userManager;


        public EventController(IMapper mapper, ISender mediator, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpPut("createEvent")]
        public async Task<IActionResult> CreateEvent(CreateEventRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<CreateEventCommand>((new Guid(userId), request));
            ErrorOr<string> createEventResult = await _mediator.Send(command);

            return createEventResult.Match(
                createEventResult => Ok(createEventResult),
                errors => Problem(errors));
        }

        [HttpGet("getEventById")]
        public async Task<IActionResult> GetEventById([FromQuery] GetEventRequestModel request)
        {
            var command = _mapper.Map<GetEventQuery>(request);

            var getEventResult = await _mediator.Send(command);

            return getEventResult.Match(
                getEventResult => Ok(getEventResult),
                errors => Problem(errors));
        }

        [HttpGet("getOrganizerEvents")]
        public async Task<IActionResult> GetEventById()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            { 
                return BadRequest("User not found!");
            }

            var command = new GetOrganizerEventsQuery(new Guid(userId));

            var getEventsResult = await _mediator.Send(command);

            return getEventsResult.Match(
                getEventResult => Ok(getEventResult),
                errors => Problem(errors));
        }

        [HttpDelete("deleteEvent")]
        public async Task<IActionResult> DeleteEvent(DeleteEventRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<DeleteEventCommand>((new Guid(userId), request));

            var result = await _mediator.Send(command); 

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }
    }
}
