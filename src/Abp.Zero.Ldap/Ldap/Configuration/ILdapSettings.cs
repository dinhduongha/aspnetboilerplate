using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;

namespace Abp.Zero.Ldap.Configuration
{
    /// <summary>
    /// Used to obtain current values of LDAP settings.
    /// This abstraction allows to define a different source for settings than SettingManager (see default implementation: <see cref="LdapSettings"/>).
    /// </summary>
    public interface ILdapSettings
    {
        Task<bool> GetIsEnabled(long? tenantId);

        Task<ContextType> GetContextType(long? tenantId);

        Task<string> GetContainer(long? tenantId);

        Task<string> GetDomain(long? tenantId);

        Task<string> GetUserName(long? tenantId);

        Task<string> GetPassword(long? tenantId);
    }
}