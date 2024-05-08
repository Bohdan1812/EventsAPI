using Application.Messages.Commands.AddMessage;
using Application.Messages.Commands.DeleteMessage;
using Application.Messages.Commands.UpdateMessage;
using Application.Messages.Queries.GetEventMessages;
using Application.Messages.Queries.GetMessage;
using Contracts.Message;
using Domain.ChatAggregate.Entities;
using ErrorOr;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("Message")]
    public class MessageController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public MessageController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpPut("addMessage")]
        public async Task<IActionResult> AddMessage(AddMessageRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<AddMessageCommand>((new Guid(userId), request));
            ErrorOr<string> addMessageResult = await _mediator.Send(command);

            return addMessageResult.Match(
                result => Ok(result),
                errors => Problem(errors));
        }


        [HttpDelete("deleteMessage")]
        public async Task<IActionResult> DeleteMessage(DeleteMessageRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<DeleteMessageCommand>((new Guid(userId), request));
            ErrorOr<string> deleteMessageResult = await _mediator.Send(command);

            return deleteMessageResult.Match(
                result => Ok(result),
                errors => Problem(errors));
        }


        [HttpPost("updateMessage")]
        public async Task<IActionResult> UpdateMessage(UpdateMessageRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<UpdateMessageCommand>((new Guid(userId), request));
            ErrorOr<string> updateMessageResult = await _mediator.Send(command);

            return updateMessageResult.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPut("getMessage")]
        public async Task<IActionResult> GetMessage([FromQuery]GetMessageRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<GetMessageQuery>((new Guid(userId), request));
            ErrorOr<Message> getEventResult = await _mediator.Send(command);

            return getEventResult.Match(
                result => Ok(_mapper.Map<MessageResponse>(result)),
                errors => Problem(errors));
        }


        [HttpPut("getEventMessages")]
        public async Task<IActionResult> GetEventMessages([FromQuery] GetEventMessagesRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<GetEventMessagesQuery>((new Guid(userId), request));
            ErrorOr<List<Message>> getEventsResult = await _mediator.Send(command);

            return getEventsResult.Match(
                result => Ok(result.Adapt<MessageResponse>()),
                errors => Problem(errors));
        }

    }
}
