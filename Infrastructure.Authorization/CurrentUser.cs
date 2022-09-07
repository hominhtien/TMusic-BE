using Infrastructure.Authorization.Common;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Tmusic.Domain;

namespace Infrastructure.Authorization
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected ClaimsPrincipal ClaimsPrincipal => _httpContextAccessor.HttpContext?.User;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated => ClaimsPrincipal is not null && ClaimsPrincipal.Identity.IsAuthenticated;

        public Guid? Id => ClaimsPrincipal?.FindUserId();

        public string UserName => ClaimsPrincipal?.FindClaimValue(AppClaimTypes.UserName);

        public string Name => ClaimsPrincipal?.FindClaimValue(AppClaimTypes.Name);

        public string PhoneNumber => ClaimsPrincipal?.FindClaimValue(AppClaimTypes.PhoneNumber);

        public bool PhoneNumberVerified => string.Equals(ClaimsPrincipal?.FindClaimValue(AppClaimTypes.PhoneNumberVerified), "true", StringComparison.InvariantCultureIgnoreCase);

        public string Email => ClaimsPrincipal?.FindClaimValue(AppClaimTypes.Email);

        public bool EmailVerified => string.Equals(ClaimsPrincipal?.FindClaimValue(AppClaimTypes.EmailVerified), "true", StringComparison.InvariantCultureIgnoreCase);

        public string[] Roles => FindClaims(AppClaimTypes.Role).Select(c => c.Value).ToArray();

        public string[] Permissions => FindClaims(AppClaimTypes.Permission).Select(c => c.Value).ToArray();

        public Claim FindClaim(string claimType) => ClaimsPrincipal?.Claims.FirstOrDefault(x => x.Type == claimType);

        public IEnumerable<Claim> FindClaims(string claimType) => ClaimsPrincipal?.Claims.Where(x => x.Type == claimType);

        public IEnumerable<Claim> GetAllClaims() => ClaimsPrincipal?.Claims;

        public bool IsInRole(string roleName) => ClaimsPrincipal is not null && ClaimsPrincipal.IsInRole(roleName);

        public string LanguageCode => string.Empty;
        public string ClientId => GetClientId();
        private string GetClientId()
        {
            Claim clientIdClaim = FindClaim(AppClaimTypes.ClientId);
            return clientIdClaim != null ? clientIdClaim.Value : string.Empty;
        }

    }
}
