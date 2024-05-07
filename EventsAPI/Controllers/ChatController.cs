using Application.Chat.Commands.AddMessage;
using Contracts.Chat;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("Chat")]
    public class ChatController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ChatController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPut("AddMessage")]
        public async Task<IActionResult> AddMessage(AddMessageRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<AddMessageCommand>((new Guid(userId), request));
            var addMessageResult = await _mediator.Send(command);

            return addMessageResult.Match(
                result => Ok(result),
                errors => Problem(errors));
        }
    }
}
