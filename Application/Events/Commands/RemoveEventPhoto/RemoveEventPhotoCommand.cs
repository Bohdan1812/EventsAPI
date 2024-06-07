using ErrorOr;
using MediatR;

namespace Application.Events.Commands.RemoveEventPhoto
{
    public record RemoveEventPhotoCommand(Guid ApplicationUserId, Guid EventId) : IRequest<ErrorOr<string>>;
}
