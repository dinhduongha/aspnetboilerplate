using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.DynamicEntityParameters
{
    public interface IDynamicParameterStore
    {
        DynamicParameter Get(Guid id);

        Task<DynamicParameter> GetAsync(Guid id);

        DynamicParameter Get(string parameterName);

        Task<DynamicParameter> GetAsync(string parameterName);

        List<DynamicParameter> GetAll();

        Task<List<DynamicParameter>> GetAllAsync();

        void Add(DynamicParameter dynamicParameter);

        Task AddAsync(DynamicParameter dynamicParameter);

        void Update(DynamicParameter dynamicParameter);

        Task UpdateAsync(DynamicParameter dynamicParameter);

        void Delete(Guid id);

        Task DeleteAsync(Guid id);
    }
}
