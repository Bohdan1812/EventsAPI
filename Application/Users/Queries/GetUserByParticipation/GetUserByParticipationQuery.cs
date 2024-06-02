using Application.Users.Dto;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetUserByParticipation
{
    public record GetUserByParticipationQuery(Guid ParticipationId) : IRequest<ErrorOr<UserInfo>>;
}
