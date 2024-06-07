using Application.Users.Dto;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetOrganizerInfo
{
    public record GetOrganizerInfoQuery(Guid OrganizerId) : IRequest<ErrorOr<UserInfo>>;
}
