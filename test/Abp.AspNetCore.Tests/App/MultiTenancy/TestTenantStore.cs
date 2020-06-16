using System;
using System.Collections.Generic;
using System.Linq;
using Abp.MultiTenancy;

namespace Abp.AspNetCore.App.MultiTenancy
{
    public class TestTenantStore : ITenantStore
    {
        private readonly List<TenantInfo> _tenants = new List<TenantInfo>
        {
            new TenantInfo(Guid.Parse("00000000-0000-0000-0000-000000000001"), "Default"),
            new TenantInfo(Guid.Parse("00000000-0000-0000-0000-000000000042"), "acme"),
            new TenantInfo(Guid.Parse("00000000-0000-0000-0000-000000000043"), "vlsft")
        };

        public TenantInfo Find(Guid tenantId)
        {
            return _tenants.FirstOrDefault(t => t.Id == tenantId);
        }

        public TenantInfo Find(string tenancyName)
        {
            return _tenants.FirstOrDefault(t => t.TenancyName.ToLower() == tenancyName.ToLower());
        }
    }
}
