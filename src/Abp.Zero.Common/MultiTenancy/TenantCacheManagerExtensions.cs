using Abp.Runtime.Caching;
using System;

namespace Abp.MultiTenancy
{
    public static class TenantCacheManagerExtensions
    {
        public static ITypedCache<Guid, TenantCacheItem> GetTenantCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<Guid, TenantCacheItem>(TenantCacheItem.CacheName);
        }

        public static ITypedCache<string, Guid?> GetTenantByNameCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<string, Guid?>(TenantCacheItem.ByNameCacheName);
        }
    }
}