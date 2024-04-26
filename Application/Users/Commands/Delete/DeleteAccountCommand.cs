using Domain.Common.Models;
using ErrorOr;
using MediatR;

namespace Application.Users.Commands.Delete
{
    public record DeleteAccountCommand
    (
         Guid AppUserId,
         string Password
    ) : IRequest<ErrorOr<string>>;
}
