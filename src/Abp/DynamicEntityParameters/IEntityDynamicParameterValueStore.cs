using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.DynamicEntityParameters
{
    public interface IEntityDynamicParameterValueStore
    {
        EntityDynamicParameterValue Get(Guid id);

        Task<EntityDynamicParameterValue> GetAsync(Guid id);

        void Add(EntityDynamicParameterValue entityDynamicParameterValue);

        Task AddAsync(EntityDynamicParameterValue entityDynamicParameterValue);

        void Update(EntityDynamicParameterValue entityDynamicParameterValue);

        Task UpdateAsync(EntityDynamicParameterValue entityDynamicParameterValue);

        void Delete(Guid id);

        Task DeleteAsync(Guid id);

        List<EntityDynamicParameterValue> GetValues(Guid entityDynamicParameterId, string entityId);

        Task<List<EntityDynamicParameterValue>> GetValuesAsync(Guid entityDynamicParameterId, string entityId);

        List<EntityDynamicParameterValue> GetValues(string entityFullName, string entityId);

        Task<List<EntityDynamicParameterValue>> GetValuesAsync(string entityFullName, string entityId);

        List<EntityDynamicParameterValue> GetValues(string entityFullName, string entityId, Guid dynamicParameterId);

        Task<List<EntityDynamicParameterValue>> GetValuesAsync(string entityFullName, string entityId, Guid dynamicParameterId);

        void CleanValues(Guid entityDynamicParameterId, string entityId);

        Task CleanValuesAsync(Guid entityDynamicParameterId, string entityId);
    }
}
