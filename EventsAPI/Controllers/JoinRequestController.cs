﻿using Application.Events.Commands.Create;
using Application.JoinRequests.Commands.Add;
using Application.JoinRequests.Commands.RemoveAsOrganizer;
using Application.JoinRequests.Commands.RemoveOwn;
using Application.JoinRequests.Queries.GetJoinRequest;
using Application.JoinRequests.Queries.GetOrganizerJoinRequests;
using Application.JoinRequests.Queries.GetOwnJoinRequests;
using Azure.Core;
using Contracts.JoinRequest;
using Domain.Common.Models;
using Domain.JoinRequestAggregate;
using ErrorOr;
using Mapster;
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

        [HttpGet("getOwnJoinRequests")]
        public async Task<IActionResult> GetOwnJoinRequests()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<GetOwnJoinRequestsQuery>((new Guid(userId)));
            ErrorOr<List<JoinRequest>> getUserJoinRequestsResult = await _mediator.Send(command);

            return getUserJoinRequestsResult.Match(
                getUserJoinRequestsResult => Ok(getUserJoinRequestsResult.Adapt<List<JoinRequestResponse>>()),
                errors => Problem(errors));
        }

        [HttpGet("getOrganizerJoinRequests")]
        public async Task<IActionResult> GetOrganizerJoinRequests()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<GetOrganizerJoinRequestsQuery>((new Guid(userId)));
            ErrorOr<List<JoinRequest>> getOrganizerJoinRequestsResult = await _mediator.Send(command);

            return getOrganizerJoinRequestsResult.Match(
                getOrganizerJoinRequestsResult => Ok(getOrganizerJoinRequestsResult.Adapt<List<JoinRequestResponse>>()),
                errors => Problem(errors));
        }

        [HttpGet("getJoinRequest")]
        public async Task<IActionResult> GetJoinRequest(Guid joinRequestId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<GetJoinRequestQuery>((new Guid(userId), joinRequestId));
            ErrorOr<JoinRequest?> getJoinRequestResult = await _mediator.Send(command);

            return getJoinRequestResult.Match(
                getJoinRequestResult => Ok(_mapper.Map<JoinRequestResponse>(getJoinRequestResult)),
                errors => Problem(errors));
        }

        [HttpDelete("removeOwnJoinRequest")]
        public async Task<IActionResult> RemoveOwnJoinRequest(RemoveJoinRequestRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<RemoveOwnJoinRequestCommand>((new Guid(userId), request));

            ErrorOr<string> removeJoinRequestResult = await _mediator.Send(command);

            return removeJoinRequestResult.Match(
                result => Ok(_mapper.Map<string>(result)),
                errors => Problem(errors));
        }

        [HttpDelete("removeJoinRequestAsOrganizer")]
        public async Task<IActionResult> RemoveOrganizerJoinRequest(RemoveJoinRequestRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<RemoveOrganizerJoinRequestCommand>((new Guid(userId), request));

            ErrorOr<string> removeJoinRequestResult = await _mediator.Send(command);

            return removeJoinRequestResult.Match(
                result => Ok(_mapper.Map<string>(result)),
                errors => Problem(errors));
        }

    }
}
