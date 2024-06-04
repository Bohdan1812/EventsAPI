using Application.Users.Dto;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetParticipantsUserInfo
{
    public record GetParticipantsUserInfoQuery(
        Guid ApplicatinUserId,
        Guid EventId) : IRequest<ErrorOr<List<UserInfo>>>;
   
}
