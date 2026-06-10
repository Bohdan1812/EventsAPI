using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Models;

namespace Application.Persistence.Services.Authentication
{
    public class JwtTokenGenerator// : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        public string GenerateToken(ApplicationUser user)
        {
            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException("User email cannot be null or empty.", nameof(user.Email));

            var jwtSettings = _configuration.GetSection("JwtSettings");

            if (jwtSettings == null)
                throw new InvalidOperationException("JWT settings are not configured properly."); 
            
            var secretKey = jwtSettings["SecretKey"];
            
            if (string.IsNullOrEmpty(secretKey))
                throw new InvalidOperationException("JWT SecretKey is not configured.");
            
            var issuer = jwtSettings["Issuer"];

            if (string.IsNullOrEmpty(issuer))
                throw new InvalidOperationException("JWT Issuer is not configured.");

            var audience = jwtSettings["Audience"];
            
            if (string.IsNullOrEmpty(audience))
                throw new InvalidOperationException("JWT Audience is not configured.");

            var expiryMinutesStr = jwtSettings["ExpiryMinutes"];
           
            if (string.IsNullOrEmpty(expiryMinutesStr) || !int.TryParse(expiryMinutesStr, out int expiryMinutes))
                throw new InvalidOperationException("JWT ExpiryMinutes is not configured or is not a valid integer.");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(expiryMinutesStr)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public int GetExpiryMinutes()
        {
            var expiryMinutes = _configuration.GetSection("JwtSettings")["ExpiryMinutes"];

            if (string.IsNullOrEmpty(expiryMinutes))
                return 0;

            return int.Parse(expiryMinutes);
        }
    }
}