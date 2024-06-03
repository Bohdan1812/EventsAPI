﻿using Application.Users.Commands.Delete;
using Application.Users.Commands.Update;
using Application.Users.Dto;
using Application.Users.Queries.FindUsers;
using Application.Users.Queries.GetCurrentUserInfo;
using Application.Users.Queries.GetUserByParticipation;
using Application.Users.Queries.GetUserInfo;
using Contracts.Authentication;
using Contracts.User;
using Domain.Common.Models;
using Domain.UserAggregate;
using ErrorOr;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("User")]
    public class UserController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public UserController(IMapper mapper, ISender mediator, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userManager = userManager;
        }


        [HttpPost("updateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<UpdateUserCommand>((new Guid(userId), request));

            ErrorOr<string> updateResut = await _mediator.Send(command);

            return updateResut.Match(
                updateResut => Ok(updateResut),
                errors => Problem(errors));
        }


        [HttpDelete("deleteUser")]
        public async Task<IActionResult> DeleteAccount(DeleteAccountRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<DeleteAccountCommand>((userId, request));

            ErrorOr<string> deleteResult = await _mediator.Send(command);

            return deleteResult.Match(
               authReult => Ok(authReult),
               errors => Problem(errors));
        }

        [HttpGet("getCurrentUserInfo")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not found!");
            }

            var query = _mapper.Map<GetCurrentUserInfoQuery>(new Guid(userId));

            ErrorOr<UserInfo> getUserResult = await _mediator.Send(query);

            return getUserResult.Match(
               result => Ok(result),
               errors => Problem(errors));
        }

        [HttpGet("getUserInfo")]
        public async Task<IActionResult> GetUserInfo([FromQuery] GetUserInfoRequestModel request)
        {
            var query = _mapper.Map<GetUserInfoQuery>(request);

            ErrorOr<UserInfo> getUserResult = await _mediator.Send(query);

            return getUserResult.Match(
               result => Ok(result),
               errors => Problem(errors));
        }

        [HttpGet("getUserByParticipation")]
        public async Task<IActionResult> GetUserByParticipation([FromQuery] GetUserByParticipationRequestModel request)
        {
            var query = _mapper.Map<GetUserByParticipationQuery>(request);

            ErrorOr<UserInfo> getUserResult = await _mediator.Send(query);

            return getUserResult.Match(
               result => Ok(result),
               errors => Problem(errors));
        }

        [HttpGet("findUsers")]
        public async Task<IActionResult> FindUsers([FromQuery] string? email, [FromQuery] string? firstName, [FromQuery] string? lastName)
        {
            var query = _mapper.Map<FindUsersQuery>(new FindUsersRequestModel(email, firstName, lastName));

            ErrorOr<List<User>> getUserResult = await _mediator.Send(query);

            /*return getUserResult.Match(
               result => Ok(result.Adapt<List<UserInfoResponse>>()),
               errors => Problem(errors));*/

            if (getUserResult.IsError)
            {
                var errors = getUserResult.Errors;
                return Problem(errors);
            }
            else
            {
                var result = getUserResult.Value;

                List<UserInfoResponse> mappedResult = new List<UserInfoResponse>();


                foreach (var user in result)
                {
                    mappedResult.Add(new UserInfoResponse(user.Id.Value, user.FirstName, user.LastName, user.ApplicationUser.Email));
                }

                return Ok(mappedResult);
            }
        }
    }
}
