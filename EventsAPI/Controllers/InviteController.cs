using Application.Invites.Commands.AddInvite;
using Application.Invites.Commands.DeleteOwnInvite;
using Application.Invites.Commands.RemoveAsOrganizer;
using Application.Invites.Queries.GetEventInvites;
using Application.Invites.Queries.GetInvite;
using Application.Invites.Queries.GetUserInvites;
using Application.JoinRequests.Commands.Add;
using Application.JoinRequests.Commands.RemoveOwn;
using Contracts.Invite;
using Domain.Common.Models;
using Domain.InviteAggregate;
using ErrorOr;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("Invite")]
    public class InviteController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public InviteController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPut("addInvite")]
        public async Task<IActionResult> AddInvite(AddInviteRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<AddInviteCommand>((new Guid(userId), request));
            ErrorOr<string> addInviteResult = await _mediator.Send(command);

            return addInviteResult.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpDelete("removeOwnInvite")]
        public async Task<IActionResult> RemoveOwnInvite(RemoveInviteRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<RemoveOwnInviteCommand>((new Guid(userId), request));

            ErrorOr<string> removeInviteResult = await _mediator.Send(command);

            return removeInviteResult.Match(
                result => Ok(_mapper.Map<string>(result)),
                errors => Problem(errors));
        }

        [HttpDelete("removeOrganizerInvite")]
        public async Task<IActionResult> RemoveOrganizerInvite(RemoveInviteRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<RemoveOrganizerInviteCommand>((new Guid(userId), request));

            ErrorOr<string> removeInviteResult = await _mediator.Send(command);

            return removeInviteResult.Match(
                result => Ok(_mapper.Map<string>(result)),
                errors => Problem(errors));
        }

        [HttpGet("getInvite")]
        public async Task<IActionResult> GetInvite([FromQuery]GetInviteRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<GetInviteQuery>((new Guid(userId), request));

            ErrorOr<Invite> getInviteResult = await _mediator.Send(command);

            return getInviteResult.Match(
                result => Ok(_mapper.Map<GetInviteResponse>(result)),
                errors => Problem(errors));
        }

        [HttpGet("getUserInvites")]
        public async Task<IActionResult> GetUserInvites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<GetUserInvitesQuery>(new Guid(userId));

            ErrorOr<List<Invite>> getInvitesResult = await _mediator.Send(command);

            return getInvitesResult.Match(
                result => Ok(result.Adapt<List<GetInviteResponse>>()),
                errors => Problem(errors));
        }

        [HttpGet("getEventInvites")]
        public async Task<IActionResult> GetEventInvites([FromQuery]GetEventInvitesRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<GetEventInvitesQuery>((new Guid(userId), request));

            ErrorOr<List<Invite>> getInvitesResult = await _mediator.Send(command);

            return getInvitesResult.Match(
                result => Ok(result.Adapt<List<GetInviteResponse>>()),
                errors => Problem(errors));
        }




    }
}
