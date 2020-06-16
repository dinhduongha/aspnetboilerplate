using System;

namespace Abp.MultiTenancy
{
    public class TenantInfo
    {
        public Guid Id { get; set; }

        public string TenancyName { get; set; }

        public TenantInfo(Guid id, string tenancyName)
        {
            Id = id;
            TenancyName = tenancyName;
        }
    }
}