using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Abp.DynamicEntityParameters
{
    [Table("AbpDynamicParameters")]
    public class DynamicParameter : Entity<Guid>, IMayHaveTenant
    {
        public string ParameterName { get; set; }

        public string InputType { get; set; }

        public string Permission { get; set; }

        public virtual ICollection<DynamicParameterValue> DynamicParameterValues { get; set; }

        public Guid? TenantId { get; set; }
        public DynamicParameter()
        {
            Id = SequentialGuidGenerator.Instance.Create();
        }
    }
}
