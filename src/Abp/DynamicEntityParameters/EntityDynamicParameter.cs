using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Abp.DynamicEntityParameters
{
    [Table("AbpEntityDynamicParameters")]
    public class EntityDynamicParameter : Entity<Guid>, IMayHaveTenant
    {
        public string EntityFullName { get; set; }

        [Required]
        public Guid DynamicParameterId { get; set; }

        [ForeignKey("DynamicParameterId")]
        public virtual DynamicParameter DynamicParameter { get; set; }

        public Guid? TenantId { get; set; }
        public EntityDynamicParameter()
        {
            Id = SequentialGuidGenerator.Instance.Create();
        }
    }
}
