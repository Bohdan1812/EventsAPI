using Domain.UserAggregate;
using Domain.UserAggregate.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Domain.Common.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public User? User { get; set; } 

    }
}
