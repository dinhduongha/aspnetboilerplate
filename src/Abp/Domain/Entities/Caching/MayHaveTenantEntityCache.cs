using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using System;

namespace Abp.Domain.Entities.Caching
{
    public class MayHaveTenantEntityCache<TEntity, TCacheItem> :
        MayHaveTenantEntityCache<TEntity, TCacheItem, Guid>,
        IMultiTenancyEntityCache<TCacheItem>
        where TEntity : class, IEntity<Guid>, IMayHaveTenant
    {
        public MayHaveTenantEntityCache(
            ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<TEntity, Guid> repository,
            string cacheName = null)
            : base(
                cacheManager,
                unitOfWorkManager,
                repository,
                cacheName)
        {
        }
    }

    public class MayHaveTenantEntityCache<TEntity, TCacheItem, TPrimaryKey> : MultiTenancyEntityCache<TEntity, TCacheItem, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>, IMayHaveTenant
    {
        public MayHaveTenantEntityCache(
            ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<TEntity, TPrimaryKey> repository,
            string cacheName = null)
            : base(
                cacheManager,
                unitOfWorkManager,
                repository,
                cacheName)
        {
        }

        protected override string GetCacheKey(TEntity entity)
        {
            return GetCacheKey(entity.Id, entity.TenantId);
        }

        public override string ToString()
        {
            return $"MayHaveTenantEntityCache {CacheName}";
        }
    }
}
