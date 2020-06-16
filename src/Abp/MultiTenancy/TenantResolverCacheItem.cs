using System;

namespace Abp.MultiTenancy
{
    public class TenantResolverCacheItem
    {
        public Guid? TenantId { get; }

        public TenantResolverCacheItem(Guid? tenantId)
        {
            TenantId = tenantId;
        }
    }
}