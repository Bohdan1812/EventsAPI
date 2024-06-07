using Application.Common.Errors;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using ErrorOr;
using MediatR;

namespace Application.Users.Commands.RemoveUserPhoto
{
    public class RemoveUserPhotoCommandHandler : IRequestHandler<RemoveUserPhotoCommand, ErrorOr<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserPhotoService _userPhotoService;

        public RemoveUserPhotoCommandHandler(IUserRepository userRepository, IUserPhotoService userPhotoService)
        {
            _userRepository = userRepository;
            _userPhotoService = userPhotoService;
        }

        public async Task<ErrorOr<string>> Handle(RemoveUserPhotoCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicationUserId);

            if (user is null)
                return UserError.UserNotFound;

            if (string.IsNullOrEmpty(user.PhotoPath))
                return "User doen not conrain photo";

            bool isDeleted = false;

            try
            {
                isDeleted = _userPhotoService.DeleteImage(user.PhotoPath);   
            }
            catch (Exception ex)
            {
                return UserError.UserPhotoDeleteError(ex.Message);
            }

            if (!isDeleted)
                return UserError.UserPreviousPhotoNotDeleted;

            user.PhotoPath = null;

            await _userRepository.Update(user);

            user = await _userRepository.GetUser(user.Id);

            if (user is not null && user.PhotoPath is null)
                return "User photo is deleted successfully!";

            return UserError.UserPhotoDeleteError("User photo was not deleted");
        }
    }
}
