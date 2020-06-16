using Abp.MultiTenancy;
using System;

namespace Abp.Web.Models.AbpUserConfiguration
{
    public class AbpUserSessionConfigDto
    {
        public Guid? UserId { get; set; }

        public Guid? TenantId { get; set; }

        public Guid? ImpersonatorUserId { get; set; }

        public Guid? ImpersonatorTenantId { get; set; }

        public MultiTenancySides MultiTenancySide { get; set; }
    }
}