using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.DynamicEntityParameters
{
    public class NullEntityDynamicParameterValueStore : IEntityDynamicParameterValueStore
    {
        public static NullEntityDynamicParameterValueStore Instance = new NullEntityDynamicParameterValueStore();

        public EntityDynamicParameterValue Get(Guid id)
        {
            return default;
        }

        public Task<EntityDynamicParameterValue> GetAsync(Guid id)
        {
            return Task.FromResult<EntityDynamicParameterValue>(default);
        }

        public void Add(EntityDynamicParameterValue entityDynamicParameterValue)
        {
        }

        public Task AddAsync(EntityDynamicParameterValue entityDynamicParameterValue)
        {
            return Task.CompletedTask;
        }

        public void Update(EntityDynamicParameterValue entityDynamicParameterValue)
        {
        }

        public Task UpdateAsync(EntityDynamicParameterValue entityDynamicParameterValue)
        {
            return Task.CompletedTask;
        }

        public void Delete(Guid id)
        {
        }

        public Task DeleteAsync(Guid id)
        {
            return Task.CompletedTask;
        }

        public List<EntityDynamicParameterValue> GetValues(Guid entityDynamicParameterId, string entityId)
        {
            return new List<EntityDynamicParameterValue>();
        }

        public Task<List<EntityDynamicParameterValue>> GetValuesAsync(Guid entityDynamicParameterId, string entityId)
        {
            return Task.FromResult(new List<EntityDynamicParameterValue>());
        }

        public List<EntityDynamicParameterValue> GetValues(string entityFullName, string entityId)
        {
            return new List<EntityDynamicParameterValue>();
        }

        public Task<List<EntityDynamicParameterValue>> GetValuesAsync(string entityFullName, string entityId)
        {
            return Task.FromResult(new List<EntityDynamicParameterValue>());
        }

        public List<EntityDynamicParameterValue> GetValues(string entityFullName, string entityId, Guid dynamicParameterId)
        {
            return new List<EntityDynamicParameterValue>();
        }

        public Task<List<EntityDynamicParameterValue>> GetValuesAsync(string entityFullName, string entityId, Guid dynamicParameterId)
        {
            return Task.FromResult(new List<EntityDynamicParameterValue>());
        }

        public void CleanValues(Guid entityDynamicParameterId, string entityId)
        {
        }

        public Task CleanValuesAsync(Guid entityDynamicParameterId, string entityId)
        {
            return Task.CompletedTask;
        }
    }
}
