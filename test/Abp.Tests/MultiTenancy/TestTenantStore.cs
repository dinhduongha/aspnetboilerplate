using System;
using System.Collections.Generic;
using System.Linq;
using Abp.MultiTenancy;

namespace Abp.Tests.MultiTenancy
{
    public class TestTenantStore : ITenantStore
    {
        private readonly List<TenantInfo> _tenants = new List<TenantInfo>
        {
            new TenantInfo(new Guid("00000000-0000-0000-0000-000000000001"), "Default")
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
