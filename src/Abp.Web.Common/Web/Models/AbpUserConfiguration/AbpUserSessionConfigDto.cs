using Abp.MultiTenancy;
using System;

namespace Abp.Web.Models.AbpUserConfiguration
{
    public class AbpUserSessionConfigDto
    {
        public Guid? UserId { get; set; }

        public int? TenantId { get; set; }

        public Guid? ImpersonatorUserId { get; set; }

        public int? ImpersonatorTenantId { get; set; }

        public MultiTenancySides MultiTenancySide { get; set; }
    }
}