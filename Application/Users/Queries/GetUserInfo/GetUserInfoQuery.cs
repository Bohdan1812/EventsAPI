using Application.Users.Dto;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetUserInfo
{
    public record GetUserInfoQuery(Guid UserId) : IRequest<ErrorOr<UserInfo>>;
}
