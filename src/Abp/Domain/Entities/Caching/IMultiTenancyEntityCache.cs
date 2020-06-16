using Abp.Runtime.Caching;
using System;

namespace Abp.Domain.Entities.Caching
{
    public interface IMultiTenancyEntityCache<TCacheItem> : IMultiTenancyEntityCache<TCacheItem, Guid>
    {
    }

    public interface IMultiTenancyEntityCache<TCacheItem, TPrimaryKey> : IEntityCacheBase<TCacheItem, TPrimaryKey>
    {
        ITypedCache<string, TCacheItem> InternalCache { get; }

        string GetCacheKey(TPrimaryKey id);

        string GetCacheKey(TPrimaryKey id, Guid? tenantId);
    }
}
