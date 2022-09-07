using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authorization
{
    public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;

        public PermissionAuthorizationPolicyProvider(IPermissionDefinitionManager permissionDefinitionManager, IOptions<AuthorizationOptions> options) : base(options)
        {
            _permissionDefinitionManager = permissionDefinitionManager;
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);
            if (policy != null)
            {
                return policy;
            }

            var permission = _permissionDefinitionManager.GetOrNull(policyName);
            if (permission != null)
            {
                var policyBuilder = new AuthorizationPolicyBuilder(Array.Empty<string>());
                policyBuilder.Requirements.Add(new PermissionRequirement(policyName));
                return policyBuilder.Build();
            }

            return null;
        }
    }
}
