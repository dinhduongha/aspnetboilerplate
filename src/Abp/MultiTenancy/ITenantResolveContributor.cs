using System;

namespace Abp.MultiTenancy
{
    public interface ITenantResolveContributor
    {
        Guid? ResolveTenantId();
    }
}