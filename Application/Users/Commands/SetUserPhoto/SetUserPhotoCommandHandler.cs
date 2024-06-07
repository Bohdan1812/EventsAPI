using Application.Common.Errors;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using ErrorOr;
using MediatR;

namespace Application.Users.Commands.SetUserPhoto
{
    public class SetUserPhotoCommandHandler : IRequestHandler<SetUserPhotoCommand, ErrorOr<string>>
    {
        private readonly IUserPhotoService _userPhotoService;
        private readonly IUserRepository _userRepository;

        public SetUserPhotoCommandHandler(
            IUserPhotoService userPhotoService, 
            IUserRepository userRepository)
        {
            _userPhotoService = userPhotoService;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<string>> Handle(SetUserPhotoCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicationUserId);

            if (user is null)
                return UserError.UserNotFound;

            if(!String.IsNullOrEmpty(user.PhotoPath))
            {
                var previousDeleted = false;
                try
                {
                    previousDeleted = _userPhotoService.DeleteImage(user.PhotoPath);
                } 
                catch (Exception ex) 
                {
                    return UserError.UserPhotoDeleteError(ex.Message);
                }  
                if(!previousDeleted)
                    return UserError.UserPreviousPhotoNotDeleted;
            }

            var path = string.Empty;

            try
            {
                path = _userPhotoService.SaveImage(request.Photo);
            }
            catch (Exception ex) 
            {
                return UserError.UserPhotoUploadError(ex.Message);
            }

            user.PhotoPath = path;

            await _userRepository.Update(user);

            user = await _userRepository.GetUser(user.Id);

            if (user is null)
                return UserError.UserNotFound;

            if (!string.IsNullOrEmpty(user.PhotoPath) &&
                user.PhotoPath == path) 
            {
                return "User photo uploaded successfully!";
            }
            
            return UserError.PhotoNotUploaded;
        }
    }
}
