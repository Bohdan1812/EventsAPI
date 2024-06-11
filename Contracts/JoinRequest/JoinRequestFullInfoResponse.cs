using Contracts.Event;
using Contracts.User;

namespace Contracts.JoinRequest
{
    public record JoinRequestFullInfoResponse(Guid Id, UserInfoResponse User, EventResponse Event);
}
