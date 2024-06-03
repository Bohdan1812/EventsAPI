using Domain.UserAggregate;
using Domain.UserAggregate.ValueObjects;

namespace Application.Persistence.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUser(Guid appUserId);
        Task<User?> GetUser(UserId userId);
        Task<User?> GetFullUser(UserId userId);

        Task<List<User>> FindUsers(string email, string firstName, string secondName);

        Task Add(User user);

        Task Remove(UserId userId);

        Task Update(User user);

    }
}
