using Infrastructure.Authorization.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using Tmusic.Domain;

namespace Infrastructure.Authorization
{
    public static class AuthorizationServiceCollectionExtensions
    {
        private static class AuthenticationScheme
        {
            public const string Bearer = "Bearer";
            public const string Introspection = "Introspection";
        }

        private static class Selector
        {
            /// <summary>
            /// Provides a forwarding func for JWT vs reference tokens (based on existence of dot in token)
            /// </summary>
            /// <returns></returns>
            public static Func<HttpContext, string> ForwardReferenceToken = (context) =>
            {
                var (scheme, credential) = GetSchemeAndCredential(context);

                if (scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase) &&
                    !credential.Contains("."))
                {
                    return AuthenticationScheme.Introspection;
                }

                return null;
            };

            /// <summary>
            /// Extracts scheme and credential from Authorization header (if present)
            /// </summary>
            /// <param name="context"></param>
            /// <returns></returns>
            public static (string, string) GetSchemeAndCredential(HttpContext context)
            {
                var header = context.Request.Headers[HeaderNames.Authorization].FirstOrDefault();

                if (string.IsNullOrEmpty(header))
                {
                    return ("", "");
                }

                var parts = header.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                {
                    return ("", "");
                }

                return (parts[0], parts[1]);
            }

        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUser>();
            return services;
        }
        public static string FindClaimValue(this ClaimsPrincipal principal, string claimType)
        {
            return principal.Claims.FirstOrDefault(x => x.Type == claimType)?.Value;
        }

        public static Guid? FindUserId(this ClaimsPrincipal principal)
        {
            var userIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == AppClaimTypes.UserId);
            if (userIdOrNull == null || string.IsNullOrEmpty(userIdOrNull.Value))
            {
                return null;
            }
            if (Guid.TryParse(userIdOrNull.Value, out Guid result))
            {
                return result;
            }
            return null;
        }

    }
}
