using ErrorOr;
using MediatR;

namespace Application.Users.Commands.RemoveUserPhoto
{
    public record RemoveUserPhotoCommand(Guid ApplicationUserId) : IRequest<ErrorOr<string>>;
}
