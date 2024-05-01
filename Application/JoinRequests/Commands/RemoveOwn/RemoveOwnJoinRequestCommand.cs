using ErrorOr;
using MediatR;

namespace Application.JoinRequests.Commands.RemoveOwn
{
    public record RemoveOwnJoinRequestCommand(Guid ApplicationUserId, Guid JoinRequestId) 
        : IRequest<ErrorOr<string>>;
}
