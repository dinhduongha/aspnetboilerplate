using System;
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Extensions;

namespace Abp.Zero.Ldap.Configuration
{
    /// <summary>
    /// Implements <see cref="ILdapSettings"/> to get settings from <see cref="ISettingManager"/>.
    /// </summary>
    
    public class LdapSettings : ILdapSettings, ITransientDependency
    {
        protected ISettingManager SettingManager { get; }

        public LdapSettings(ISettingManager settingManager)
        {
            SettingManager = settingManager;
        }

        public virtual Task<bool> GetIsEnabled(Guid? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync<bool>(LdapSettingNames.IsEnabled, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync<bool>(LdapSettingNames.IsEnabled);
        }

        public virtual async Task<ContextType> GetContextType(Guid? tenantId)
        {
            return tenantId.HasValue
                ? (await SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.ContextType, tenantId.Value)).ToEnum<ContextType>()
                : (await SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.ContextType)).ToEnum<ContextType>();
        }

        public virtual Task<string> GetContainer(Guid? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.Container, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.Container);
        }

        public virtual Task<string> GetDomain(Guid? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.Domain, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.Domain);
        }

        public virtual Task<string> GetUserName(Guid? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.UserName, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.UserName);
        }

        public virtual Task<string> GetPassword(Guid? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.Password, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.Password);
        }
    }
}