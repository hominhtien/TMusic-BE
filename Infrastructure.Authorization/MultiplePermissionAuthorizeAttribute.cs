using Infrastructure.Authorization.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class MultiplePermissionAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _permissions;
        private readonly bool _isAnd;

        public MultiplePermissionAuthorizeAttribute(string permissions, bool isAnd = false)
        {
            _permissions = permissions;
            _isAnd = isAnd;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var claims = user.FindAll(AppClaimTypes.Permission);
            var permissions = claims.Select(c => c.Value);
            if (string.IsNullOrEmpty(_permissions) || !permissions.Any())
            {
                context.Result = new StatusCodeResult(403);
                return;
            }
            var listPermissionCheck = _permissions.Split(",").Select(v => v.Trim()).ToList();
            if (_isAnd)
            {
                if (listPermissionCheck.Any(x => !permissions.Contains(x)))
                {
                    context.Result = new StatusCodeResult(403);
                    return;
                }
            }
            else
            {
                if (!listPermissionCheck.Any(x => permissions.Contains(x)))
                {
                    context.Result = new StatusCodeResult(403);
                    return;
                }
            }

            return;
        }
    }
}
