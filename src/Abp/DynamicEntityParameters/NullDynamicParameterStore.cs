﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.DynamicEntityParameters
{
    public class NullDynamicParameterStore : IDynamicParameterStore
    {
        public static NullDynamicParameterStore Instance = new NullDynamicParameterStore();

        public DynamicParameter Get(Guid id)
        {
            return default;
        }

        public Task<DynamicParameter> GetAsync(Guid id)
        {
            return Task.FromResult<DynamicParameter>(default);
        }

        public DynamicParameter Get(string parameterName)
        {
            return default;
        }

        public Task<DynamicParameter> GetAsync(string parameterName)
        {
            return Task.FromResult<DynamicParameter>(default);
        }

        public List<DynamicParameter> GetAll()
        {
            return new List<DynamicParameter>();
        }

        public Task<List<DynamicParameter>> GetAllAsync()
        {
            return Task.FromResult(new List<DynamicParameter>());
        }

        public void Add(DynamicParameter dynamicParameter)
        {
        }

        public Task AddAsync(DynamicParameter dynamicParameter)
        {
            return Task.CompletedTask;
        }

        public void Update(DynamicParameter dynamicParameter)
        {
        }

        public Task UpdateAsync(DynamicParameter dynamicParameter)
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
    }
}
