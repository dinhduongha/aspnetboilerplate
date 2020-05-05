using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;

namespace Abp.DynamicEntityParameters
{
    public class DynamicParameterStore : IDynamicParameterStore, ITransientDependency
    {
        private readonly IRepository<DynamicParameter, Guid> _dynamicParameterRepository;

        public DynamicParameterStore(IRepository<DynamicParameter, Guid> dynamicParameterRepository)
        {
            _dynamicParameterRepository = dynamicParameterRepository;
        }

        public virtual DynamicParameter Get(Guid id)
        {
            return _dynamicParameterRepository.Get(id);
        }

        public virtual Task<DynamicParameter> GetAsync(Guid id)
        {
            return _dynamicParameterRepository.GetAsync(id);
        }

        public virtual DynamicParameter Get(string parameterName)
        {
            return _dynamicParameterRepository.FirstOrDefault(x => x.ParameterName == parameterName);
        }

        public virtual Task<DynamicParameter> GetAsync(string parameterName)
        {
            return _dynamicParameterRepository.FirstOrDefaultAsync(x => x.ParameterName == parameterName);
        }

        public virtual List<DynamicParameter> GetAll()
        {
            return _dynamicParameterRepository.GetAllList();
        }

        public virtual async Task<List<DynamicParameter>> GetAllAsync()
        {
            return await _dynamicParameterRepository.GetAllListAsync();
        }

        public virtual void Add(DynamicParameter dynamicParameter)
        {
            _dynamicParameterRepository.Insert(dynamicParameter);
        }

        public virtual Task AddAsync(DynamicParameter dynamicParameter)
        {
            return _dynamicParameterRepository.InsertAsync(dynamicParameter);
        }

        public virtual void Update(DynamicParameter dynamicParameter)
        {
            _dynamicParameterRepository.Update(dynamicParameter);
        }

        public virtual Task UpdateAsync(DynamicParameter dynamicParameter)
        {
            return _dynamicParameterRepository.UpdateAsync(dynamicParameter);
        }

        public virtual void Delete(Guid id)
        {
            _dynamicParameterRepository.Delete(id);
        }

        public virtual Task DeleteAsync(Guid id)
        {
            return _dynamicParameterRepository.DeleteAsync(id);
        }
    }
}
