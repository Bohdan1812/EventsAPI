using Application.Authentication.Commands.Register;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Contracts.Authentication;
using Microsoft.AspNetCore.Identity;
using Domain.Common.Models;

namespace Api.Controllers
{
    [Route("Auth")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;

        private readonly IMapper _mapper;

        public AuthenticationController(IMediator mediator, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestModel request)
        {
            var command = _mapper.Map<RegisterCommand>(request);

            ErrorOr<string> authReult = await _mediator.Send(command);

            return authReult.Match(
                authReult => Ok(authReult),
                errors => Problem(errors));
        }
    }
}
