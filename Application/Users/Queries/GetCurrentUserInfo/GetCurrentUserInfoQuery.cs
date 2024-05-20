using Application.Users.Dto;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetCurrentUserInfo
{
    public record class GetCurrentUserInfoQuery(Guid ApplicationUserId) : IRequest<ErrorOr<UserInfo>>;
}
