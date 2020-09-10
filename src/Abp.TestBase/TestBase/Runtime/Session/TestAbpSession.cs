using System;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.MultiTenancy;
using Abp.Runtime;
using Abp.Runtime.Session;

namespace Abp.TestBase.Runtime.Session
{
    public class TestAbpSession : IAbpSession, ISingletonDependency
    {
        public virtual Guid? UserId
        {
            get
            {
                if (_sessionOverrideScopeProvider.GetValue(AbpSessionBase.SessionOverrideContextKey) != null)
                {
                    return _sessionOverrideScopeProvider.GetValue(AbpSessionBase.SessionOverrideContextKey).UserId;
                }

                return _userId;
            }
            set { _userId = value; }
        }

        public virtual Guid? TenantId
        {
            get
            {
                if (!_multiTenancy.IsEnabled)
                {
                    // TODO: DinhHa
                    //return null;
                    //return 1;
                    return new Guid("00000000-0000-0000-0000-000000000001");
                }

                if (_sessionOverrideScopeProvider.GetValue(AbpSessionBase.SessionOverrideContextKey) != null)
                {
                    return _sessionOverrideScopeProvider.GetValue(AbpSessionBase.SessionOverrideContextKey).TenantId;
                }

                var resolvedValue = _tenantResolver.ResolveTenantId();
                if (resolvedValue != null)
                {
                    return resolvedValue;
                }

                return _tenantId;
            }
            set
            {
                //if (!_multiTenancy.IsEnabled && value != 1 && value != null)
                if (!_multiTenancy.IsEnabled && value != Guid.Empty && value != null)
                {
                    throw new AbpException("Can not set TenantId since multi-tenancy is not enabled. Use IMultiTenancyConfig.IsEnabled to enable it.");
                }

                _tenantId = value;
            }
        }

        public virtual MultiTenancySides MultiTenancySide { get { return GetCurrentMultiTenancySide(); } }
        
        public virtual Guid? ImpersonatorUserId { get; set; }
        
        public virtual Guid? ImpersonatorTenantId { get; set; }

        private readonly IMultiTenancyConfig _multiTenancy;
        private readonly IAmbientScopeProvider<SessionOverride> _sessionOverrideScopeProvider;
        private readonly ITenantResolver _tenantResolver;
        private Guid? _tenantId;
        private Guid? _userId;

        public TestAbpSession(
            IMultiTenancyConfig multiTenancy, 
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider,
            ITenantResolver tenantResolver)
        {
            _multiTenancy = multiTenancy;
            _sessionOverrideScopeProvider = sessionOverrideScopeProvider;
            _tenantResolver = tenantResolver;
        }

        protected virtual MultiTenancySides GetCurrentMultiTenancySide()
        {
            return _multiTenancy.IsEnabled && !TenantId.HasValue
                ? MultiTenancySides.Host
                : MultiTenancySides.Tenant;
        }

        public virtual IDisposable Use(Guid? tenantId, Guid? userId)
        {
            return _sessionOverrideScopeProvider.BeginScope(AbpSessionBase.SessionOverrideContextKey, new SessionOverride(tenantId, userId));
        }
    }
}