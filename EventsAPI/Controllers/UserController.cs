using Application.Users.Commands.Delete;
using Application.Users.Commands.Update;
using Contracts.Authentication;
using Contracts.User;
using Domain.Common.Models;
using ErrorOr;
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
            var appUser = await _userManager.GetUserAsync(User);

            if (appUser is null)
            {
                return BadRequest("User not found!");
            }

            var command = _mapper.Map<DeleteAccountCommand>((appUser.Id, request));

            ErrorOr<string> deleteResult = await _mediator.Send(command);

            return deleteResult.Match(
               authReult => Ok(authReult),
               errors => Problem(errors));
        }
    }
}
