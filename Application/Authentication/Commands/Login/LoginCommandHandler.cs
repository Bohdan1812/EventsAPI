using Application.Persistence.Services;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<(string token, int expiryMinutes)>>
    {
        private readonly IIdentityService _identityService;
        public LoginCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        
        public async Task<ErrorOr<(string token, int expiryMinutes)>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var loginResult = await _identityService.LoginUserAsync(request.Email, request.Password);
            
            return loginResult;
        }
    }
}
