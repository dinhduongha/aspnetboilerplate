using System;
using System.Linq;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.MultiTenancy;
using Abp.Runtime.Security;

namespace Abp.Runtime.Session
{
    /// <summary>
    /// Implements <see cref="IAbpSession"/> to get session properties from current claims.
    /// </summary>
    public class ClaimsAbpSession : AbpSessionBase, ISingletonDependency
    {
        public override Guid? UserId
        {
            get
            {
                if (OverridedValue != null)
                {
                    return OverridedValue.UserId;
                }

                var userIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.UserId);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }

                Guid userId;
                if (!Guid.TryParse(userIdClaim.Value, out userId))
                {
                    return null;
                }

                return userId;
            }
        }

        public override Guid? TenantId
        {
            get
            {
                if (!MultiTenancy.IsEnabled)
                {
                    return MultiTenancyConsts.DefaultTenantId;
                }

                if (OverridedValue != null)
                {
                    return OverridedValue.TenantId;
                }

                var tenantIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.TenantId);
                if (!string.IsNullOrEmpty(tenantIdClaim?.Value))
                {
                    return Guid.Parse(tenantIdClaim.Value);
                    //return Convert.ToInt32(tenantIdClaim.Value);
                }

                if (UserId == null)
                {
                    //Resolve tenant id from request only if user has not logged in!
                    return TenantResolver.ResolveTenantId();
                }
                
                return null;
            }
        }

        public override Guid? ImpersonatorUserId
        {
            get
            {
                var impersonatorUserIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.ImpersonatorUserId);
                if (string.IsNullOrEmpty(impersonatorUserIdClaim?.Value))
                {
                    return null;
                }
                return Guid.Parse(impersonatorUserIdClaim.Value);
            }
        }

        public override Guid? ImpersonatorTenantId
        {
            get
            {
                if (!MultiTenancy.IsEnabled)
                {
                    return MultiTenancyConsts.DefaultTenantId;
                }

                var impersonatorTenantIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.ImpersonatorTenantId);
                if (string.IsNullOrEmpty(impersonatorTenantIdClaim?.Value))
                {
                    return null;
                }

                return Guid.Parse(impersonatorTenantIdClaim.Value);
                //return Convert.ToInt32(impersonatorTenantIdClaim.Value);
            }
        }

        protected IPrincipalAccessor PrincipalAccessor { get; }
        protected ITenantResolver TenantResolver { get; }

        public ClaimsAbpSession(
            IPrincipalAccessor principalAccessor,
            IMultiTenancyConfig multiTenancy,
            ITenantResolver tenantResolver,
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider)
            : base(
                  multiTenancy, 
                  sessionOverrideScopeProvider)
        {
            TenantResolver = tenantResolver;
            PrincipalAccessor = principalAccessor;
        }
    }
}