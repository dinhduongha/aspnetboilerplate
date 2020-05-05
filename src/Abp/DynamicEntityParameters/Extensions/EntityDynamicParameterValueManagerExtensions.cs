using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Abp.DynamicEntityParameters.Extensions
{
    public static class EntityDynamicParameterValueManagerExtensions
    {
        public static List<EntityDynamicParameterValue> GetValues<TEntity, TPrimaryKey>(this IEntityDynamicParameterValueManager manager, string entityId)
            where TEntity : IEntity<TPrimaryKey>
        {
            return manager.GetValues(entityFullName: typeof(TEntity).FullName, entityId: entityId);
        }

        public static List<EntityDynamicParameterValue> GetValues<TEntity>(this IEntityDynamicParameterValueManager manager, string entityId)
            where TEntity : IEntity<int>
        {
            return manager.GetValues<TEntity, int>(entityId: entityId);
        }

        public static Task<List<EntityDynamicParameterValue>> GetValuesAsync<TEntity, TPrimaryKey>(this IEntityDynamicParameterValueManager manager, string entityId)
            where TEntity : IEntity<TPrimaryKey>
        {
            return manager.GetValuesAsync(entityFullName: typeof(TEntity).FullName, entityId: entityId);
        }

        public static Task<List<EntityDynamicParameterValue>> GetValuesAsync<TEntity>(this IEntityDynamicParameterValueManager manager, string entityId)
            where TEntity : IEntity<Guid>
        {
            return manager.GetValuesAsync<TEntity, Guid>(entityId: entityId);
        }

        public static List<EntityDynamicParameterValue> GetValues<TEntity, TPrimaryKey>(this IEntityDynamicParameterValueManager manager, string entityId, Guid dynamicParameterId)
            where TEntity : IEntity<TPrimaryKey>
        {
            return manager.GetValues(entityFullName: typeof(TEntity).FullName, entityId: entityId, dynamicParameterId: dynamicParameterId);
        }

        public static List<EntityDynamicParameterValue> GetValues<TEntity>(this IEntityDynamicParameterValueManager manager, string entityId, Guid dynamicParameterId)
            where TEntity : IEntity<Guid>
        {
            return manager.GetValues<TEntity, Guid>(entityId: entityId, dynamicParameterId: dynamicParameterId);
        }

        public static Task<List<EntityDynamicParameterValue>> GetValuesAsync<TEntity, TPrimaryKey>(this IEntityDynamicParameterValueManager manager, string entityId, Guid dynamicParameterId)
            where TEntity : IEntity<TPrimaryKey>
        {
            return manager.GetValuesAsync(entityFullName: typeof(TEntity).FullName, entityId: entityId, dynamicParameterId: dynamicParameterId);
        }

        public static Task<List<EntityDynamicParameterValue>> GetValuesAsync<TEntity>(this IEntityDynamicParameterValueManager manager, string entityId, Guid dynamicParameterId)
            where TEntity : IEntity<int>
        {
            return manager.GetValuesAsync<TEntity, int>(entityId: entityId, dynamicParameterId: dynamicParameterId);
        }

        public static List<EntityDynamicParameterValue> GetValues<TEntity, TPrimaryKey>(this IEntityDynamicParameterValueManager manager, string entityId, DynamicParameter dynamicParameter)
            where TEntity : IEntity<TPrimaryKey>
        {
            return manager.GetValues<TEntity, TPrimaryKey>(entityId: entityId, dynamicParameterId: dynamicParameter.Id);
        }

        public static List<EntityDynamicParameterValue> GetValues<TEntity>(this IEntityDynamicParameterValueManager manager, string entityId, DynamicParameter dynamicParameter)
            where TEntity : IEntity<Guid>
        {
            return manager.GetValues<TEntity>(entityId: entityId, dynamicParameterId: dynamicParameter.Id);
        }

        public static Task<List<EntityDynamicParameterValue>> GetValuesAsync<TEntity, TPrimaryKey>(this IEntityDynamicParameterValueManager manager, string entityId, DynamicParameter dynamicParameter)
            where TEntity : IEntity<TPrimaryKey>
        {
            return manager.GetValuesAsync<TEntity, TPrimaryKey>(entityId: entityId, dynamicParameterId: dynamicParameter.Id);
        }

        public static Task<List<EntityDynamicParameterValue>> GetValuesAsync<TEntity>(this IEntityDynamicParameterValueManager manager, string entityId, DynamicParameter dynamicParameter)
            where TEntity : IEntity<Guid>
        {
            return manager.GetValuesAsync<TEntity, Guid>(entityId: entityId, dynamicParameterId: dynamicParameter.Id);
        }

        public static List<EntityDynamicParameterValue> GetValues<TEntity, TPrimaryKey>(this IEntityDynamicParameterValueManager manager, string entityId, string parameterName)
            where TEntity : IEntity<TPrimaryKey>
        {
            return manager.GetValues(entityFullName: typeof(TEntity).FullName, entityId: entityId, parameterName: parameterName);
        }

        public static List<EntityDynamicParameterValue> GetValues<TEntity>(this IEntityDynamicParameterValueManager manager, string entityId, string parameterName)
            where TEntity : IEntity<Guid>
        {
            return manager.GetValues<TEntity, Guid>(entityId: entityId, parameterName: parameterName);
        }

        public static Task<List<EntityDynamicParameterValue>> GetValuesAsync<TEntity, TPrimaryKey>(this IEntityDynamicParameterValueManager manager, string entityId, string parameterName)
            where TEntity : IEntity<TPrimaryKey>
        {
            return manager.GetValuesAsync(entityFullName: typeof(TEntity).FullName, entityId: entityId, parameterName: parameterName);
        }

        public static Task<List<EntityDynamicParameterValue>> GetValuesAsync<TEntity>(this IEntityDynamicParameterValueManager manager, string entityId, string parameterName)
            where TEntity : IEntity<Guid>
        {
            return manager.GetValuesAsync<TEntity, Guid>(entityId: entityId, parameterName: parameterName);
        }
    }
}
