namespace Abp.MultiTenancy
{
    public class TenantResolverCacheItem
    {
        public long? TenantId { get; }

        public TenantResolverCacheItem(long? tenantId)
        {
            TenantId = tenantId;
        }
    }
}