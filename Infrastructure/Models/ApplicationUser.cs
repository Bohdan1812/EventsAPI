using Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public User? User { get; set; } 

    }
}
