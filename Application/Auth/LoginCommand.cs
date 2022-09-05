using Amazon.Runtime.Internal;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Auth
{
    internal class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IConfiguration  _configuration;

        public LoginHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if(request.UserName == "Tien")
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.GivenName, "Tien"));
                claims.Add(new Claim(ClaimTypes.Surname, "Ho"));
                claims.Add(new Claim(ClaimTypes.Email, "Tien@gmail.com"));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(20),
                    signingCredentials: credentials);
                var res = new JwtSecurityTokenHandler().WriteToken(token);
                return res;
            }
            return "";
        }
    }
    public record LoginCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    internal class DeleteRoutePricingAutoSettingValidator : AbstractValidator<LoginCommand> { }

}
