using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.DynamicEntityParameters
{
    public class NullDynamicParameterValueStore : IDynamicParameterValueStore
    {
        public static NullDynamicParameterValueStore Instance = new NullDynamicParameterValueStore();

        public DynamicParameterValue Get(Guid id)
        {
            return default;
        }

        public Task<DynamicParameterValue> GetAsync(Guid id)
        {
            return Task.FromResult<DynamicParameterValue>(default);
        }

        public List<DynamicParameterValue> GetAllValuesOfDynamicParameter(Guid dynamicParameterId)
        {
            return new List<DynamicParameterValue>();
        }

        public Task<List<DynamicParameterValue>> GetAllValuesOfDynamicParameterAsync(Guid dynamicParameterId)
        {
            return Task.FromResult(new List<DynamicParameterValue>());
        }

        public void Add(DynamicParameterValue dynamicParameterValue)
        {
        }

        public Task AddAsync(DynamicParameterValue dynamicParameterValue)
        {
            return Task.CompletedTask;
        }

        public void Update(DynamicParameterValue dynamicParameterValue)
        {
        }

        public Task UpdateAsync(DynamicParameterValue dynamicParameterValue)
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

        public void CleanValues(Guid dynamicParameterId)
        {
        }

        public Task CleanValuesAsync(Guid dynamicParameterId)
        {
            return Task.CompletedTask;
        }
    }
}
