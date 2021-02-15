using System.Threading.Tasks;

namespace Abp.MultiTenancy
{
    public interface ITenantCache
    {
        TenantCacheItem Get(long tenantId);

        TenantCacheItem Get(string tenancyName);

        TenantCacheItem GetOrNull(string tenancyName);

        TenantCacheItem GetOrNull(long tenantId);

        Task<TenantCacheItem> GetAsync(long tenantId);

        Task<TenantCacheItem> GetAsync(string tenancyName);

        Task<TenantCacheItem> GetOrNullAsync(string tenancyName);

        Task<TenantCacheItem> GetOrNullAsync(long tenantId);
    }
}
