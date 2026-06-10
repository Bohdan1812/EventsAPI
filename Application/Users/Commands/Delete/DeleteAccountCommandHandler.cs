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

        public DeleteAccountCommandHandler(IIdentityService identityService, IUserRepository userRepository)
        {
            _identityService = identityService;
            _userRepository = userRepository;
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
            

            var result = await _identityService.DeleteUserAsync(request.AppUserId, request.Password);
            
            if (result.IsError)
                return result.Errors;
            
            await _userRepository.Remove(user.Id);
            
            return $"Account {result.Value} deleted successfully!";
        }
    }
}
