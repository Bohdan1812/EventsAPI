using ErrorOr;
using MediatR;

namespace Application.JoinRequests.Commands.Add
{
    public record AddJoinRequestCommand(
        Guid ApplicatioUserId,
        Guid EventId) : IRequest<ErrorOr<string>>;
    
}
