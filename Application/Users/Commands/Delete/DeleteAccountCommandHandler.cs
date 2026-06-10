using Application.Common.Errors;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using ErrorOr;
using MediatR;

namespace Application.Users.Commands.Delete
{
    public class DeleteAccountCommandHandler
        : IRequestHandler<DeleteAccountCommand, ErrorOr<string>>
    {
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountCommandHandler(IIdentityService identityService, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _identityService = identityService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<string>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.AppUserId);

            if  (user is null)
                return UserError.UserNotFound;
            
            user = await _userRepository.GetFullUser(user.Id);

            if (user is null)
                 return UserError.UserNotFound;


            if (user.Organizer.Events.Count > 0)
                return OrganizerError.OrganizerContiansEvents;
            
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            
            try
            {
                var result = await _identityService.DeleteUserAsync(request.AppUserId, request.Password);
                
                if (result.IsError)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return result.Errors;
                }
                
                await _userRepository.Remove(user.Id);

                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                
                return $"Account {result.Value} deleted successfully!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return ApplicationUserError.ApplicationUserNotDeleted("500", ex.Message);
            }
        }
    }
}
