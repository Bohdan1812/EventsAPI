using Domain.Common.Models;

namespace Application.Persistence.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser user);
    }
}