using MediatR;
using ErrorOr;
using Application.Persistence.Services;

namespace Application.Authentication.Commands.Register
{

    public class RegsiterCommandHandler
        : IRequestHandler<RegisterCommand, ErrorOr<string>>
    {
        private readonly IIdentityService _identityService;
        public RegsiterCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ErrorOr<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.RegisterUserAsync(request.Email, request.Password, request.FirstName, request.LastName);     
            
            if (result.IsError)
                return result.Errors;
            
            return $"User {result.Value} registered successfully";
        }
    }
}
    

