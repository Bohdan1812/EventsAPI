using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Users.Commands.SetUserPhoto
{
    public record SetUserPhotoCommand(
        Guid ApplicationUserId, 
        IFormFile Photo) : IRequest<ErrorOr<string>>;
}
