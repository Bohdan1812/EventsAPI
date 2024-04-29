using Application.Events.Commands.Create;
using Application.JoinRequests.Commands.Add;
using Contracts.JoinRequest;
using Domain.Common.Models;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("JoinRequest")]
    public class JoinRequestController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public JoinRequestController(IMapper mapper, ISender mediator, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpPut("addJoinRequest")]
        public async Task<IActionResult> AddJoinRequest(CreateJoinRequestRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<AddJoinRequestCommand>((new Guid(userId), request));
            ErrorOr<string> createEventResult = await _mediator.Send(command);

            return createEventResult.Match(
                createEventResult => Ok(createEventResult),
                errors => Problem(errors));
        }
    }
}
