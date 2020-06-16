using Abp.Configuration.Startup;
using Abp.MultiTenancy;
using Abp.Runtime.Remoting;
using System;

namespace Abp.Runtime.Session
{
    /// <summary>
    /// Implements null object pattern for <see cref="IAbpSession"/>.
    /// </summary>
    public class NullAbpSession : AbpSessionBase
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullAbpSession Instance { get; } = new NullAbpSession();

        /// <inheritdoc/>
        public override Guid? UserId => null;

        /// <inheritdoc/>
        public override Guid? TenantId => null;

        public override MultiTenancySides MultiTenancySide => MultiTenancySides.Tenant;

        public override Guid? ImpersonatorUserId => null;

        public override Guid? ImpersonatorTenantId => null;

        private NullAbpSession() 
            : base(
                  new MultiTenancyConfig(), 
                  new DataContextAmbientScopeProvider<SessionOverride>(new AsyncLocalAmbientDataContext())
            )
        {

        }
    }
}