using Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Users.Commands.Update
{
    public record UpdateUserCommand
    (
        Guid appUserId,
        string FirstName,
        string LastName
    ) : IRequest<ErrorOr<string>>;
}
