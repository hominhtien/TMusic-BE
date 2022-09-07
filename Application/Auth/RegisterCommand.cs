using Domain.Model;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TMusic.Service.Domain;

namespace Application.Auth
{
    internal class RegisterHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly MainDbContext _mainDbContext;

        public RegisterHandler(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var checkExist = await _mainDbContext.Users.SingleOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
            if (checkExist is not null)
            {
                throw new Exception("Email đã tồn tại user trên hệ thống");
            }
            using var hma = new HMACSHA512();
            _mainDbContext.Add(new User
            {
                Email = request.Email,
                Active = true,
                Avatar = null,
                Vip = null,
                DateOfBirth = request.DateOfBirth,
                FullName = request.FullName,
                ExpiryVipDate = DateTime.UtcNow,
                PassWordHash = hma.ComputeHash(Encoding.UTF8.GetBytes(request.PassWord)), 
                PassWordSalt = hma.Key,
                Sex = request.Sex,
                Description = request.Description,
                Uid = request.Uid,
                Verified = false
            });
            await _mainDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    public record RegisterCommand : IRequest<Unit>
    {
        public string PassWord { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        public string Description { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Avatar { get; set; }

        public string Sex { get; set; }
        public string Online { get; set; }

        public string Vip { get; set; }

        public DateTime ExpiryVipDate { get; set; }
        public string? Uid { get; set; }
        public bool Enable { get; set; }
        public bool Active { get; set; }
    }

    internal class RegisterCommandValidator : AbstractValidator<RegisterCommand> { }
}
