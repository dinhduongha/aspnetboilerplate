using System;
using System.Security.Claims;
using Abp.Runtime.Security;

namespace Abp.Authorization
{
    internal static class AbpZeroClaimsIdentityHelper
    {
        public static Guid? GetTenantId(ClaimsPrincipal principal)
        {
            var tenantIdOrNull = principal?.FindFirstValue(AbpClaimTypes.TenantId);
            if (tenantIdOrNull == null)
            {
                return null;
            }
            return Guid.Parse(tenantIdOrNull);
            //return Convert.ToInt32(tenantIdOrNull);
        }
    }
}