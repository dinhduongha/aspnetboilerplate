using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Linq;

namespace Abp.DynamicEntityParameters
{
    public class EntityDynamicParameterValueStore : IEntityDynamicParameterValueStore, ITransientDependency
    {
        private readonly IRepository<EntityDynamicParameterValue, Guid> _entityDynamicParameterValueRepository;
        private readonly IAsyncQueryableExecuter _asyncQueryableExecuter;

        public EntityDynamicParameterValueStore(IRepository<EntityDynamicParameterValue, Guid> entityDynamicParameterValueRepository, IAsyncQueryableExecuter asyncQueryableExecuter)
        {
            _entityDynamicParameterValueRepository = entityDynamicParameterValueRepository;
            _asyncQueryableExecuter = asyncQueryableExecuter;
        }

        public virtual EntityDynamicParameterValue Get(Guid id)
        {
            return _entityDynamicParameterValueRepository.Get(id);
        }

        public virtual Task<EntityDynamicParameterValue> GetAsync(Guid id)
        {
            return _entityDynamicParameterValueRepository.GetAsync(id);
        }

        public virtual void Add(EntityDynamicParameterValue entityDynamicParameterValue)
        {
            _entityDynamicParameterValueRepository.Insert(entityDynamicParameterValue);
        }

        public virtual Task AddAsync(EntityDynamicParameterValue entityDynamicParameterValue)
        {
            return _entityDynamicParameterValueRepository.InsertAsync(entityDynamicParameterValue);
        }

        public virtual void Update(EntityDynamicParameterValue entityDynamicParameterValue)
        {
            _entityDynamicParameterValueRepository.Update(entityDynamicParameterValue);
        }

        public virtual Task UpdateAsync(EntityDynamicParameterValue entityDynamicParameterValue)
        {
            return _entityDynamicParameterValueRepository.UpdateAsync(entityDynamicParameterValue);
        }

        public virtual void Delete(Guid id)
        {
            _entityDynamicParameterValueRepository.Delete(id);
        }

        public virtual Task DeleteAsync(Guid id)
        {
            return _entityDynamicParameterValueRepository.DeleteAsync(id);
        }

        public virtual List<EntityDynamicParameterValue> GetValues(Guid entityDynamicParameterId, string entityId)
        {
            return _entityDynamicParameterValueRepository.GetAll().Where(val =>
                val.EntityId == entityId && val.EntityDynamicParameterId == entityDynamicParameterId).ToList();
        }

        public virtual Task<List<EntityDynamicParameterValue>> GetValuesAsync(Guid entityDynamicParameterId, string entityId)
        {
            return _asyncQueryableExecuter.ToListAsync(
                _entityDynamicParameterValueRepository.GetAll()
                .Where(val => val.EntityId == entityId && val.EntityDynamicParameterId == entityDynamicParameterId)
                );
        }

        public List<EntityDynamicParameterValue> GetValues(string entityFullName, string entityId)
        {
            return _entityDynamicParameterValueRepository.GetAll()
                .Where(val => val.EntityId == entityId && val.EntityDynamicParameter.EntityFullName == entityFullName)
                .ToList();
        }

        public Task<List<EntityDynamicParameterValue>> GetValuesAsync(string entityFullName, string entityId)
        {
            return _asyncQueryableExecuter.ToListAsync(
                _entityDynamicParameterValueRepository.GetAll()
                    .Where(val => val.EntityId == entityId && val.EntityDynamicParameter.EntityFullName == entityFullName)
                );
        }

        public List<EntityDynamicParameterValue> GetValues(string entityFullName, string entityId, Guid dynamicParameterId)
        {
            return _entityDynamicParameterValueRepository.GetAll()
                .Where(val =>
                    val.EntityId == entityId &&
                    val.EntityDynamicParameter.EntityFullName == entityFullName &&
                    val.EntityDynamicParameter.DynamicParameterId == dynamicParameterId
                )
                .ToList();
        }

        public Task<List<EntityDynamicParameterValue>> GetValuesAsync(string entityFullName, string entityId, Guid dynamicParameterId)
        {
            return _asyncQueryableExecuter.ToListAsync(
                _entityDynamicParameterValueRepository.GetAll()
                    .Where(val =>
                        val.EntityId == entityId &&
                        val.EntityDynamicParameter.EntityFullName == entityFullName &&
                        val.EntityDynamicParameter.DynamicParameterId == dynamicParameterId
                    )
            );
        }

        public virtual void CleanValues(Guid entityDynamicParameterId, string entityId)
        {
            var list = _entityDynamicParameterValueRepository.GetAll().Where(val =>
                 val.EntityId == entityId && val.EntityDynamicParameterId == entityDynamicParameterId).ToList();

            foreach (var entityDynamicParameterValue in list)
            {
                _entityDynamicParameterValueRepository.Delete(entityDynamicParameterValue);
            }
        }

        public virtual async Task CleanValuesAsync(Guid entityDynamicParameterId, string entityId)
        {
            var list = await _asyncQueryableExecuter.ToListAsync(_entityDynamicParameterValueRepository.GetAll().Where(val =>
                 val.EntityId == entityId && val.EntityDynamicParameterId == entityDynamicParameterId));

            foreach (var entityDynamicParameterValue in list)
            {
                await _entityDynamicParameterValueRepository.DeleteAsync(entityDynamicParameterValue);
            }
        }
    }
}
