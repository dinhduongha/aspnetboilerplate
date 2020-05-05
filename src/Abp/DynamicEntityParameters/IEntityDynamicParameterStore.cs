using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.DynamicEntityParameters
{
    public interface IEntityDynamicParameterStore
    {
        EntityDynamicParameter Get(Guid id);

        Task<EntityDynamicParameter> GetAsync(Guid id);

        List<EntityDynamicParameter> GetAll();

        Task<List<EntityDynamicParameter>> GetAllAsync();

        List<EntityDynamicParameter> GetAll(string entityFullName);

        Task<List<EntityDynamicParameter>> GetAllAsync(string entityFullName);
        
        void Add(EntityDynamicParameter entityDynamicParameter);

        Task AddAsync(EntityDynamicParameter entityDynamicParameter);

        void Update(EntityDynamicParameter entityDynamicParameter);

        Task UpdateAsync(EntityDynamicParameter entityDynamicParameter);

        void Delete(Guid id);

        Task DeleteAsync(Guid id);
    }
}
