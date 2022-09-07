using Domain.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetToken(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, user.FullName));
            claims.Add(new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()));
            claims.Add(new Claim(nameof(user.Email), user.Email));
            claims.Add(new Claim(nameof(user.Vip), user.Vip));
            claims.Add(new Claim(nameof(user.ExpiryVipDate), user.ExpiryVipDate.ToString()));
            claims.Add(new Claim(nameof(user.Id), user.Id.ToString()));
            claims.Add(new Claim(nameof(user.Sex), user.Sex));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
