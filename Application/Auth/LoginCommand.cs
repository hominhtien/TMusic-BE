using Amazon.Runtime.Internal;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TMusic.Service.Domain;
using Tmusic.Extentions;
using System.Security.Cryptography;
using Application.Services;

namespace Application.Auth
{
    internal class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly MainDbContext _MainDbContext;
        private readonly TokenService _tokenService;
        public LoginHandler(MainDbContext mainDbContext, TokenService tokenService)
        {
            _MainDbContext = mainDbContext;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _MainDbContext.Users.Where(x => x.Email == request.UserName).SingleOrDefaultAsync(cancellationToken);
            if (user is null) throw CoreException.NotFound("User not found");

            using var hma = new HMACSHA512(user.PassWordSalt);
            var computedHash = hma.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PassWordHash[i])
                {
                    throw CoreException.Exception("Mật khẩu không chính xác");
                }
            }
            return _tokenService.GetToken(user);
        }
    }
    public record LoginCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    internal class DeleteRoutePricingAutoSettingValidator : AbstractValidator<LoginCommand> { }

}
