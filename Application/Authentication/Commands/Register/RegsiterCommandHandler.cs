using MediatR;
using ErrorOr;
using Application.Persistence.Services;
using Application.Persistence.Repositories;
using Application.Common.Errors;
using Domain.UserAggregate;

namespace Application.Authentication.Commands.Register
{

    public class RegsiterCommandHandler
        : IRequestHandler<RegisterCommand, ErrorOr<string>>
    {
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public RegsiterCommandHandler(IIdentityService identityService, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _identityService = identityService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var result = await _identityService.RegisterUserAsync(request.Email, request.Password, request.FirstName, request.LastName);     
                
                if (result.IsError)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return result.Errors;
                }
                
                var user = new User(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    result.Value);

                await _userRepository.Add(user);
                
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return $"User {result.Value} registered successfully";
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return ApplicationUserError.ApplicationUserUnexpectedError("500", ex.Message);
            }
        }
    }
}
    

