using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.DynamicEntityParameters
{
    public interface IDynamicParameterValueStore
    {
        DynamicParameterValue Get(Guid id);

        Task<DynamicParameterValue> GetAsync(Guid id);

        List<DynamicParameterValue> GetAllValuesOfDynamicParameter(Guid dynamicParameterId);

        Task<List<DynamicParameterValue>> GetAllValuesOfDynamicParameterAsync(Guid dynamicParameterId);

        void Add(DynamicParameterValue dynamicParameterValue);

        Task AddAsync(DynamicParameterValue dynamicParameterValue);

        void Update(DynamicParameterValue dynamicParameterValue);

        Task UpdateAsync(DynamicParameterValue dynamicParameterValue);

        void Delete(Guid id);

        Task DeleteAsync(Guid id);

        void CleanValues(Guid dynamicParameterId);

        Task CleanValuesAsync(Guid dynamicParameterId);
    }
}
