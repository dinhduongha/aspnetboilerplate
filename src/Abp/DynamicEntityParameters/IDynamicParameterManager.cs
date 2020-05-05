using System;
using System.Threading.Tasks;

namespace Abp.DynamicEntityParameters
{
    public interface IDynamicParameterManager
    {
        DynamicParameter Get(Guid id);

        Task<DynamicParameter> GetAsync(Guid id);

        DynamicParameter Get(string parameterName);

        Task<DynamicParameter> GetAsync(string parameterName);

        void Add(DynamicParameter dynamicParameter);

        Task AddAsync(DynamicParameter dynamicParameter);

        void Update(DynamicParameter dynamicParameter);

        Task UpdateAsync(DynamicParameter dynamicParameter);

        void Delete(Guid id);

        Task DeleteAsync(Guid id);
    }
}
