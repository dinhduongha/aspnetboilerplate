﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Abp.DynamicEntityProperties
{
    [Table("AbpDynamicProperties")]
    public class DynamicProperty : Entity, IMayHaveTenant
    {
        public string PropertyName { get; set; }

        public string InputType { get; set; }

        public string Permission { get; set; }
        
        public Guid? TenantId { get; set; }

        public virtual ICollection<DynamicPropertyValue> DynamicPropertyValues { get; set; }
    }
}
