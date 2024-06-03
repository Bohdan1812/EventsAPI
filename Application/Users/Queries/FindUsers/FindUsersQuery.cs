using Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.FindUsers
{
    public record FindUsersQuery(string Email, string FirstName, string LastName) : IRequest<ErrorOr<List<User>>> ;
}
