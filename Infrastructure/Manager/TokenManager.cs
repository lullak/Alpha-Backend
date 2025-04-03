using Infrastructure.Data.Entities;
using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Manager
{
    public interface ITokenManager
    {
        string GenerateJwtToken(UserEntity user, string? role = null);
    }

    public class TokenManager(IConfiguration configuration) : ITokenManager
    {
        private readonly IConfiguration _configuration = configuration;

        public string GenerateJwtToken(UserEntity user, string? role = null)
        {
            try
            {
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!);
                var issuer = _configuration["Jwt:Issuer"]!;
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(ClaimTypes.Email, user.Email!)
                };

                if (role != null)
                    claims.Add(new Claim(ClaimTypes.Role, role));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = issuer,
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                return null!;
            }
        }
    }
}
