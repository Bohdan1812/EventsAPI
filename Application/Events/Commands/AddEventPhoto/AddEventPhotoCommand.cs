using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Events.Commands.AddEventPhoto
{
    public record AddEventPhotoCommand(Guid ApplicationUserId, Guid EventId, IFormFile Photo) : IRequest<ErrorOr<string>>;
}
