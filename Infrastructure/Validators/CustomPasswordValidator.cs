using Domain.Common.Models;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace Infrastructure.Valdators
{
    public class CustomPasswordValidator : IPasswordValidator<ApplicationUser>
    {
        /*
        private readonly EventAppDbContext _context;

        public CustomPasswordValidator(EventAppDbContext context)
        {
            _context = context;
        }
        */
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
        {
            /*
            if (_context.UserExemptions.Where(e => e.UserId == user.Id) != null)
            {
                Task.FromResult(IdentityResult.Success);
            }
            */
            var hasAlternatingLetterAndDigit = Regex.IsMatch(password, @"(([a-zA-Z]+\d+[a-zA-Z]))+")
                && Regex.IsMatch(password, @"^[a-zA-Z].*[a-zA-Z]$");


            return hasAlternatingLetterAndDigit
                ? Task.FromResult(IdentityResult.Success)
                : Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "PasswordRequiresAlternatingLetterAndDigit",
                    Description = "The password must be a sequence of latin letters, numbers and letters again"
                }));
        }
    }
}
