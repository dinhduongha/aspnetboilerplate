using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Abp.DynamicEntityParameters
{
    [Table("AbpDynamicParameterValues")]
    public class DynamicParameterValue : Entity<Guid>, IMayHaveTenant
    {
        /// <summary>
        /// Value.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Value { get; set; }

        public int? TenantId { get; set; }

        public Guid DynamicParameterId { get; set; }

        [ForeignKey("DynamicParameterId")]
        public virtual DynamicParameter DynamicParameter { get; set; }

        public DynamicParameterValue()
        {
            Id = SequentialGuidGenerator.Instance.Create();
        }

        public DynamicParameterValue(DynamicParameter dynamicParameter, string value, int? tenantId)
        {
            Id = SequentialGuidGenerator.Instance.Create();
            Value = value;
            TenantId = tenantId;
            DynamicParameterId = dynamicParameter.Id;
        }
    }
}
