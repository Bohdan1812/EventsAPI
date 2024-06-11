using Application.Events.Dto;
using Application.Users.Dto;

namespace Application.JoinRequests.Dto
{
    public record  JoinRequestInfoResponse(Guid Id, UserInfo User, EventInfo Event);
}
