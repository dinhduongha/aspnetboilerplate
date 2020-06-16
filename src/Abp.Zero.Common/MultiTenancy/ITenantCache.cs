using System;

namespace Abp.MultiTenancy
{
    public interface ITenantCache
    {
        TenantCacheItem Get(Guid tenantId);

        TenantCacheItem Get(string tenancyName);

        TenantCacheItem GetOrNull(string tenancyName);

        TenantCacheItem GetOrNull(Guid tenantId);
    }
}