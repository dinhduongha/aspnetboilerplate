using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Abp.DynamicEntityParameters
{
    [Table("AbpEntityDynamicParameterValues")]
    public class EntityDynamicParameterValue : Entity<Guid>, IMayHaveTenant
    {
        [Required(AllowEmptyStrings = false)]
        public string Value { get; set; }

        public string EntityId { get; set; }

        public Guid EntityDynamicParameterId { get; set; }

        public virtual EntityDynamicParameter EntityDynamicParameter { get; set; }

        public Guid? TenantId { get; set; }

        public EntityDynamicParameterValue()
        {
            Id = SequentialGuidGenerator.Instance.Create();
        }

        public EntityDynamicParameterValue(EntityDynamicParameter entityDynamicParameter, string entityId, string value, Guid? tenantId)
        {
            Id = SequentialGuidGenerator.Instance.Create();
            EntityDynamicParameterId = entityDynamicParameter.Id;
            EntityId = entityId;
            Value = value;
            TenantId = tenantId;
        }
    }
}
