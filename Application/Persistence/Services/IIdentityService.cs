using ErrorOr;

namespace Application.Persistence.Services
{
    public interface IIdentityService
    {
        Task<ErrorOr<Guid>> RegisterUserAsync(
        string email, 
        string password, 
        string firstName, 
        string lastName);

        Task<ErrorOr<(string token, int expiryMinutes)>> LoginUserAsync(
            string email, 
            string password);
        
        Task<ErrorOr<Guid>> DeleteUserAsync(Guid appUserId, string password);
    }
}