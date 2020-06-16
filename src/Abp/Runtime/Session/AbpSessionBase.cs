using System;
using Abp.Configuration.Startup;
using Abp.MultiTenancy;

namespace Abp.Runtime.Session
{
    public abstract class AbpSessionBase : IAbpSession
    {
        public const string SessionOverrideContextKey = "Abp.Runtime.Session.Override";

        public IMultiTenancyConfig MultiTenancy { get; }

        public abstract Guid? UserId { get; }

        public abstract Guid? TenantId { get; }

        public abstract Guid? ImpersonatorUserId { get; }

        public abstract Guid? ImpersonatorTenantId { get; }

        public virtual MultiTenancySides MultiTenancySide
        {
            get
            {
                return MultiTenancy.IsEnabled && !TenantId.HasValue
                    ? MultiTenancySides.Host
                    : MultiTenancySides.Tenant;
            }
        }

        protected SessionOverride OverridedValue => SessionOverrideScopeProvider.GetValue(SessionOverrideContextKey);
        protected IAmbientScopeProvider<SessionOverride> SessionOverrideScopeProvider { get; }

        protected AbpSessionBase(IMultiTenancyConfig multiTenancy, IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider)
        {
            MultiTenancy = multiTenancy;
            SessionOverrideScopeProvider = sessionOverrideScopeProvider;
        }

        public IDisposable Use(Guid? tenantId, Guid? userId)
        {
            return SessionOverrideScopeProvider.BeginScope(SessionOverrideContextKey, new SessionOverride(tenantId, userId));
        }
    }
}