using Application.Participations.Commands.AddParticipationFromInvite;
using Application.Participations.Commands.AddParticipationFromJoinRequest;
using Application.Participations.Commands.RemoveOwnParticipation;
using Application.Participations.Commands.RemoveParticipationAsOrganizer;
using Application.Participations.Queries.GetOwnParticipations;
using Application.Participations.Queries.GetParticipation;
using Application.Participations.Queries.GetParticipationsByEvent;
using Contracts.Participation;
using Domain.ParticipationAggregate;
using ErrorOr;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("Participation")]
    public class ParticipationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ParticipationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPut("addParticicpationByInvite")]
        public async Task<IActionResult> AddParticipationByInvite(AddParticipationByInviteRequestModel request) 
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<AddParticipationFromInviteCommand>((new Guid(userId), request));
            ErrorOr<string> addParticipationResult = await _mediator.Send(command);

            return addParticipationResult.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPut("addParticicpationByJoinRequest")]
        public async Task<IActionResult> AddParticipationByJoinRequest(AddParticipationByJoinRequestRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<AddParticipationFromJoinRequestCommand>((new Guid(userId), request));
            ErrorOr<string> addParticipationResult = await _mediator.Send(command);

            return addParticipationResult.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpDelete("removeOwnParticipation")]
        public async Task<IActionResult> RemoveOwnParticipation(RemoveParticipationRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<RemoveOwnParticipationCommand>((new Guid(userId), request));
            ErrorOr<string> removeResult = await _mediator.Send(command);

            return removeResult.Match(
                result => Ok(result),
                errors => Problem(errors));
        }


        [HttpDelete("removeParticipationAsOrganizer")]
        public async Task<IActionResult> RemoveParticipationAsOrganizer(RemoveParticipationRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<RemoveParticipationAsOrganizerCommand>((new Guid(userId), request));
            ErrorOr<string> removeResult = await _mediator.Send(command);

            return removeResult.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpGet("getOwnParticipations")]
        public async Task<IActionResult> GetOwnParticipations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<GetOwnParticipationsQuery>(new Guid(userId));
            ErrorOr<List<Participation>> removeResult = await _mediator.Send(command);

            return removeResult.Match(
                result => Ok(result.Adapt<List<ParticipationResponse>>()),
                errors => Problem(errors));
        }

        [HttpGet("getParticipationsByEvent")]
        public async Task<IActionResult> GetParticipationsByEvent(GetParticipationsByEventRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<GetParticipationsByEventQuery>((new Guid(userId), request));
            ErrorOr<List<Participation>> removeResult = await _mediator.Send(command);

            return removeResult.Match(
                result => Ok(result.Adapt<List<ParticipationResponse>>()),
                errors => Problem(errors));
        }

        [HttpGet("getParticipation")]
        public async Task<IActionResult> GetParticipation(GetParticipationRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<GetParticipationQuery>((new Guid(userId), request));
            ErrorOr<Participation> getParticipationResult = await _mediator.Send(command);

            return getParticipationResult.Match(
                result => Ok(_mapper.Map<ParticipationResponse>(result)),
                errors => Problem(errors));
        }
    }
}
